using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HMetrics
{
    internal static class Tools
    {
        internal static string GetFileText(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
        internal static string ReadCpuInfoProperty(string inputString, string pattern, int GroupIndex)
        {
            return Regex.Match(inputString, pattern).Groups[GroupIndex].Value;
        }
        internal static List<string> ReadCpuInfoProperties(string inputString, string pattern, int GroupIndex)
        {
            List<string> list = new List<string>();
            foreach (Match match in Regex.Matches(inputString, pattern))
            {
                list.Add(match.Groups[GroupIndex].Value);
            }
            return list;
        }
        internal static bool ReadCpuInfoProperty(string inputString, string pattern, int GroupIndex, out string ReadCpuInfoProperty)
        {
            var match = Regex.Match(inputString, pattern).Groups[GroupIndex];
            ReadCpuInfoProperty = match.Value;
            return match.Length > 0;
        }
        internal static string ExecuteShellCommand(string cmd, Dictionary<string, string> environmentVariables = null)
        {
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{cmd.Replace("\"", "\\\"")}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                if (environmentVariables != null)
                {
                    foreach (var variable in environmentVariables)
                    {
                        process.StartInfo.EnvironmentVariables[variable.Key] = variable.Value;
                    }
                }

                process.Start();
                var result = process.StandardOutput.ReadToEnd().Trim('\n').Trim('\r');
                process.WaitForExit();
                return result;
            }
        }

    }
}