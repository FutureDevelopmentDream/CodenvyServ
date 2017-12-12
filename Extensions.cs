using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ClientO1
{
    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            try
            {
                var escapedArgs = cmd.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
    public class Extensions
    {

    }
}
