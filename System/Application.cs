using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace System
{
    public class Application
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

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

        public void ProcessWaitForInputIdle(string pid, int timeout)
        {
            Process process;
            int tid;
            uint handlePid;

            try
            {
                //Busca o processo pelo ID
                process = Process.GetProcessById(int.Parse(pid));
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar o processo pelo pid.", ex);
            }

            tid = (int)GetWindowThreadProcessId(process.MainWindowHandle, out handlePid);
            if (tid == 0)
                throw new ArgumentException("Janela principal do processo não encontrada.");

            if (!WaitForInputIdle(Int32.Parse(pid), tid, timeout))
                throw new Exception("Processo demorou demais para responder.");            
        }

        private static bool WaitForInputIdle(int pid, int tid, int timeout = 0)
        {
            var tick = Environment.TickCount;
            do
            {
                if (IsThreadIdle((int)pid, tid)) return true;
                System.Threading.Thread.Sleep(15);
            } while (timeout > 0 && Environment.TickCount - tick < timeout);
            return false;
        }

        private static bool IsThreadIdle(int pid, int tid)
        {
            Process prc = System.Diagnostics.Process.GetProcessById(pid);
            var thr = prc.Threads.Cast<ProcessThread>().First((t) => tid == t.Id);
            return thr.ThreadState == ThreadState.Wait &&
                   thr.WaitReason == ThreadWaitReason.UserRequest;
        }
    }

    
}
