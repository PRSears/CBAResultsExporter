using System;
using System.IO;
using System.Xml;
using CBAResultsExporter.IO;

namespace CBAResultsExporter.Objects
{
    public class BatteryResult
    {
        public string TestName
        {
            get;
            private set;
        }

        public double InitialVoltage
        {
            get;
            private set;
        }

        public double TestedCapacity
        {
            get;
            private set;
        }

        public int TestMinutes
        {
            get { return (int)Math.Round(TestedCapacity/6*60); }
        }

        public float PercentCapacity
        {
            get { return (float)TestedCapacity / 75; }
        }

        public DateTime TestDate
        {
            get;
            private set;
        }
        
        public string Note
        {
            get;
            private set;
        }

        public BatteryResult(string testName, double initVoltage, double testCapacity, DateTime testDate)
        {
            TestName = testName;
            InitialVoltage = initVoltage;
            TestedCapacity = testCapacity;
            TestDate = testDate;
        }

        public BatteryResult(string testName, double initVoltage, double testCapacity, DateTime testDate, string note)
        {
            TestName = testName;
            InitialVoltage = initVoltage;
            TestedCapacity = testCapacity;
            TestDate = testDate;
            Note = note;
        }

        public BatteryResult(string testName)
        {
            TestName = testName;
            InitialVoltage = 0;
            TestedCapacity = 0;
            TestDate = DateTime.Now;
        }

        public BatteryResult(string testName, string note)
        {
            TestName = testName;
            InitialVoltage = 0;
            TestedCapacity = 0;
            TestDate = DateTime.Now;
            Note = note;
        }

        public BatteryResult(XmlDocument bt2File)
        {
            BT2Reader r = new BT2Reader(bt2File);

            this.TestName = r.Data.TestName;
            this.InitialVoltage = r.Data.InitialVoltage;
            this.TestedCapacity = r.Data.TestedCapacity;
            this.TestDate = r.Data.TestDate;
        }

        public BatteryResult(XmlDocument bt2File, string note)
        {
            BT2Reader r = new BT2Reader(bt2File);

            this.TestName = r.Data.TestName;
            this.InitialVoltage = r.Data.InitialVoltage;
            this.TestedCapacity = r.Data.TestedCapacity;
            this.TestDate = r.Data.TestDate;
            this.Note = note;
        }

        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;

            BatteryResult objResult = (BatteryResult)obj;

            if (!((this.TestName == objResult.TestName) &&
                  (this.TestedCapacity == objResult.TestedCapacity) &&
                  (this.InitialVoltage == objResult.InitialVoltage) &&
                  (this.TestDate == objResult.TestDate)))
                return false;
            
            return true;
        }

        public string ToScanbarString()
        {
            System.Text.StringBuilder t = new System.Text.StringBuilder();
            t.AppendFormat("{0} - AH {1}", 
                TestName, 
                TestedCapacity.ToString("F"));

            return t.ToString();
        }
    }
}
