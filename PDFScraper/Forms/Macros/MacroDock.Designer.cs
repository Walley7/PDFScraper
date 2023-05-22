namespace PDFScraper.Forms.Macros {
    partial class MacroDock {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMacro = new System.Windows.Forms.DataGridView();
            this.dgvMacroEventColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMacroTimeSpanColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMacroParametersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMacro = new System.Windows.Forms.TableLayoutPanel();
            this.tlvMacro = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlpSampleModel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectSamplePDF = new System.Windows.Forms.Button();
            this.txtSamplePDFPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectSampleModel = new System.Windows.Forms.Button();
            this.txtSampleModelPath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTestMode = new System.Windows.Forms.Button();
            this.btnRecordMode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dlgOpenModel = new System.Windows.Forms.OpenFileDialog();
            this.dlgOpenPDF = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMacro)).BeginInit();
            this.tlpMacro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvMacro)).BeginInit();
            this.tlpSampleModel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMacro
            // 
            this.dgvMacro.AllowUserToAddRows = false;
            this.dgvMacro.AllowUserToDeleteRows = false;
            this.dgvMacro.AllowUserToResizeRows = false;
            this.dgvMacro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMacro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMacro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvMacroEventColumn,
            this.dgvMacroTimeSpanColumn,
            this.dgvMacroParametersColumn});
            this.dgvMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMacro.Location = new System.Drawing.Point(3, 86);
            this.dgvMacro.MultiSelect = false;
            this.dgvMacro.Name = "dgvMacro";
            this.dgvMacro.ReadOnly = true;
            this.dgvMacro.RowHeadersVisible = false;
            this.dgvMacro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMacro.Size = new System.Drawing.Size(794, 177);
            this.dgvMacro.TabIndex = 2;
            // 
            // dgvMacroEventColumn
            // 
            this.dgvMacroEventColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvMacroEventColumn.DataPropertyName = "Event";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMacroEventColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMacroEventColumn.FillWeight = 50F;
            this.dgvMacroEventColumn.HeaderText = "Event";
            this.dgvMacroEventColumn.Name = "dgvMacroEventColumn";
            this.dgvMacroEventColumn.ReadOnly = true;
            // 
            // dgvMacroTimeSpanColumn
            // 
            this.dgvMacroTimeSpanColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvMacroTimeSpanColumn.DataPropertyName = "TimeSpan";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvMacroTimeSpanColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMacroTimeSpanColumn.FillWeight = 25F;
            this.dgvMacroTimeSpanColumn.HeaderText = "Time Span";
            this.dgvMacroTimeSpanColumn.Name = "dgvMacroTimeSpanColumn";
            this.dgvMacroTimeSpanColumn.ReadOnly = true;
            // 
            // dgvMacroParametersColumn
            // 
            this.dgvMacroParametersColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvMacroParametersColumn.DataPropertyName = "Parameters";
            this.dgvMacroParametersColumn.FillWeight = 75F;
            this.dgvMacroParametersColumn.HeaderText = "Parameters";
            this.dgvMacroParametersColumn.Name = "dgvMacroParametersColumn";
            this.dgvMacroParametersColumn.ReadOnly = true;
            // 
            // tlpMacro
            // 
            this.tlpMacro.ColumnCount = 1;
            this.tlpMacro.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMacro.Controls.Add(this.tlvMacro, 0, 3);
            this.tlpMacro.Controls.Add(this.dgvMacro, 0, 2);
            this.tlpMacro.Controls.Add(this.tlpSampleModel, 0, 1);
            this.tlpMacro.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMacro.Location = new System.Drawing.Point(0, 0);
            this.tlpMacro.Name = "tlpMacro";
            this.tlpMacro.RowCount = 4;
            this.tlpMacro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMacro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tlpMacro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMacro.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMacro.Size = new System.Drawing.Size(800, 450);
            this.tlpMacro.TabIndex = 5;
            // 
            // tlvMacro
            // 
            this.tlvMacro.AllColumns.Add(this.olvColumn1);
            this.tlvMacro.AllColumns.Add(this.olvColumn2);
            this.tlvMacro.AllColumns.Add(this.olvColumn3);
            this.tlvMacro.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tlvMacro.CellEditUseWholeCell = false;
            this.tlvMacro.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.tlvMacro.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvMacro.FullRowSelect = true;
            this.tlvMacro.Location = new System.Drawing.Point(3, 269);
            this.tlvMacro.Name = "tlvMacro";
            this.tlvMacro.ShowGroups = false;
            this.tlvMacro.Size = new System.Drawing.Size(794, 178);
            this.tlvMacro.TabIndex = 17;
            this.tlvMacro.UseCellFormatEvents = true;
            this.tlvMacro.UseCompatibleStateImageBehavior = false;
            this.tlvMacro.View = System.Windows.Forms.View.Details;
            this.tlvMacro.VirtualMode = true;
            this.tlvMacro.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.tlvMacro_FormatCell);
            this.tlvMacro.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.tlvMacro_FormatRow);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Description";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.MaximumWidth = 360;
            this.olvColumn1.Text = "Event";
            this.olvColumn1.Width = 240;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "TimeSpanDescription";
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.olvColumn2.Text = "Time Span";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn2.Width = 80;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "ParametersDescription";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Text = "Parameters";
            this.olvColumn3.Width = 120;
            // 
            // tlpSampleModel
            // 
            this.tlpSampleModel.ColumnCount = 3;
            this.tlpSampleModel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSampleModel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSampleModel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSampleModel.Controls.Add(this.btnSelectSamplePDF, 2, 1);
            this.tlpSampleModel.Controls.Add(this.txtSamplePDFPath, 1, 1);
            this.tlpSampleModel.Controls.Add(this.label3, 0, 1);
            this.tlpSampleModel.Controls.Add(this.label1, 0, 0);
            this.tlpSampleModel.Controls.Add(this.btnSelectSampleModel, 2, 0);
            this.tlpSampleModel.Controls.Add(this.txtSampleModelPath, 1, 0);
            this.tlpSampleModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSampleModel.Location = new System.Drawing.Point(3, 28);
            this.tlpSampleModel.Name = "tlpSampleModel";
            this.tlpSampleModel.RowCount = 2;
            this.tlpSampleModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSampleModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSampleModel.Size = new System.Drawing.Size(794, 52);
            this.tlpSampleModel.TabIndex = 4;
            // 
            // btnSelectSamplePDF
            // 
            this.btnSelectSamplePDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectSamplePDF.Location = new System.Drawing.Point(697, 28);
            this.btnSelectSamplePDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.btnSelectSamplePDF.Name = "btnSelectSamplePDF";
            this.btnSelectSamplePDF.Size = new System.Drawing.Size(94, 21);
            this.btnSelectSamplePDF.TabIndex = 7;
            this.btnSelectSamplePDF.Text = "Select PDF";
            this.btnSelectSamplePDF.UseVisualStyleBackColor = true;
            this.btnSelectSamplePDF.Click += new System.EventHandler(this.btnSelectSamplePDF_Click);
            // 
            // txtSamplePDFPath
            // 
            this.txtSamplePDFPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSamplePDFPath.Location = new System.Drawing.Point(103, 29);
            this.txtSamplePDFPath.Name = "txtSamplePDFPath";
            this.txtSamplePDFPath.ReadOnly = true;
            this.txtSamplePDFPath.Size = new System.Drawing.Size(588, 20);
            this.txtSamplePDFPath.TabIndex = 6;
            this.txtSamplePDFPath.TextChanged += new System.EventHandler(this.txtSamplePDFPath_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sample PDF:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sample Model:";
            // 
            // btnSelectSampleModel
            // 
            this.btnSelectSampleModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectSampleModel.Location = new System.Drawing.Point(697, 2);
            this.btnSelectSampleModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.btnSelectSampleModel.Name = "btnSelectSampleModel";
            this.btnSelectSampleModel.Size = new System.Drawing.Size(94, 21);
            this.btnSelectSampleModel.TabIndex = 4;
            this.btnSelectSampleModel.Text = "Select Model";
            this.btnSelectSampleModel.UseVisualStyleBackColor = true;
            this.btnSelectSampleModel.Click += new System.EventHandler(this.btnSelectSampleModel_Click);
            // 
            // txtSampleModelPath
            // 
            this.txtSampleModelPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSampleModelPath.Location = new System.Drawing.Point(103, 3);
            this.txtSampleModelPath.Name = "txtSampleModelPath";
            this.txtSampleModelPath.ReadOnly = true;
            this.txtSampleModelPath.Size = new System.Drawing.Size(588, 20);
            this.txtSampleModelPath.TabIndex = 2;
            this.txtSampleModelPath.TextChanged += new System.EventHandler(this.txtSampleModelPath_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnTestMode, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRecordMode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 25);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnTestMode
            // 
            this.btnTestMode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnTestMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestMode.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnTestMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestMode.Location = new System.Drawing.Point(145, 0);
            this.btnTestMode.Margin = new System.Windows.Forms.Padding(0);
            this.btnTestMode.Name = "btnTestMode";
            this.btnTestMode.Size = new System.Drawing.Size(100, 25);
            this.btnTestMode.TabIndex = 3;
            this.btnTestMode.Text = "Test";
            this.btnTestMode.UseVisualStyleBackColor = false;
            this.btnTestMode.Click += new System.EventHandler(this.btnTestMode_Click);
            // 
            // btnRecordMode
            // 
            this.btnRecordMode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRecordMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecordMode.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnRecordMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecordMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecordMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecordMode.Location = new System.Drawing.Point(45, 0);
            this.btnRecordMode.Margin = new System.Windows.Forms.Padding(0);
            this.btnRecordMode.Name = "btnRecordMode";
            this.btnRecordMode.Size = new System.Drawing.Size(100, 25);
            this.btnRecordMode.TabIndex = 2;
            this.btnRecordMode.Text = "Record";
            this.btnRecordMode.UseVisualStyleBackColor = false;
            this.btnRecordMode.Click += new System.EventHandler(this.btnRecordMode_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mode";
            // 
            // dlgOpenModel
            // 
            this.dlgOpenModel.Filter = "Model Files (*.psmdl|*.psmdl|All files (*.*)|*.*";
            // 
            // dlgOpenPDF
            // 
            this.dlgOpenPDF.Filter = "PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // MacroDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpMacro);
            this.Name = "MacroDock";
            this.Text = " ";
            this.DockStateChanged += new System.EventHandler(this.MacroDock_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MacroDock_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MacroDock_FormClosed);
            this.Shown += new System.EventHandler(this.MacroDock_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMacro)).EndInit();
            this.tlpMacro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvMacro)).EndInit();
            this.tlpSampleModel.ResumeLayout(false);
            this.tlpSampleModel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMacro;
        private System.Windows.Forms.TableLayoutPanel tlpMacro;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMacroEventColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMacroTimeSpanColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMacroParametersColumn;
        private System.Windows.Forms.TableLayoutPanel tlpSampleModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectSampleModel;
        private System.Windows.Forms.TextBox txtSampleModelPath;
        private System.Windows.Forms.OpenFileDialog dlgOpenModel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnRecordMode;
        private System.Windows.Forms.Button btnTestMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectSamplePDF;
        private System.Windows.Forms.TextBox txtSamplePDFPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog dlgOpenPDF;
        private BrightIdeasSoftware.TreeListView tlvMacro;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
    }
}