using System;
using System.Collections.Generic;

namespace HMetrics
{
    internal abstract class CpuImpl
    {
        protected (int, bool) coresCount;
        protected (string, bool) modelName;
        protected (double, bool) clockAvg;
        protected (List<double>, bool) coresClock;
        protected (double, bool) usage;
        protected (double, bool) chipsetTemp;
        protected (double, bool) temperature;
        public int CoresCount => coresCount.Item2 ? coresCount.Item1 : 0;
        public string ModelName => modelName.Item2 ? modelName.Item1 : "Unknown";
        public double ClockAvg
        {
            get
            {
                getClockAvg();
                return clockAvg.Item2 ? clockAvg.Item1 : double.NaN;
            }
        }
        public List<double> CoresClock
        {
            get
            {
                getCoresClock();
                return coresClock.Item2 ? coresClock.Item1 : new List<double>();
            }
        }
        public double Usage
        {
            get
            {
                GetUsage();
                return usage.Item2 ? usage.Item1 : double.NaN;
            }
        }
        public double ChipsetTemp
        {
            get
            {
                GetChipsetTemp();
                return chipsetTemp.Item2 ? chipsetTemp.Item1 : double.NaN;
            }
        }

        public double Temperature
        {
            get
            {
                GetTemperature();
                return temperature.Item2 ? temperature.Item1 : double.NaN;
            }
        }
        public abstract void getCoresClock();
        public abstract void getClockAvg();
        public abstract void GetUsage();
        public abstract void GetChipsetTemp();
        public abstract void GetTemperature();
    }
}