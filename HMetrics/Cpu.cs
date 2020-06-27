using System;

namespace HMetrics
{
    public static class Cpu
    {
        private static CpuImpl impl = CpuImplFabric.GetCpuImpl(Environment.OSVersion.Platform);
        public static int CoresCount => impl.CoresCount;
        public static double Usage => impl.Usage;
        public static double Frequency => impl.Frequency;

        public static string ModelName => impl.ModelName;
        public static double ChipsetTemp => impl.ChipsetTemp;
        public static double Temperature => impl.Temperature;

    }
}
