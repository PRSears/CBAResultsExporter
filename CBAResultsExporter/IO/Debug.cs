using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CBAResultsExporter.IO
{
    public static class Debug
    {
        private static string PathToLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CBAResultsExporter\Log.txt";

        public static void WriteLog(string Message)
        {
            string outMessage = string.Format("[{0}]: {1}{2}    {3}", DateTime.Now.ToString(), Message, Environment.NewLine, Environment.StackTrace);

            try
            {
                using (StreamWriter writer = File.AppendText(PathToLog))
                {
                    writer.WriteLine(outMessage);
                }
            }
            catch(Exception e)
            {
                Irony(new Exception(Message), e);
            }
        }

        public static void WriteLog(string[] Message)
        {
            foreach (string message in Message) 
                WriteLog(message);
        }

        public static void WriteLog(Exception ex)
        {
            string outMessage = string.Format("[{0}]: {1} @ {2}{3}    {4}", DateTime.Now.ToString(), ex.Message, ex.TargetSite, Environment.NewLine, ex.StackTrace);

            try
            {
                using (StreamWriter writer = File.AppendText(PathToLog))
                {
                    writer.WriteLine(outMessage);
                }
            }
            catch(Exception e)
            {
                Irony(ex, e);
            }
        }

        public static void WriteStackTrace()
        {
            try
            {
                using (StreamWriter writer = File.AppendText(PathToLog))
                {
                    writer.WriteLine(Environment.StackTrace);
                }
            }
            catch(Exception e)
            {
                Irony(e, e);
            }
        }

        private static void Irony(Exception outerException, Exception innerException)
        {
            MessageBox.Show("Something went wrong while trying to log something else going wrong.\nSorry.\n\n" + 
                outerException.Message + "\n\n" + 
                innerException.Message + "\n" +
                innerException.StackTrace);
        }
    }
}
