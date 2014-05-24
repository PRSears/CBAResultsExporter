using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Color = System.Drawing.Color;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using CBAResultsExporter.Objects;

namespace CBAResultsExporter.IO
{
    public class Inventory
    {
        private ExcelPackage inv;
        private ExcelWorksheet sheet;

        public Inventory(string inventoryPath)
        {
            FileInfo f = new FileInfo(inventoryPath);
            if (f.Exists)
            {
                this.inv   = new ExcelPackage(f);
                this.sheet = inv.Workbook.Worksheets[1];
            }
            else Create(f);
        }

        public void Append(BatteryResult newResult)
        {
            List<BatteryResult> results = new List<BatteryResult>();
            results.Add(newResult);

            Append(results);
        }

        public void Append(List<BatteryResult> results)
        {
            int n = 1 + sheet.Cells.Last().End.Row;

            foreach (BatteryResult newResult in results)
            {
                sheet.Cells["A" + n].Value = newResult.TestName;
                sheet.Cells["B" + n].Value = newResult.InitialVoltage;
                sheet.Cells["C" + n].Value = newResult.TestedCapacity; 
                sheet.Cells["D" + n].Value = newResult.TestMinutes;
                sheet.Cells["E" + n].Value = newResult.PercentCapacity;
                sheet.Cells["F" + n].Value = newResult.TestDate.ToLongDateString();
                sheet.Cells["G" + n].Value = newResult.ToScanbarString();
                if (newResult.Note != string.Empty) sheet.Cells["H" + n].Value = newResult.Note;

                using(ExcelRange row = sheet.Cells["A"+n+":H"+n])
                {
                    row.Style.Font.Bold = true;
                    row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    row.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                    // Alternate background color
                    if(n % 2 != 0)
                    {
                        row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        row.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                }
                                
                sheet.Cells["B" + n].Style.Numberformat.Format = "##0.00";
                sheet.Cells["C" + n].Style.Numberformat.Format = "##0.00";
                sheet.Cells["C" + n].Style.Font.Color.SetColor((newResult.TestedCapacity > 52) ? Color.DarkOliveGreen : Color.DarkRed);
                sheet.Cells["D" + n].Style.Numberformat.Format = "##0";
                sheet.Cells["E" + n].Style.Numberformat.Format = "##0.00%";
                sheet.Cells["F" + n].Value = newResult.TestDate.ToLongDateString();
                sheet.Cells["G" + n].Value = newResult.ToScanbarString();
                
                n++;
            }

            sheet.Cells["A1:H" + n].AutoFitColumns();
        }

        public bool Save()
        {
            try
            {
                inv.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Create(FileInfo newFile)
        {
            this.inv = new ExcelPackage(newFile);
            this.sheet = this.inv.Workbook.Worksheets.Add("Battery Results");

            sheet.Cells["A1"].Value = "Batt Serial";
            sheet.Cells["B1"].Value = "Initial Voltage";
            sheet.Cells["C1"].Value = "Tested Capacity (AH)";
            sheet.Cells["D1"].Value = "Tested Time (minutes)";
            sheet.Cells["E1"].Value = "Tested Capacity";
            sheet.Cells["F1"].Value = "Test Date";
            sheet.Cells["G1"].Value = "Scan Bar Label";
            sheet.Cells["H1"].Value = "Notes";

            using (ExcelRange row = sheet.Cells["A1:H1"])
            {
                row.Style.Font.Size = 14;
                row.Style.Font.Color.SetColor(Color.White);
                row.Style.Font.Bold = true;
                row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                row.Style.Fill.BackgroundColor.SetColor(Color.SlateGray);
                row.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
                row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
    }
}
