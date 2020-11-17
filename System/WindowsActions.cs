using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class WindowsActions
    {

        public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
            IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SetWindowText(IntPtr hWnd, string windowName);

        [DllImport("user32")]
        internal static extern int GetWindowText(int hWnd, String text, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(int hWnd);

        //[DllImport("user32.dll")]
        //public static extern int FindWindow(String text, String class_name);

        [DllImport("user32.dll")]
        public static extern int FindWindowEx(int parent, int start, String class_name);

        [DllImport("user32.dll")]
        public static extern int GetWindow(int parent, uint cmd);

        [DllImport("user32.dll")]
        static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("User32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int index);
        [DllImport("User32.dll")]
        public static extern IntPtr SendDlgItemMessage(IntPtr hWnd, int IDDlgItem, int uMsg, int nMaxCount, StringBuilder lpString);
        [DllImport("User32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        public string ChangeWindowNameByTitle(string windowName, string toName, int timeout)
        {
            uint pid = 0;
            int tid = 0;

            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                var result = new StringBuilder(255);
                GetWindowText(hWnd, result, result.Capacity + 1);
                string title = result.ToString();

                if (title.ToUpper().Contains(windowName.ToUpper()) && IsWindowVisible(hWnd))
                {
                    SetWindowText(hWnd, toName);
                    GetTopWindow(hWnd);

                    tid = (int) GetWindowThreadProcessId(hWnd,  out pid);
                    if (tid == 0) 
                        throw new ArgumentException("Janela não encontrada.");

                    return WaitForInputIdle((int)pid, tid, timeout);
                } 
                else
                {
                    return true;
                }
            };

            bool output = EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);

            if (!output)
                throw new Exception("Ocorreu um erro ao renomear a janela: " + Marshal.GetLastWin32Error());

            return pid.ToString();
        }

        public void ChangeWindowNameByProcessId(string defaultWindowNames, string newWindowName, string pid, int timeout)
        {
            //Declaração de variáveis
            IEnumerable<IntPtr> handles = null;
            Process process = null;
            StringBuilder message = new StringBuilder(255);
            int tid = 0;
            uint handlePid = 0;
            int result = 0;

            //Validações iniciais
            if (defaultWindowNames.Length == 0)
                throw new ArgumentNullException("defaultWindowNames");
            else if (newWindowName.Length == 0)
                throw new ArgumentNullException("newWindowName");
            else if (pid.Length == 0)
                throw new ArgumentNullException("pid");

            try
            {
                //Busca o processo pelo ID
                process = Process.GetProcessById(int.Parse(pid));
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar o processo pelo pid.", ex);
            }

            // Buscar todas as janelas do processo
            handles = EnumerateProcessWindowHandles(process);

            List<string> listDefaultWindowNames = new List<string>();
            if (defaultWindowNames.Contains(";"))
                listDefaultWindowNames = new List<string>(defaultWindowNames.Split(';'));
            else
                listDefaultWindowNames.Add(defaultWindowNames);

            foreach (IntPtr handle in handles)
            {
                if (handle == null || !IsWindowVisible(handle))
                    continue;

                //Busca o título da janela
                var task = Task.Run(() => SendMessage(handle, 0x000D, message.Capacity, message));
                if (!task.Wait(TimeSpan.FromSeconds(3)))
                {
                    throw new Exception("A janela demorou demais para responder.");
                }

                //Se a janela não tiver título, ignora
                if (message.Length == 0)
                    continue;

                //Se encontrar no processo uma WindowHandle com o title contendo um dos valores recebidos na variável "defaultWindowNames", renomeia ela para o valor da variável "newWindowName".
                foreach (var defaultWindowName in listDefaultWindowNames)
                {
                    if (message.ToString().ToUpper().Contains(defaultWindowName.ToUpper()) || message.ToString().ToUpper().Contains(newWindowName.ToUpper()))
                    {
                        //Manda renomear a janela
                        try
                        {
                            result = SetWindowText(handle, newWindowName);
                            if (result == 0)
                                throw new ArgumentException("Não foi possível renomear a janela.");

                            tid = (int)GetWindowThreadProcessId(handle, out handlePid);
                            if (tid == 0)
                                throw new ArgumentException("Janela não encontrada.");

                            if (!WaitForInputIdle(Int32.Parse(pid), tid, timeout))
                                throw new Exception("A janela demorou demais para responder.");

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
        }

        public void HandlePopupWindow(string button, string pid, int timeout, string defaultButton, string popupName)
        {
            //Declaração de variáveis
            StringBuilder message = new StringBuilder(255);
            string windowName = null;
            //Console.WriteLine("Default Button: \"" + defaultButton + "\"");
            //Console.WriteLine("");
            IEnumerable<IntPtr> windowHandles = null;
            IntPtr hwndChild = IntPtr.Zero;
            Process process = null;
            List<Button> buttons = new List<Button>();
            bool warningExists = true;
            bool warningFound = false;

            //Validações iniciais
            if (button.Length == 0)
                throw new ArgumentNullException("button");
            else if (pid.Length == 0)
                throw new ArgumentNullException("pid");

            //// Splita variavel button em lista de botões
            //List<string> listButtons = new List<string>();
            //if (button.Contains(";"))
            //    listButtons = new List<string>(button.Split(';'));
            //else
            //    listButtons.Add(button);

            foreach(string b in button.Split('|'))
            {
                buttons.Add(new Button(b, 0, ""));
            }

            try
            {
                //Busca o processo pelo ID
                process = Process.GetProcessById(int.Parse(pid));
                process.WaitForInputIdle(timeout);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            while (warningExists)
            {
                warningFound = false;

                //Busca todos WindowHandles do processo
                windowHandles = EnumerateProcessWindowHandles(process);

                foreach (IntPtr handle in windowHandles)
                {
                    if (handle == null || !IsWindowVisible(handle))
                        continue;

                    //Busca o título da janela
                    var task = Task.Run(() => SendMessage(handle, 0x000D, message.Capacity, message));
                    if (!task.Wait(TimeSpan.FromSeconds(3)))
                    {
                        throw new Exception("A janela demorou demais para responder.");
                    }

                    //Se a janela não tiver título, ignora
                    if (message.Length == 0)
                        continue;
                    else
                    {
                        //Coloca o nome da janela em UPPER CASE
                        windowName = message.ToString().ToUpper();

                        //Ignora os acentos no nome para comparação
                        windowName = RemoveAccents(windowName);

                        //Remove acentos e põe em UPPER CASE o nome da popup recebido como parâmetro
                        popupName = RemoveAccents(popupName);
                        popupName = popupName.ToUpper();

                        //Após encontrar a janela de Atenção
                        if (windowName.Contains(popupName))//ATENCAO
                        {
                            //Console.WriteLine("Window Name: " + windowName);
                            //Itera sobre os handles filhos com classe "Static" até que tenha um com tamanho diferente de zero, para identificar a mensagem do popup.
                            IntPtr handleInnterText = IntPtr.Zero;
                            int len = BuscarInnterText(handle, ref handleInnterText);

                            //Busca o texto do handle filho que contém o length maior que zero. (Mensagem da popup)
                            StringBuilder windowInnerText = new StringBuilder(len + 1);
                            GetWindowText(handleInnterText, windowInnerText, len + 1);
                            //Console.WriteLine("Window Message: " + windowInnerText.ToString());

                            warningFound = true;
                            // Buscar botão na janela de atenção
                            Button btn = null;
                            try
                            {
                                btn = buttons.Where(b => b.Value == 0).First();
                                hwndChild = FindWindowEx((IntPtr)handle, IntPtr.Zero, "Button", btn.Name);
                                //Caso tenha identificado o Handle zerado, tenta pesquisar ele com Underscore na primeira letra.
                                if (hwndChild.ToString().Equals("0"))
                                {
                                    hwndChild = FindWindowEx((IntPtr)handle, IntPtr.Zero, "Button", "&" + btn.Name);
                                }
                                // Caso o handle ainda esteja zerado, clica no botão default.
                                if (hwndChild.ToString().Equals("0"))
                                {
                                    hwndChild = FindWindowEx((IntPtr)handle, IntPtr.Zero, "Button", defaultButton);
                                    //Console.WriteLine("Button \"" + btn.Name + "\" Not Found - clicking default.");
                                    btn.Value = 2;
                                }
                                else
                                {
                                    //Console.WriteLine("\"" + btn.Name + "\" Found!");
                                    btn.Value = 1;
                                }
                            }
                            catch (Exception)
                            {
                                hwndChild = FindWindowEx((IntPtr)handle, IntPtr.Zero, "Button", defaultButton);
                                //Console.WriteLine("Button \"" + btn.Name + "\" Not Found - clicking default.");
                                btn.Value = 2;
                            }

                            //Buscar mensagem interna da janela
                            btn.WindowMessage = windowInnerText.ToString();

                            //Deixa a janela encontrada em primeiro plano.
                            SetForegroundWindow(hwndChild);

                            //Após colocar a janela em primeiro plano, aguarda 1 segundo antes de realizar o clique no botão.
                            //System.Threading.Thread.Sleep(1000);
                            process.WaitForInputIdle(timeout);

                            //Envia o clique para o botão encontrado.
                            IntPtr sendMessageReturn = SendMessage(hwndChild, 0x00F5, message.Capacity, message);

                            process.WaitForInputIdle(timeout);

                            //System.Threading.Thread.Sleep(3000);

                            //Console.WriteLine("");
                            //Console.WriteLine("");

                            break;
                        }
                    }
                }

                if (!warningFound)
                    warningExists = false;
            }

            String retorno = "";

            foreach (Button btn in buttons)
            {
                if (!btn.WindowMessage.Equals(""))
                    retorno = retorno.Length == 0 ? btn.toString() : retorno + "|" + btn.toString();
            }

            Console.Write("return:" + retorno);
        }

        private static int BuscarInnterText(IntPtr handle, ref IntPtr innerTextHandle)
        {
            int len;
            do
            {
                innerTextHandle = FindWindowEx(handle, innerTextHandle, "Static", null);
                len = GetWindowTextLength(innerTextHandle);
            } while (len == 0 && innerTextHandle != IntPtr.Zero);
            return len;
        }

        private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(Process process)
        {
            List<IntPtr> handles = new List<IntPtr>();

            ProcessThreadCollection threads = null;
            try
            {
                threads = process.Threads;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return handles;
            }

            foreach (ProcessThread thread in threads)
            {
                if (thread == null)
                    continue;

                int threadId = 0;
                try
                {
                    threadId = thread.Id;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    continue;
                }
                try
                {
                    EnumThreadWindows(threadId, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    continue;
                }
            }
            return handles;
        }

        private static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
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

        private List<IntPtr> GetRootWindowsOfProcess(int pid)
        {
            List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
            List<IntPtr> dsProcRootWindows = new List<IntPtr>();
            foreach (IntPtr hWnd in rootWindows)
            {
                uint lpdwProcessId;
                GetWindowThreadProcessId(hWnd, out lpdwProcessId);
                if (lpdwProcessId == pid)
                    dsProcRootWindows.Add(hWnd);
            }
            return dsProcRootWindows;
        }

        private static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                Win32Callback childProc = new Win32Callback(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        private class Button
        {
            string name;
            int value;
            string windowMessage;

            public Button(string name, int value, string windowMessage)
            {
                this.name = name;
                this.value = value;
                this.windowMessage = windowMessage;
            }

            public string Name { get => name; set => name = value; }
            public int Value { get => value; set => this.value = value; }
            public string WindowMessage { get => windowMessage; set => this.windowMessage = value; }

            public string toString()
            {
                return windowMessage.ToString();
            }
        }
    }


}