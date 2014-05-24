using System;
using System.IO;
using System.Xml;
using CBAResultsExporter.Objects;
using System.Globalization;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CBAResultsExporter.IO
{
    public class BT2Reader
    {
        private bool DEBUG = false;

        public BatteryResult Data
        {
            get
            {
                if (BT2File == null) throw new InvalidOperationException("Cannot create BatteryResult while no BT2 is loaded");

                // For debugging only
                if (DEBUG) MessageBox.Show(TestNode.Attributes[0].Value + (HasAppends() ? ": This file was appends." : ": no appends."));
                                
                return new BatteryResult(TestName, InitVoltage, TestedCapacity, TestedDate);
            }
        }

        private XmlDocument BT2File;

        public XmlNode TestNode
        {
            get
            {
                return BT2File.SelectSingleNode("/CBATest/Tests/Test");
            }
        }
        public XmlNode DateNode
        {
            get
            {
                return BT2File.SelectSingleNode("/CBATest/Tests/Test/DateStarted");
            }
        }
        public XmlNode SmplNode
        {
            get
            {
                return BT2File.SelectSingleNode("/CBATest/Tests/Test/Samples");
            }
        }

        public string TestName
        {
            get
            {
                return TestNode.Attributes[0].Value;
            }
        }
        public double InitVoltage
        {
            get
            {
                return double.Parse(SmplNode.ChildNodes[0].Attributes[1].Value);
            }
        }
        public double TestedCapacity
        {
            get
            {
                double totalCapacity = 0;

                XmlNode CurTestNode = TestNode;
                do
                {
                    totalCapacity += double.Parse(CurTestNode.SelectSingleNode("TestedCapacity").InnerText);
                    CurTestNode = CurTestNode.NextSibling;
                } while (CurTestNode != null);

                return totalCapacity;
            }
        }
        public DateTime TestedDate
        {
            get
            {
                return DateTime.Parse(DateNode.InnerText);
            }
        }
        
        public BT2Reader(string PathToBT2)
        {
            Load(PathToBT2);
        }

        public BT2Reader(XmlDocument BT2)
        {
            Load(BT2);
        }

        public BT2Reader()
        {

        }

        public void Load(string PathToBT2)
        {
            BT2File = new XmlDocument();
            try
            {
                BT2File.Load(PathToBT2);
            }
            catch(Exception ex)
            {
                if (ex is XmlException)
                    MessageBox.Show("There was a load or parse error in the XML. Check the BT2 file and try again.");
                if (ex is ArgumentException)
                    MessageBox.Show("The filename or path to the results (BT2) file contained illegal characters.");
                if (ex is PathTooLongException)
                    MessageBox.Show("The path to the results (BT2) file is too long. Check the directory names.");
                if (ex is DirectoryNotFoundException)
                    MessageBox.Show("The path to the results (BT2) files no longer exists.");
                if (ex is IOException)
                    MessageBox.Show("An I/O error occurred while opening the file.");
                if (ex is UnauthorizedAccessException)
                    MessageBox.Show("We don't have the required permissions to open the result (BT2) file. Is it Read-Only?");
                if (ex is NotSupportedException)
                    MessageBox.Show("The result (BT2) filename is in an invalid format. Does it contain any illegal chatacters?");
                Debug.WriteLog(ex);
                throw;
            }
        }

        public void Load(XmlDocument BT2)
        {
            BT2File = BT2;
        }        
        
        /// <summary>
        /// Test to see if one or more extra tests have been appended to the BT2 file.
        /// </summary>
        /// <returns>Returns true when one or more extra tests are appeneded to the BT2 file.</returns>
        public bool HasAppends()
        {
            try
            {
                if (TestNode.NextSibling.Name.Equals(TestNode.Name))
                    return true;
            }
            catch (NullReferenceException e)
            {
                // NextSibling is null. Messy, I know, but it works. Don't judge me.
                return false;
            }
            return false;
        }

        public string[] TestHarness(string PathToBT2)
        {
            /*
            string[] files = Directory.GetFiles(PathToBT2, "*.bt2");
            Load(files[0]);

            #region DEBUG
            debugValues = new string[4];
            debugValues[0] = Data.TestName;
            debugValues[1] = Data.InitialVoltage.ToString();
            debugValues[2] = Data.TestedCapacity.ToString();
            debugValues[3] = Data.TestDate.ToShortDateString();
            #endregion

            return debugValues;
             */
            throw new NotImplementedException();
        }
    }
}
