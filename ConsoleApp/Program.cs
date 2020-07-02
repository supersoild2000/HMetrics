using System;
using System.Threading;
using HMetrics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            string a = Cpu.ModelName;
            Console.WriteLine("Cpu cores count: " + Cpu.CoresCount);
            Console.WriteLine("Cpu model name: " + Cpu.ModelName);

            while (true)
            {

                Console.WriteLine("\rCpu usage: " + Cpu.Usage.ToString("000.00") + " %\tChipset temp: " + Cpu.ChipsetTemp.ToString("000.00") + " °C" + " %\tCpu temp: " + Cpu.Temperature.ToString("000.00") + " °C" + "\tClockAvg: " + Cpu.ClockAvg.ToString("000") + " MHz");
                var list = Cpu.CoresClock;
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("\rCore " + i + ": " + list[i].ToString("00000.00") + " MHz");
                }
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.SetCursorPosition(0, Console.CursorTop - list.Count);
            }


        }
    }
}
