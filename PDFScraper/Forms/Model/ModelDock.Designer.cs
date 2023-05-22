namespace PDFScraper.Forms.Model {
    partial class ModelDock {
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
            this.dgvExtractors = new System.Windows.Forms.DataGridView();
            this.dgvExtractorsNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExtractorsTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExtractorsRegionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTemplatePDFPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectTemplatePDF = new System.Windows.Forms.Button();
            this.tlpModel = new System.Windows.Forms.TableLayoutPanel();
            this.tlpFilterButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddExtractor = new System.Windows.Forms.Button();
            this.mnuAddExtractor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableExtractorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditExtractor = new System.Windows.Forms.Button();
            this.btnDeleteExtractor = new System.Windows.Forms.Button();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tlpReview = new System.Windows.Forms.TableLayoutPanel();
            this.dgvExtractReview = new System.Windows.Forms.DataGridView();
            this.dlgOpenPDF = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractors)).BeginInit();
            this.tlpModel.SuspendLayout();
            this.tlpFilterButtons.SuspendLayout();
            this.mnuAddExtractor.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tlpReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractReview)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExtractors
            // 
            this.dgvExtractors.AllowUserToAddRows = false;
            this.dgvExtractors.AllowUserToDeleteRows = false;
            this.dgvExtractors.AllowUserToResizeColumns = false;
            this.dgvExtractors.AllowUserToResizeRows = false;
            this.dgvExtractors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExtractors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtractors.ColumnHeadersVisible = false;
            this.dgvExtractors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvExtractorsNameColumn,
            this.dgvExtractorsTypeColumn,
            this.dgvExtractorsRegionColumn});
            this.dgvExtractors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtractors.Location = new System.Drawing.Point(3, 55);
            this.dgvExtractors.MultiSelect = false;
            this.dgvExtractors.Name = "dgvExtractors";
            this.dgvExtractors.ReadOnly = true;
            this.dgvExtractors.RowHeadersVisible = false;
            this.dgvExtractors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExtractors.Size = new System.Drawing.Size(626, 285);
            this.dgvExtractors.TabIndex = 1;
            this.dgvExtractors.DoubleClick += new System.EventHandler(this.dgvExtractors_DoubleClick);
            // 
            // dgvExtractorsNameColumn
            // 
            this.dgvExtractorsNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvExtractorsNameColumn.DataPropertyName = "Extractor";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvExtractorsNameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExtractorsNameColumn.HeaderText = "Extractor";
            this.dgvExtractorsNameColumn.Name = "dgvExtractorsNameColumn";
            this.dgvExtractorsNameColumn.ReadOnly = true;
            this.dgvExtractorsNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgvExtractorsTypeColumn
            // 
            this.dgvExtractorsTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvExtractorsTypeColumn.DataPropertyName = "Type";
            this.dgvExtractorsTypeColumn.HeaderText = "Type";
            this.dgvExtractorsTypeColumn.Name = "dgvExtractorsTypeColumn";
            this.dgvExtractorsTypeColumn.ReadOnly = true;
            this.dgvExtractorsTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtractorsTypeColumn.Width = 50;
            // 
            // dgvExtractorsRegionColumn
            // 
            this.dgvExtractorsRegionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvExtractorsRegionColumn.DataPropertyName = "Region";
            this.dgvExtractorsRegionColumn.HeaderText = "Region";
            this.dgvExtractorsRegionColumn.Name = "dgvExtractorsRegionColumn";
            this.dgvExtractorsRegionColumn.ReadOnly = true;
            this.dgvExtractorsRegionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtractorsRegionColumn.Width = 200;
            // 
            // txtTemplatePDFPath
            // 
            this.txtTemplatePDFPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplatePDFPath.Location = new System.Drawing.Point(103, 3);
            this.txtTemplatePDFPath.Name = "txtTemplatePDFPath";
            this.txtTemplatePDFPath.ReadOnly = true;
            this.txtTemplatePDFPath.Size = new System.Drawing.Size(440, 20);
            this.txtTemplatePDFPath.TabIndex = 2;
            this.txtTemplatePDFPath.TextChanged += new System.EventHandler(this.txtTemplatePDFPath_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Template PDF:";
            // 
            // btnSelectTemplatePDF
            // 
            this.btnSelectTemplatePDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectTemplatePDF.Location = new System.Drawing.Point(549, 2);
            this.btnSelectTemplatePDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.btnSelectTemplatePDF.Name = "btnSelectTemplatePDF";
            this.btnSelectTemplatePDF.Size = new System.Drawing.Size(74, 21);
            this.btnSelectTemplatePDF.TabIndex = 4;
            this.btnSelectTemplatePDF.Text = "Select PDF";
            this.btnSelectTemplatePDF.UseVisualStyleBackColor = true;
            this.btnSelectTemplatePDF.Click += new System.EventHandler(this.btnSelectTemplatePDF_Click);
            // 
            // tlpModel
            // 
            this.tlpModel.ColumnCount = 1;
            this.tlpModel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpModel.Controls.Add(this.tlpFilterButtons, 0, 4);
            this.tlpModel.Controls.Add(this.dgvExtractors, 0, 3);
            this.tlpModel.Controls.Add(this.tlpTop, 0, 0);
            this.tlpModel.Controls.Add(this.label2, 0, 2);
            this.tlpModel.Controls.Add(this.label3, 0, 1);
            this.tlpModel.Controls.Add(this.label4, 0, 5);
            this.tlpModel.Controls.Add(this.label5, 0, 6);
            this.tlpModel.Controls.Add(this.tlpReview, 0, 7);
            this.tlpModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpModel.Location = new System.Drawing.Point(0, 0);
            this.tlpModel.Name = "tlpModel";
            this.tlpModel.RowCount = 9;
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpModel.Size = new System.Drawing.Size(632, 702);
            this.tlpModel.TabIndex = 5;
            // 
            // tlpFilterButtons
            // 
            this.tlpFilterButtons.ColumnCount = 4;
            this.tlpFilterButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilterButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpFilterButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpFilterButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpFilterButtons.Controls.Add(this.btnAddExtractor, 1, 0);
            this.tlpFilterButtons.Controls.Add(this.btnEditExtractor, 2, 0);
            this.tlpFilterButtons.Controls.Add(this.btnDeleteExtractor, 3, 0);
            this.tlpFilterButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilterButtons.Location = new System.Drawing.Point(3, 343);
            this.tlpFilterButtons.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpFilterButtons.Name = "tlpFilterButtons";
            this.tlpFilterButtons.RowCount = 1;
            this.tlpFilterButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilterButtons.Size = new System.Drawing.Size(626, 24);
            this.tlpFilterButtons.TabIndex = 6;
            // 
            // btnAddExtractor
            // 
            this.btnAddExtractor.ContextMenuStrip = this.mnuAddExtractor;
            this.btnAddExtractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddExtractor.Location = new System.Drawing.Point(389, 0);
            this.btnAddExtractor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnAddExtractor.Name = "btnAddExtractor";
            this.btnAddExtractor.Size = new System.Drawing.Size(74, 21);
            this.btnAddExtractor.TabIndex = 0;
            this.btnAddExtractor.Text = "Add";
            this.btnAddExtractor.UseVisualStyleBackColor = true;
            this.btnAddExtractor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddExtractor_MouseDown);
            // 
            // mnuAddExtractor
            // 
            this.mnuAddExtractor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableExtractorToolStripMenuItem});
            this.mnuAddExtractor.Name = "mnuAddExtractor";
            this.mnuAddExtractor.Size = new System.Drawing.Size(152, 26);
            this.mnuAddExtractor.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuAddExtractor_ItemClicked);
            // 
            // tableExtractorToolStripMenuItem
            // 
            this.tableExtractorToolStripMenuItem.Name = "tableExtractorToolStripMenuItem";
            this.tableExtractorToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tableExtractorToolStripMenuItem.Text = "Table Extractor";
            // 
            // btnEditExtractor
            // 
            this.btnEditExtractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditExtractor.Location = new System.Drawing.Point(469, 0);
            this.btnEditExtractor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnEditExtractor.Name = "btnEditExtractor";
            this.btnEditExtractor.Size = new System.Drawing.Size(74, 21);
            this.btnEditExtractor.TabIndex = 1;
            this.btnEditExtractor.Text = "Edit";
            this.btnEditExtractor.UseVisualStyleBackColor = true;
            this.btnEditExtractor.Click += new System.EventHandler(this.btnEditExtractor_Click);
            // 
            // btnDeleteExtractor
            // 
            this.btnDeleteExtractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteExtractor.Location = new System.Drawing.Point(549, 0);
            this.btnDeleteExtractor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnDeleteExtractor.Name = "btnDeleteExtractor";
            this.btnDeleteExtractor.Size = new System.Drawing.Size(74, 21);
            this.btnDeleteExtractor.TabIndex = 2;
            this.btnDeleteExtractor.Text = "Delete";
            this.btnDeleteExtractor.UseVisualStyleBackColor = true;
            this.btnDeleteExtractor.Click += new System.EventHandler(this.btnDeleteExtractor_Click);
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 3;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTop.Controls.Add(this.label1, 0, 0);
            this.tlpTop.Controls.Add(this.btnSelectTemplatePDF, 2, 0);
            this.tlpTop.Controls.Add(this.txtTemplatePDFPath, 1, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(3, 3);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(626, 26);
            this.tlpTop.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Extractors";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(626, 2);
            this.label3.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(626, 2);
            this.label4.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Review";
            // 
            // tlpReview
            // 
            this.tlpReview.ColumnCount = 1;
            this.tlpReview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpReview.Controls.Add(this.dgvExtractReview, 0, 0);
            this.tlpReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpReview.Location = new System.Drawing.Point(3, 393);
            this.tlpReview.Name = "tlpReview";
            this.tlpReview.RowCount = 1;
            this.tlpReview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReview.Size = new System.Drawing.Size(626, 285);
            this.tlpReview.TabIndex = 8;
            // 
            // dgvExtractReview
            // 
            this.dgvExtractReview.AllowUserToAddRows = false;
            this.dgvExtractReview.AllowUserToDeleteRows = false;
            this.dgvExtractReview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExtractReview.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExtractReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtractReview.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExtractReview.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExtractReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtractReview.Location = new System.Drawing.Point(3, 3);
            this.dgvExtractReview.Name = "dgvExtractReview";
            this.dgvExtractReview.ReadOnly = true;
            this.dgvExtractReview.RowHeadersVisible = false;
            this.dgvExtractReview.Size = new System.Drawing.Size(620, 279);
            this.dgvExtractReview.TabIndex = 1;
            this.dgvExtractReview.SelectionChanged += new System.EventHandler(this.dgvExtractReview_SelectionChanged);
            // 
            // dlgOpenPDF
            // 
            this.dlgOpenPDF.Filter = "PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // ModelDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 702);
            this.Controls.Add(this.tlpModel);
            this.Name = "ModelDock";
            this.Text = "Model";
            this.DockStateChanged += new System.EventHandler(this.ModelDock_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelDock_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModelDock_FormClosed);
            this.Shown += new System.EventHandler(this.ModelDock_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractors)).EndInit();
            this.tlpModel.ResumeLayout(false);
            this.tlpModel.PerformLayout();
            this.tlpFilterButtons.ResumeLayout(false);
            this.mnuAddExtractor.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tlpReview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractReview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExtractors;
        private System.Windows.Forms.TextBox txtTemplatePDFPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectTemplatePDF;
        private System.Windows.Forms.TableLayoutPanel tlpModel;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tlpFilterButtons;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddExtractor;
        private System.Windows.Forms.Button btnEditExtractor;
        private System.Windows.Forms.Button btnDeleteExtractor;
        private System.Windows.Forms.TableLayoutPanel tlpReview;
        private System.Windows.Forms.OpenFileDialog dlgOpenPDF;
        private System.Windows.Forms.ContextMenuStrip mnuAddExtractor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvExtractorsNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvExtractorsTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvExtractorsRegionColumn;
        private System.Windows.Forms.ToolStripMenuItem tableExtractorToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvExtractReview;
    }
}