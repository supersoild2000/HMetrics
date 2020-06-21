using System;
using HMetrics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = Cpu.ModelName;
            Console.WriteLine(Cpu.CoresCount);
            Console.WriteLine(Cpu.ModelName);
            Console.WriteLine(Cpu.Frequency);
            while (true)
            {
                Console.WriteLine(Cpu.Usage);
            }
        }
    }
}
