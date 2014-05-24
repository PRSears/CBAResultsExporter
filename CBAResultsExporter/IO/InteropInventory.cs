using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel; 
using CBAResultsExporter.Objects;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MessageBox = System.Windows.Forms.MessageBox;
using Color = System.Drawing.Color;

namespace CBAResultsExporter.IO
{
    [Obsolete]
    public class InteropInventory
    {
        private Excel.Application ex;
        private Excel.Workbooks workbooks;
        private Excel.Workbook workingbook;
        private Excel.Sheets sheets;
        private Excel.Worksheet workingsheet;
        private Excel.Range selected;

        private bool DEBUG = false;
        
        public InteropInventory(string pathToInventory)
        {
            CloseAllInstancesOf("EXCEL");

            if (!File.Exists(pathToInventory))
            {
                MessageBox.Show("The specified Inventory file does not exist or cannot be opened.");
                throw new FileNotFoundException("There was a problem locating the Inventory file.");
            }

            ex = new Excel.Application();
            workbooks = ex.Workbooks;
            workingbook = workbooks.Open(pathToInventory);
            sheets = workingbook.Worksheets;
            workingsheet = (Excel.Worksheet)sheets.get_Item("Inventory");
            selected = (Excel.Range)workingsheet.get_Range("A1", "A1");
        }

        public void Append(BatteryResult newResult)
        {
            int bottom = NextRow("B") + 2; // I'm not entirely sure why +1 doesn't work... 

            //HACK force text formating on B column each time data is appended. 
            //Excel is a crazy piece of shit that was changing the cell formatting whenever it felt like it.
            SetCellFormat("@", "B5", "B" + bottom);

            EditCell("B" + bottom, newResult.TestName);
            EditCell("C" + bottom, newResult.InitialVoltage.ToString());
            EditCell("D" + bottom, newResult.TestedCapacity.ToString(), ((newResult.TestedCapacity > 52) ? Color.DarkOliveGreen : Color.DarkRed));
            EditCell("G" + bottom, newResult.TestDate.ToShortDateString());
            EditCell("H" + bottom, "=CONCATENATE([@[Bat Serial '#]],\" - AH \",ROUND([@[Tested Capacity (AH)]], 2))"); // formula for scan bar
            if (newResult.Note != null) EditCell("I" + bottom, newResult.Note);

            //DEBUG
            if (DEBUG) MessageBox.Show("Data written: " + newResult.TestName + "\n" + 
                                                            "Data read: " + GetCell("B" + bottom).ToString());
        }

        private string GetCell(string coordinate)
        {
            selected = (Excel.Range)workingsheet.get_Range(coordinate, coordinate);

            if (selected.Value2 == null)
                return null;

            object cellValue = selected.Value2;
            return cellValue.ToString();
        }

        private void EditCell(string coordinate, string value)
        {
            selected = (Excel.Range)workingsheet.get_Range(coordinate, coordinate);
            selected.Value2 = value;

            workingbook.Save();
        }

        private void EditCell(string coordinate, string value, System.Drawing.Color fontColor)
        {
            selected = (Excel.Range)workingsheet.get_Range(coordinate, coordinate);
            selected.Value2 = value;
            selected.Font.Color = fontColor;

            workingbook.Save();
        }

        public void SetCellFormat(string format, string startCellSelection, string endCellSelection)
        {
            selected = (Excel.Range)workingsheet.get_Range(startCellSelection, endCellSelection);
            selected.Cells.NumberFormat = format;
        }

        private int NextRow(string Column)
        {
            selected = workingsheet.UsedRange;
            int height = selected.Rows.Count;

            // if the cell isn't blank go to the next row
            // I think maybe the pivot table screws this up. 'Empty' cells inside the table seem to not be null
            if (GetCell(Column + height) != null)
                height++;
            
            return height;
        }

        public void CleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(selected);
            Marshal.FinalReleaseComObject(workingsheet);
            Marshal.FinalReleaseComObject(sheets);

            workingbook.Close(Type.Missing, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(workingbook);
            
            workbooks.Close();
            Marshal.FinalReleaseComObject(workbooks);

            ex.Quit();
            Marshal.FinalReleaseComObject(ex);
        }

        public bool TryOpenInventory(string pathToInventory)
        {
            try
            {
                System.Diagnostics.Process.Start(pathToInventory);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void CloseAllInstancesOf(string p)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(p))
                    proc.Kill();
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                    return; // the process is already exited
                if (ex is System.ComponentModel.Win32Exception)
                {
                    Debug.WriteLog(ex);
                    Debug.WriteLog("The process is terminating / could not be terminated");
                    return;
                }

                Debug.WriteLog(ex);
                throw;
            }
        }

        public void TestHarness()
        {
            Append(new BatteryResult("99-0007"));
            Append(new BatteryResult("99-0008"));
            Append(new BatteryResult("99-0009"));
            Append(new BatteryResult("99-0010"));
            Append(new BatteryResult("99-0011"));
        }

        //TODO Implement IDisposable and call CleanUp automatically

        //God damn the Interop classes get really messy, really fast. Opening an invisible Excel app just to edit 
        // a document? Really Microsoft? The fuck...
    }
}
