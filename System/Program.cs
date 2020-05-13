using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    class Program
    {
        public static void Main()
        {
            File f = new File();
            string ff = f.GetFilesBetweenDates(@"C:\Users\Zdan\Desktop\PTUS A500", "24/03/2020 10:00:00", "26/03/2020 10:57:21", "dd/MM/yyyy HH:mm:ss", "false");
            Console.WriteLine(ff);
            Console.ReadLine();
        }
    }
}
