using System;
namespace HMetrics
{
    internal class CpuImplWin32 : CpuImpl
    {
        private static readonly Lazy<CpuImplWin32> lazy = new Lazy<CpuImplWin32>(() => new CpuImplWin32());

        public static CpuImplWin32 Current => lazy.Value;

        public override void GetUsage()
        {
            throw new NotImplementedException();
        }
    }
}