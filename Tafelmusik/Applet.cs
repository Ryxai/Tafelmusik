using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Runtime.Hosting;
using System.Text;

namespace Tafelmusik
{
    public class Applet<TKey, TOut> : IApp<TKey, TOut>
    {
        private string args;
        private TOut lastRunOutput = default(TOut);
        public string Name { get; private set; }
        private readonly Func<string, TOut> outputEvaluator;
        private readonly string path;
        private readonly Func<int, TOut, TOut> successMetric;
        private bool promptForCompletionIfError = false;


        private Applet(string _name, string _path, string _args,
            Func<int, TOut, TOut> _successMetric)
        {
            Name = _name;
            path = _path;
            args = _args;
            successMetric = _successMetric;
        }

        private Applet(string _name, string _path, string _args,
            Func<int, TOut, TOut> _successMetric,
            Func<string, TOut> _outputEvaluator)
        {
            Name = _name;
            path = _path;
            args = _args;
            successMetric = _successMetric;
            outputEvaluator = _outputEvaluator;
        }


        public TOut Run(TKey key)
        {
            return successMetric(appRunner(), lastRunOutput);
        }

        public void SetPromptForCompletionIfError()
        {
            promptForCompletionIfError = true;
        }

        public static Applet<TKey, TOut> Create(string name,
            Func<int, TOut, TOut> successEvaluator,
            Func<string, TOut> outputEvaluator, 
            string path="", string args = "")
        {
            if (string.Empty.Equals(path))
                path = Environment.CurrentDirectory;
            return new Applet<TKey, TOut>(name, path, args, successEvaluator,
                outputEvaluator);
        }

        private TOut EvalOuput(Process prs)
        {
            var str = new StringBuilder();
            while (!prs.StandardOutput.EndOfStream)
                str.Append(prs.StandardOutput.ReadLine());

            return outputEvaluator(str.ToString());
        }

        private int appRunner()
        {   if (!File.Exists($"{path}\\{Name}.exe"))
            {
                Console.WriteLine("Error locating executable " + Name +
                                ". Please place a good known copy in " + path);
                Console.Error.WriteLine("Process_loc_failed");
                return -1;
            }

            var prsInfo =
                new ProcessStartInfo($"{path}\\{Name}.exe")
                {
                    Arguments = args,
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true
                };
            var prs = Process.Start(prsInfo);
            if (prs == null) return -1;
            try
            {
                if (outputEvaluator != null)
                    lastRunOutput = EvalOuput(prs);
                prs.WaitForExit();
            }
            catch (Exception e)
            {
                if (promptForCompletionIfError && (e is Win32Exception || e is SystemException))
                {
                    Console.WriteLine(
                        "Cannot access process output. Did it finish running properly? (y/n)");
                    if (Console.ReadLine().Contains("y"))
                        return prs.ExitCode;
                }

                throw;
            }

            return prs.ExitCode;

        }
    }
}