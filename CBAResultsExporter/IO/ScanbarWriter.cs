using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CBAResultsExporter.Objects;

namespace CBAResultsExporter.IO
{
    public class ScanbarWriter
    {
        public static bool Save(List<BatteryResult> results, string directory)
        {
            if (!Directory.Exists(directory))
                return false;

            try
            {
                using(StreamWriter stream = File.AppendText(Filename(directory)) )
                {
                    foreach(BatteryResult bat in results)
                    {
                        stream.WriteLine(bat.ToScanbarString());
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLog(e);
                return false;
            }

            return true;
        }

        public static string Filename(string directory)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");
            int increment = 1;

            StringBuilder b = new StringBuilder();
            b.AppendFormat("Scanbar_{0}__{1}.txt", timestamp, increment.ToString("D2"));
            
            while(File.Exists(Path.Combine(directory, b.ToString())))
            {
                b.Clear();
                b.AppendFormat("Scanbar_{0}__{1}.txt", timestamp, (++increment).ToString("D2"));
            }

            return Path.Combine(directory, b.ToString());
        }
    }
}
