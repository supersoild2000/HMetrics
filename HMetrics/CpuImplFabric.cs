using System;
namespace HMetrics
{
    internal static class CpuImplFabric
    {
        internal static CpuImpl GetCpuImpl(PlatformID platform)
        {
            switch (platform)
            {
                case PlatformID.Win32NT:
                    return CpuImplWin32.Current;
                default:
                    return CpuImplUnix.Current;
            }
        }
    }
}