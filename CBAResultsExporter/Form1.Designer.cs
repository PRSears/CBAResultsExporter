namespace CBAResultsExporter
{
    partial class CBAExporter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CBAExporter));
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.TextBoxBT2Path = new System.Windows.Forms.TextBox();
            this.buttonChooseImportFolder = new System.Windows.Forms.Button();
            this.LabelImportXML = new System.Windows.Forms.Label();
            this.folderBrowserDialogChooseResults = new System.Windows.Forms.FolderBrowserDialog();
            this.LabelExportTo = new System.Windows.Forms.Label();
            this.textBoxExportTo = new System.Windows.Forms.TextBox();
            this.buttonOpenInventory = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this._openFileDialogSelectInventory = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // TextBoxBT2Path
            // 
            this.TextBoxBT2Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxBT2Path.Location = new System.Drawing.Point(122, 7);
            this.TextBoxBT2Path.Name = "TextBoxBT2Path";
            this.TextBoxBT2Path.ReadOnly = true;
            this.TextBoxBT2Path.Size = new System.Drawing.Size(387, 20);
            this.TextBoxBT2Path.TabIndex = 3;
            this.TextBoxBT2Path.Text = global::CBAResultsExporter.Properties.Settings.Default.prevResultsFolder;
            // 
            // buttonChooseImportFolder
            // 
            this.buttonChooseImportFolder.Location = new System.Drawing.Point(0, 4);
            this.buttonChooseImportFolder.Name = "buttonChooseImportFolder";
            this.buttonChooseImportFolder.Size = new System.Drawing.Size(92, 23);
            this.buttonChooseImportFolder.TabIndex = 0;
            this.buttonChooseImportFolder.Text = "Choose folder...";
            this.buttonChooseImportFolder.UseVisualStyleBackColor = true;
            this.buttonChooseImportFolder.Click += new System.EventHandler(this.buttonChooseImportFolder_Click);
            // 
            // LabelImportXML
            // 
            this.LabelImportXML.AutoSize = true;
            this.LabelImportXML.ForeColor = System.Drawing.SystemColors.InfoText;
            this.LabelImportXML.Location = new System.Drawing.Point(12, 9);
            this.LabelImportXML.Name = "LabelImportXML";
            this.LabelImportXML.Size = new System.Drawing.Size(110, 13);
            this.LabelImportXML.TabIndex = 2;
            this.LabelImportXML.Text = "Open Battery Results:";
            // 
            // folderBrowserDialogChooseResults
            // 
            this.folderBrowserDialogChooseResults.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialogChooseResults.ShowNewFolderButton = false;
            // 
            // LabelExportTo
            // 
            this.LabelExportTo.AutoSize = true;
            this.LabelExportTo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LabelExportTo.Location = new System.Drawing.Point(12, 39);
            this.LabelExportTo.Name = "LabelExportTo";
            this.LabelExportTo.Size = new System.Drawing.Size(106, 13);
            this.LabelExportTo.TabIndex = 3;
            this.LabelExportTo.Text = "Select Inventory File:";
            // 
            // textBoxExportTo
            // 
            this.textBoxExportTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExportTo.Location = new System.Drawing.Point(122, 36);
            this.textBoxExportTo.Name = "textBoxExportTo";
            this.textBoxExportTo.ReadOnly = true;
            this.textBoxExportTo.Size = new System.Drawing.Size(387, 20);
            this.textBoxExportTo.TabIndex = 4;
            this.textBoxExportTo.Text = global::CBAResultsExporter.Properties.Settings.Default.prevInventoryFile;
            // 
            // buttonOpenInventory
            // 
            this.buttonOpenInventory.Location = new System.Drawing.Point(0, 34);
            this.buttonOpenInventory.Name = "buttonOpenInventory";
            this.buttonOpenInventory.Size = new System.Drawing.Size(92, 23);
            this.buttonOpenInventory.TabIndex = 1;
            this.buttonOpenInventory.Text = "Open...";
            this.buttonOpenInventory.UseVisualStyleBackColor = true;
            this.buttonOpenInventory.Click += new System.EventHandler(this.buttonOpenInventory_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(0, 63);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(92, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // _openFileDialogSelectInventory
            // 
            this._openFileDialogSelectInventory.DefaultExt = "xlsx";
            this._openFileDialogSelectInventory.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 109);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(615, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonChooseImportFolder);
            this.panel1.Controls.Add(this.buttonOpenInventory);
            this.panel1.Controls.Add(this.buttonExport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(515, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 109);
            this.panel1.TabIndex = 6;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel Files (*.xlsx;)|*.xlsx";
            this.saveFileDialog1.OverwritePrompt = false;
            this.saveFileDialog1.Title = "Create or select the Excel file to append to";
            // 
            // CBAExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 131);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxExportTo);
            this.Controls.Add(this.LabelExportTo);
            this.Controls.Add(this.LabelImportXML);
            this.Controls.Add(this.TextBoxBT2Path);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2560, 170);
            this.MinimumSize = new System.Drawing.Size(498, 170);
            this.Name = "CBAExporter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CBA Results Exporter";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button buttonChooseImportFolder;
        private System.Windows.Forms.TextBox TextBoxBT2Path;
        private System.Windows.Forms.Label LabelImportXML;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogChooseResults;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonOpenInventory;
        private System.Windows.Forms.TextBox textBoxExportTo;
        private System.Windows.Forms.Label LabelExportTo;
        private System.Windows.Forms.OpenFileDialog _openFileDialogSelectInventory;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

