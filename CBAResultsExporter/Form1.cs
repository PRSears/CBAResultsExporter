using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using CBAResultsExporter.IO;
using CBAResultsExporter.Objects;
using Process = System.Diagnostics.Process;

namespace CBAResultsExporter
{
    public partial class CBAExporter : Form
    {
        public CBAExporter()
        {
            InitializeComponent();
            CheckStatus();
        }
        protected void Message(string message)
        {
            string build = "Build 2.0.0.17: ";
            toolStripStatusLabel1.Text = build + message;
        }

        private void buttonChooseImportFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialogChooseResults.ShowDialog();
            if (result == DialogResult.OK) // successful selection
            {
                this.TextBoxBT2Path.Text = folderBrowserDialogChooseResults.SelectedPath;
                Properties.Settings.Default.prevResultsFolder = this.TextBoxBT2Path.Text;
            }
            CheckStatus();
        }

        private void buttonOpenInventory_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // successful selection
            {
                this.textBoxExportTo.Text = saveFileDialog1.FileName;
                Properties.Settings.Default.prevInventoryFile = this.textBoxExportTo.Text;
            }
            CheckStatus();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Inventory inv;
            #region try Inventory initialize
            try
            {
                inv = new Inventory(textBoxExportTo.Text);
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    DialogResult = MessageBox.Show("The inventory file you're trying to export to is already open.\nDo you want to close Excel?\n\nNote: Any unsaved changes will be lost!", "Do you want to close Excel?", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes)
                    {
                        CloseAllInstancesOf("Excel");
                        buttonExport_Click(sender, e);
                    }
                    else
                        Message("Close any open Excel docs and try again.");
                }
                else
                {
                    MessageBox.Show("There was a problem opening the inventory file.\nYou could try creating a new one.");
                }
                Debug.WriteLog(ex);
                return;
            }
            #endregion

            BT2Reader r;
            #region try BT2Reader initialize
            try
            {
                r = new BT2Reader();
            }
            catch (Exception ex) // Sloppy...
            {
                Debug.WriteLog(ex);
                return;
            }
            #endregion

            if (CheckStatus())
            {
                Message("Loading bt2 files...");
            }
            else
            {
                Debug.WriteLog("Either the inventory or results folder was invalid.");
                Debug.WriteStackTrace();
                MessageBox.Show("Invalid path. Make sure you've selected the correct files/folders and try again.");
                return;
            }

            int step = (int)Math.Round((double)(100 / (Directory.GetFiles(TextBoxBT2Path.Text).Length)));

            List<BatteryResult> resultFiles = new List<BatteryResult>();

            foreach (string file in Directory.GetFiles(TextBoxBT2Path.Text, "*.bt2"))
            {
                r.Load(file);
                resultFiles.Add(r.Data);

                toolStripProgressBar1.Value += (int)Math.Round(step / 2d);
            }

            Message("Exporting to Spreadsheet...");
            inv.Append(resultFiles);

            toolStripProgressBar1.Value = 90;

            if (!inv.Save())
            {
                MessageBox.Show("There was a problem saving to the spreadsheet. Is the file read-only?");
                return;
            }

            if(!ScanbarWriter.Save(resultFiles, Path.GetDirectoryName(textBoxExportTo.Text)))
                MessageBox.Show("There was a problem saving the Scanbar labels to a text file.\nYou'll have to do it manually from the spreadsheet.");
                        
            toolStripProgressBar1.Value = 100;
            Message("Complete.");            
        }

        private bool CheckStatus()
        {
            if (Directory.Exists(TextBoxBT2Path.Text) && (textBoxExportTo.Text != string.Empty))
            {
                Message("Ready");
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Enabled = false;
                buttonExport.Enabled = true;
                Properties.Settings.Default.Save();
                return true;
            }
            else
            {
                Message("Choose a valid results folder and inventory file.");
                buttonExport.Enabled = false;

                return false;
            }
        }

        private void CloseAllInstancesOf(string p)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(p))
                    proc.Kill();

                System.Threading.Thread.Sleep(500);
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

        //TODO check to see if the result is already in the Inventory (avoid duplicates)
    }
}
