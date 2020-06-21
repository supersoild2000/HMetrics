using System;
namespace HMetrics
{
    internal abstract class CpuImpl
    {
        protected (int, bool) coresCount;
        protected (string, bool) modelName;
        protected (double, bool) frequency;
        protected (double, bool) usage;
        public int CoresCount => coresCount.Item2 ? coresCount.Item1 : 0;
        public string ModelName => modelName.Item2 ? modelName.Item1 : "Unknown";
        public double Frequency => frequency.Item2 ? frequency.Item1 : double.NaN;
        public double Usage
        {
            get
            {
                GetUsage();
                return usage.Item2 ? usage.Item1 : double.NaN;
            }
        }
        public abstract void GetUsage();
    }
}