using System;
using HMetrics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = Cpu.ModelName;
            Console.WriteLine("Cpu cores count: " + Cpu.CoresCount);
            Console.WriteLine("Cpu model name: " + Cpu.ModelName);
            Console.WriteLine("Frequency: " + Cpu.Frequency);
            while (true)
            {
                Console.WriteLine("Cpu usage: " + Cpu.Usage + " %\t temp: " + Cpu.Temperature + " °C");
            }
        }
    }
}
