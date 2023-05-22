namespace PDFScraper.Forms.Macros {
    partial class MacroRecordingBeginBranchingForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.numColumnPosition = new System.Windows.Forms.NumericUpDown();
            this.cboColumnRelativeTo = new System.Windows.Forms.ComboBox();
            this.lblColumnPosition = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cboSource = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboExtractor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkColumnByHeader = new System.Windows.Forms.CheckBox();
            this.cboColumnHeader = new System.Windows.Forms.ComboBox();
            this.lblColumnHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numColumnPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // numColumnPosition
            // 
            this.numColumnPosition.Location = new System.Drawing.Point(191, 115);
            this.numColumnPosition.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numColumnPosition.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumnPosition.Name = "numColumnPosition";
            this.numColumnPosition.Size = new System.Drawing.Size(70, 20);
            this.numColumnPosition.TabIndex = 9;
            this.numColumnPosition.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cboColumnRelativeTo
            // 
            this.cboColumnRelativeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumnRelativeTo.FormattingEnabled = true;
            this.cboColumnRelativeTo.Items.AddRange(new object[] {
            "Start",
            "End"});
            this.cboColumnRelativeTo.Location = new System.Drawing.Point(115, 114);
            this.cboColumnRelativeTo.Name = "cboColumnRelativeTo";
            this.cboColumnRelativeTo.Size = new System.Drawing.Size(70, 21);
            this.cboColumnRelativeTo.TabIndex = 8;
            // 
            // lblColumnPosition
            // 
            this.lblColumnPosition.AutoSize = true;
            this.lblColumnPosition.Location = new System.Drawing.Point(10, 117);
            this.lblColumnPosition.Name = "lblColumnPosition";
            this.lblColumnPosition.Size = new System.Drawing.Size(85, 13);
            this.lblColumnPosition.TabIndex = 7;
            this.lblColumnPosition.Text = "Column Position:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(291, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(210, 141);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cboSource
            // 
            this.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSource.FormattingEnabled = true;
            this.cboSource.Items.AddRange(new object[] {
            "Extractor"});
            this.cboSource.Location = new System.Drawing.Point(115, 10);
            this.cboSource.Name = "cboSource";
            this.cboSource.Size = new System.Drawing.Size(250, 21);
            this.cboSource.TabIndex = 16;
            this.cboSource.SelectedIndexChanged += new System.EventHandler(this.cboSource_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Source:";
            // 
            // cboExtractor
            // 
            this.cboExtractor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboExtractor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboExtractor.Enabled = false;
            this.cboExtractor.FormattingEnabled = true;
            this.cboExtractor.Location = new System.Drawing.Point(115, 37);
            this.cboExtractor.Name = "cboExtractor";
            this.cboExtractor.Size = new System.Drawing.Size(250, 21);
            this.cboExtractor.TabIndex = 18;
            this.cboExtractor.TextChanged += new System.EventHandler(this.cboExtractor_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Extractor:";
            // 
            // chkColumnByHeader
            // 
            this.chkColumnByHeader.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkColumnByHeader.Location = new System.Drawing.Point(9, 64);
            this.chkColumnByHeader.Name = "chkColumnByHeader";
            this.chkColumnByHeader.Size = new System.Drawing.Size(120, 17);
            this.chkColumnByHeader.TabIndex = 22;
            this.chkColumnByHeader.Text = "Column By Header:";
            this.chkColumnByHeader.UseVisualStyleBackColor = true;
            this.chkColumnByHeader.CheckedChanged += new System.EventHandler(this.chkColumnByHeader_CheckedChanged);
            // 
            // cboColumnHeader
            // 
            this.cboColumnHeader.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboColumnHeader.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboColumnHeader.Enabled = false;
            this.cboColumnHeader.FormattingEnabled = true;
            this.cboColumnHeader.Location = new System.Drawing.Point(115, 87);
            this.cboColumnHeader.Name = "cboColumnHeader";
            this.cboColumnHeader.Size = new System.Drawing.Size(250, 21);
            this.cboColumnHeader.TabIndex = 24;
            // 
            // lblColumnHeader
            // 
            this.lblColumnHeader.AutoSize = true;
            this.lblColumnHeader.Enabled = false;
            this.lblColumnHeader.Location = new System.Drawing.Point(10, 90);
            this.lblColumnHeader.Name = "lblColumnHeader";
            this.lblColumnHeader.Size = new System.Drawing.Size(83, 13);
            this.lblColumnHeader.TabIndex = 23;
            this.lblColumnHeader.Text = "Column Header:";
            // 
            // MacroRecordingBeginBranchingForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(375, 173);
            this.ControlBox = false;
            this.Controls.Add(this.cboColumnHeader);
            this.Controls.Add(this.lblColumnHeader);
            this.Controls.Add(this.chkColumnByHeader);
            this.Controls.Add(this.cboExtractor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSource);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.numColumnPosition);
            this.Controls.Add(this.cboColumnRelativeTo);
            this.Controls.Add(this.lblColumnPosition);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MacroRecordingBeginBranchingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Macro - Recording - Begin Branching";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.MacroRecordingBeginBranchingForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numColumnPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numColumnPosition;
        private System.Windows.Forms.ComboBox cboColumnRelativeTo;
        private System.Windows.Forms.Label lblColumnPosition;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cboSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboExtractor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkColumnByHeader;
        private System.Windows.Forms.ComboBox cboColumnHeader;
        private System.Windows.Forms.Label lblColumnHeader;
    }
}