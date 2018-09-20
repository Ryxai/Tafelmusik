using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = 0;
            if (args.Length > 0)
            {
                if (args[0] == "win32")
                    throw new Win32Exception();
                if (args[0] == "sys")
                    throw new SystemException();
                res = Int32.TryParse(args[0], out res) ? res : -1;
            }
            Console.WriteLine($"{res}");
            Environment.Exit(res);
        }
    }
}
