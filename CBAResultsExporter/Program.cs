using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace CBAResultsExporter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //throw new ArgumentException(Path.GetExtension(args[0]));
                if (File.Exists(args[0]) && Path.GetExtension(args[0]).Equals(".bt2"))
                    Properties.Settings.Default.prevResultsFolder = Path.GetDirectoryName(args[0]);
            }
            else if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null)
            {
                args = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                if (File.Exists(args[0]) && Path.GetExtension(args[0]).Equals(".bt2"))
                    Properties.Settings.Default.prevResultsFolder = Path.GetDirectoryName(args[0]);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CBAExporter());
        }
    }
}