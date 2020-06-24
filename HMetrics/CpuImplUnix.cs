using System;
using System.IO;
using System.Text.RegularExpressions;

namespace HMetrics
{
    internal class CpuImplUnix : CpuImpl
    {
        private const string usageScript = "awk -v a=\"$(awk '/cpu /{print $2+$4,$2+$4+$5}' /proc/stat; sleep 1)\" '/cpu /{split(a,b,\" \"); print 100*($2+$4-b[1])/($2+$4+$5-b[2])}'  /proc/stat";
        private static readonly Lazy<CpuImplUnix> lazy = new Lazy<CpuImplUnix>(() => new CpuImplUnix());
        private CpuImplUnix()
        {
            string cpuInfoLines = Tools.GetFileText(@"/proc/cpuinfo");
            if (string.IsNullOrEmpty(cpuInfoLines)) return;
            coresCount.Item2 = int.TryParse(Tools.ReadCpuInfoProperty(cpuInfoLines, @"cpu cores\s+:\s+(.+)", 1), out coresCount.Item1);
            modelName.Item2 = Tools.ReadCpuInfoProperty(cpuInfoLines, @"model name\s+:\s+(.+)", 1, out modelName.Item1);
            frequency.Item2 = double.TryParse(Tools.ReadCpuInfoProperty(cpuInfoLines, @"cpu MHz\s+:\s+(.+)", 1), out frequency.Item1);
        }

        public static CpuImplUnix Current => lazy.Value;

        public override void GetTemperature()
        {
            int temp;
            temperature.Item2 = int.TryParse(Tools.GetFileText(@"/sys/class/thermal/thermal_zone0/temp"), out temp);
            if (temperature.Item2) temperature.Item1 = temp / 1000.0;

        }

        public override void GetUsage() => usage.Item2 = double.TryParse(Tools.ExecuteShellCommand(usageScript), out usage.Item1);
        //usage.Item2 ? usage.Item1 : double.NaN
    }
}