using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace System
{
    public class Application
    {
        public string OpenApplication(string fileName, string arguments, int timeout)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            string applicationWindow;

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = fileName;

            if (!string.IsNullOrWhiteSpace(arguments))
                startInfo.Arguments = arguments;

            process.StartInfo = startInfo;

            process.Start();

            process.WaitForInputIdle(timeout);

            applicationWindow = process.MainWindowTitle;

            process.Close();

            return applicationWindow;
        }

        public void CloseApplication(string processName, int timeout)
        { 
            if (string.IsNullOrWhiteSpace(processName))
                throw new ArgumentNullException("processName");

            if (processName.Contains(".exe"))
                processName = processName.Replace(".exe", "");

            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.CloseMainWindow();
                process.WaitForExit(timeout);
                if (process.HasExited)
                    process.Close();
                else
                    throw new InvalidOperationException("An unknown error occurred. The process '" + processName + "' failed to exit.");
            }
        }

        public void StartProcess(string fileName, string arguments)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = fileName;

            if (!string.IsNullOrWhiteSpace(arguments))
                startInfo.Arguments = arguments;

            process.StartInfo = startInfo;

            process.Start();

            process.Close();
        }

        public void KillProcess(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
                throw new ArgumentNullException("processName");

            if (processName.Contains(".exe"))
                processName = processName.Replace(".exe", "");

            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.CloseMainWindow();
                process.Kill();
                process.Close();
            }
        }

        public void KillProcessByPID(string pid)
        {
            if (!int.TryParse(pid, out int result))
                throw new ArgumentException("Parâmetro inválido.", "pid");

            var process = Process.GetProcessById(result);

            process.Kill();
        }
    }

    
}
