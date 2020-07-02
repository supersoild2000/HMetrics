using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HMetrics
{
    internal class CpuImplUnix : CpuImpl
    {
        private const string usageScript = "awk -v a=\"$(awk '/cpu /{print $2+$4,$2+$4+$5}' /proc/stat; sleep 1)\" '/cpu /{split(a,b,\" \"); print 100*($2+$4-b[1])/($2+$4+$5-b[2])}'  /proc/stat";
        private const string cpuTempScript = "paste /sys/devices/platform/coretemp.0/hwmon/hwmon?/temp1_label /sys/devices/platform/coretemp.0/hwmon/hwmon?/temp1_input";
        private static readonly Lazy<CpuImplUnix> lazy = new Lazy<CpuImplUnix>(() => new CpuImplUnix());
        private CpuImplUnix()
        {
            string cpuInfoLines = Tools.GetFileText(@"/proc/cpuinfo");
            if (string.IsNullOrEmpty(cpuInfoLines)) return;
            coresCount.Item2 = int.TryParse(Tools.ReadCpuInfoProperty(cpuInfoLines, @"cpu cores\s+:\s+(.+)", 1), out coresCount.Item1);
            modelName.Item2 = Tools.ReadCpuInfoProperty(cpuInfoLines, @"model name\s+:\s+(.+)", 1, out modelName.Item1);
        }

        public static CpuImplUnix Current => lazy.Value;

        public override void GetChipsetTemp()
        {
            int temp;
            chipsetTemp.Item2 = int.TryParse(Tools.GetFileText(@"/sys/class/thermal/thermal_zone0/temp"), out temp);
            if (chipsetTemp.Item2) chipsetTemp.Item1 = temp / 1000.0;

        }

        public override void getClockAvg()
        {
            var coresClock = this.CoresClock;
            if (coresClock.Any())
            {
                clockAvg.Item1 = coresClock.Average();
                clockAvg.Item2 = true;
            }
            else clockAvg.Item2 = false;
        }

        public override void getCoresClock()
        {
            string cpuInfoLines = Tools.GetFileText(@"/proc/cpuinfo");
            if (string.IsNullOrEmpty(cpuInfoLines)) return;
            var strList = Tools.ReadCpuInfoProperties(cpuInfoLines, @"cpu MHz\s+:\s+(.+)", 1);
            List<double> clocks = new List<double>();
            double buf;
            foreach (var clock in strList)
            {
                coresClock.Item2 = double.TryParse(clock.Replace(',', '.'), out buf);
                if (coresClock.Item2) clocks.Add(buf);
                else return;
            }
            coresClock.Item1 = clocks;
        }

        public override void GetTemperature()
        {
            string output = Tools.ExecuteShellCommand(cpuTempScript);
            int temp;
            if (output == String.Empty)
            {
                temperature.Item2 = false;
                return;
            }
            var buf = output.Split('\t');
            if (buf[0] == "Package id 0")
            {
                temperature.Item2 = int.TryParse(buf[1], out temp);
                if (temperature.Item2) temperature.Item1 = temp / 1000.0;
            }
            else
            {
                temperature.Item2 = false;
            }

        }

        public override void GetUsage() => usage.Item2 = double.TryParse(Tools.ExecuteShellCommand(usageScript).Replace(',', '.'), out usage.Item1);
        //usage.Item2 ? usage.Item1 : double.NaN
    }
}