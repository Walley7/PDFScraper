namespace PDFScraper.Forms.Model {
    partial class ExtractorForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectRegion = new System.Windows.Forms.Button();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.tlpFilters = new System.Windows.Forms.TableLayoutPanel();
            this.dgvExtract0 = new System.Windows.Forms.DataGridView();
            this.btnAddFilterEnd = new System.Windows.Forms.Button();
            this.dgvExtractHeaders = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSplitLinesIntoRows = new System.Windows.Forms.CheckBox();
            this.mnuAddFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddFilterColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_InsertColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_KeepColumnRange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_RemoveColumnRange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_RemoveEmptyColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_MoveColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_MergeColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_SplitColumnAtText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_SplitColumnAtWhitespace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_SplitColumnAtLineBreaks = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_SplitColumnAfterNWords = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterColumns_SplitColumnAfterNCharacters = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_KeepRowRange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_KeepRowsBetween = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_KeepRowsWhere = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_RemoveRowRange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_RemoveRowsBetween = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_RemoveRowsWhere = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterRows_MergeRows = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterCells = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterCells_AddSetText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterCells_CopyText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterCells_ReplaceText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterCells_ReformatNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFilterCells_ReformatDates = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpBase.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.tlpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtract0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractHeaders)).BeginInit();
            this.flpOptions.SuspendLayout();
            this.mnuAddFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type:";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(627, 838);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 20);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // tlpBase
            // 
            this.tlpBase.ColumnCount = 4;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpBase.Controls.Add(this.btnCancel, 3, 6);
            this.tlpBase.Controls.Add(this.label2, 0, 0);
            this.tlpBase.Controls.Add(this.label1, 0, 1);
            this.tlpBase.Controls.Add(this.btnSave, 2, 6);
            this.tlpBase.Controls.Add(this.txtName, 1, 1);
            this.tlpBase.Controls.Add(this.cboType, 1, 0);
            this.tlpBase.Controls.Add(this.label3, 0, 2);
            this.tlpBase.Controls.Add(this.btnSelectRegion, 3, 2);
            this.tlpBase.Controls.Add(this.txtRegion, 1, 2);
            this.tlpBase.Controls.Add(this.label4, 0, 4);
            this.tlpBase.Controls.Add(this.pnlFilters, 0, 5);
            this.tlpBase.Controls.Add(this.label5, 0, 3);
            this.tlpBase.Controls.Add(this.flpOptions, 1, 3);
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(0, 0);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 7;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpBase.Size = new System.Drawing.Size(784, 861);
            this.tlpBase.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(707, 838);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 20);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.tlpBase.SetColumnSpan(this.txtName, 3);
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(103, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(678, 20);
            this.txtName.TabIndex = 5;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // cboType
            // 
            this.tlpBase.SetColumnSpan(this.cboType, 3);
            this.cboType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboType.Enabled = false;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "Text",
            "Table"});
            this.cboType.Location = new System.Drawing.Point(103, 3);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(678, 21);
            this.cboType.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Region:";
            // 
            // btnSelectRegion
            // 
            this.btnSelectRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectRegion.Location = new System.Drawing.Point(707, 55);
            this.btnSelectRegion.Name = "btnSelectRegion";
            this.btnSelectRegion.Size = new System.Drawing.Size(74, 20);
            this.btnSelectRegion.TabIndex = 8;
            this.btnSelectRegion.Text = "Select";
            this.btnSelectRegion.UseVisualStyleBackColor = true;
            this.btnSelectRegion.Click += new System.EventHandler(this.btnSelectRegion_Click);
            // 
            // txtRegion
            // 
            this.tlpBase.SetColumnSpan(this.txtRegion, 2);
            this.txtRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegion.Location = new System.Drawing.Point(103, 55);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.ReadOnly = true;
            this.txtRegion.Size = new System.Drawing.Size(598, 20);
            this.txtRegion.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.tlpBase.SetColumnSpan(this.label4, 2);
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Output / Filters:";
            // 
            // pnlFilters
            // 
            this.pnlFilters.AutoScroll = true;
            this.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tlpBase.SetColumnSpan(this.pnlFilters, 4);
            this.pnlFilters.Controls.Add(this.tlpFilters);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilters.Location = new System.Drawing.Point(3, 127);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(778, 705);
            this.pnlFilters.TabIndex = 12;
            this.pnlFilters.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pnlFilters_Scroll);
            // 
            // tlpFilters
            // 
            this.tlpFilters.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tlpFilters.ColumnCount = 2;
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpFilters.Controls.Add(this.dgvExtract0, 0, 1);
            this.tlpFilters.Controls.Add(this.btnAddFilterEnd, 1, 2);
            this.tlpFilters.Controls.Add(this.dgvExtractHeaders, 0, 0);
            this.tlpFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpFilters.Location = new System.Drawing.Point(0, 0);
            this.tlpFilters.Name = "tlpFilters";
            this.tlpFilters.RowCount = 4;
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tlpFilters.Size = new System.Drawing.Size(774, 500);
            this.tlpFilters.TabIndex = 0;
            // 
            // dgvExtract0
            // 
            this.dgvExtract0.AllowUserToAddRows = false;
            this.dgvExtract0.AllowUserToDeleteRows = false;
            this.dgvExtract0.AllowUserToResizeRows = false;
            this.dgvExtract0.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExtract0.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExtract0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExtract0.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExtract0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtract0.Location = new System.Drawing.Point(6, 59);
            this.dgvExtract0.Name = "dgvExtract0";
            this.dgvExtract0.RowHeadersVisible = false;
            this.dgvExtract0.Size = new System.Drawing.Size(730, 294);
            this.dgvExtract0.TabIndex = 12;
            // 
            // btnAddFilterEnd
            // 
            this.btnAddFilterEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddFilterEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFilterEnd.Location = new System.Drawing.Point(745, 362);
            this.btnAddFilterEnd.Name = "btnAddFilterEnd";
            this.btnAddFilterEnd.Size = new System.Drawing.Size(23, 20);
            this.btnAddFilterEnd.TabIndex = 13;
            this.btnAddFilterEnd.Text = "+";
            this.btnAddFilterEnd.UseVisualStyleBackColor = true;
            this.btnAddFilterEnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddFilterEnd_MouseDown);
            // 
            // dgvExtractHeaders
            // 
            this.dgvExtractHeaders.AllowUserToAddRows = false;
            this.dgvExtractHeaders.AllowUserToDeleteRows = false;
            this.dgvExtractHeaders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExtractHeaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtractHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtractHeaders.Location = new System.Drawing.Point(6, 6);
            this.dgvExtractHeaders.Name = "dgvExtractHeaders";
            this.dgvExtractHeaders.RowHeadersVisible = false;
            this.dgvExtractHeaders.Size = new System.Drawing.Size(730, 44);
            this.dgvExtractHeaders.TabIndex = 14;
            this.dgvExtractHeaders.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtractHeaders_CellValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Options:";
            // 
            // flpOptions
            // 
            this.tlpBase.SetColumnSpan(this.flpOptions, 3);
            this.flpOptions.Controls.Add(this.chkSplitLinesIntoRows);
            this.flpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOptions.Location = new System.Drawing.Point(100, 78);
            this.flpOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(684, 26);
            this.flpOptions.TabIndex = 14;
            // 
            // chkSplitLinesIntoRows
            // 
            this.chkSplitLinesIntoRows.AutoSize = true;
            this.chkSplitLinesIntoRows.Location = new System.Drawing.Point(3, 5);
            this.chkSplitLinesIntoRows.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.chkSplitLinesIntoRows.Name = "chkSplitLinesIntoRows";
            this.chkSplitLinesIntoRows.Size = new System.Drawing.Size(125, 17);
            this.chkSplitLinesIntoRows.TabIndex = 0;
            this.chkSplitLinesIntoRows.Text = "Split Lines Into Rows";
            this.chkSplitLinesIntoRows.UseVisualStyleBackColor = true;
            this.chkSplitLinesIntoRows.CheckStateChanged += new System.EventHandler(this.chkSplitLinesIntoRows_CheckStateChanged);
            // 
            // mnuAddFilter
            // 
            this.mnuAddFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFilterColumns,
            this.mnuAddFilterRows,
            this.mnuAddFilterCells});
            this.mnuAddFilter.Name = "mnuAddFilter";
            this.mnuAddFilter.Size = new System.Drawing.Size(123, 70);
            // 
            // mnuAddFilterColumns
            // 
            this.mnuAddFilterColumns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFilterColumns_InsertColumns,
            this.mnuAddFilterColumns_KeepColumnRange,
            this.mnuAddFilterColumns_RemoveColumnRange,
            this.mnuAddFilterColumns_RemoveEmptyColumns,
            this.mnuAddFilterColumns_MoveColumn,
            this.mnuAddFilterColumns_MergeColumns,
            this.mnuAddFilterColumns_SplitColumnAtText,
            this.mnuAddFilterColumns_SplitColumnAtWhitespace,
            this.mnuAddFilterColumns_SplitColumnAtLineBreaks,
            this.mnuAddFilterColumns_SplitColumnAfterNWords,
            this.mnuAddFilterColumns_SplitColumnAfterNCharacters});
            this.mnuAddFilterColumns.Name = "mnuAddFilterColumns";
            this.mnuAddFilterColumns.Size = new System.Drawing.Size(122, 22);
            this.mnuAddFilterColumns.Text = "Columns";
            // 
            // mnuAddFilterColumns_InsertColumns
            // 
            this.mnuAddFilterColumns_InsertColumns.Name = "mnuAddFilterColumns_InsertColumns";
            this.mnuAddFilterColumns_InsertColumns.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_InsertColumns.Text = "Insert Columns";
            this.mnuAddFilterColumns_InsertColumns.Click += new System.EventHandler(this.mnuAddFilterColumns_InsertColumns_Click);
            // 
            // mnuAddFilterColumns_KeepColumnRange
            // 
            this.mnuAddFilterColumns_KeepColumnRange.Name = "mnuAddFilterColumns_KeepColumnRange";
            this.mnuAddFilterColumns_KeepColumnRange.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_KeepColumnRange.Text = "Keep Column Range";
            this.mnuAddFilterColumns_KeepColumnRange.Click += new System.EventHandler(this.mnuAddFilterColumns_KeepColumnRange_Click);
            // 
            // mnuAddFilterColumns_RemoveColumnRange
            // 
            this.mnuAddFilterColumns_RemoveColumnRange.Name = "mnuAddFilterColumns_RemoveColumnRange";
            this.mnuAddFilterColumns_RemoveColumnRange.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_RemoveColumnRange.Text = "Remove Column Range";
            this.mnuAddFilterColumns_RemoveColumnRange.Click += new System.EventHandler(this.mnuAddFilterColumns_RemoveColumnRange_Click);
            // 
            // mnuAddFilterColumns_RemoveEmptyColumns
            // 
            this.mnuAddFilterColumns_RemoveEmptyColumns.Name = "mnuAddFilterColumns_RemoveEmptyColumns";
            this.mnuAddFilterColumns_RemoveEmptyColumns.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_RemoveEmptyColumns.Text = "Remove Empty Columns";
            this.mnuAddFilterColumns_RemoveEmptyColumns.Click += new System.EventHandler(this.mnuAddFilterColumns_RemoveEmptyColumns_Click);
            // 
            // mnuAddFilterColumns_MoveColumn
            // 
            this.mnuAddFilterColumns_MoveColumn.Name = "mnuAddFilterColumns_MoveColumn";
            this.mnuAddFilterColumns_MoveColumn.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_MoveColumn.Text = "Move Column";
            this.mnuAddFilterColumns_MoveColumn.Click += new System.EventHandler(this.mnuAddFilterColumns_MoveColumn_Click);
            // 
            // mnuAddFilterColumns_MergeColumns
            // 
            this.mnuAddFilterColumns_MergeColumns.Name = "mnuAddFilterColumns_MergeColumns";
            this.mnuAddFilterColumns_MergeColumns.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_MergeColumns.Text = "Merge Columns";
            this.mnuAddFilterColumns_MergeColumns.Click += new System.EventHandler(this.mnuAddFilterColumns_MergeColumns_Click);
            // 
            // mnuAddFilterColumns_SplitColumnAtText
            // 
            this.mnuAddFilterColumns_SplitColumnAtText.Name = "mnuAddFilterColumns_SplitColumnAtText";
            this.mnuAddFilterColumns_SplitColumnAtText.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_SplitColumnAtText.Text = "Split Column At Text";
            this.mnuAddFilterColumns_SplitColumnAtText.Click += new System.EventHandler(this.mnuAddFilterColumns_SplitColumnAtText_Click);
            // 
            // mnuAddFilterColumns_SplitColumnAtWhitespace
            // 
            this.mnuAddFilterColumns_SplitColumnAtWhitespace.Name = "mnuAddFilterColumns_SplitColumnAtWhitespace";
            this.mnuAddFilterColumns_SplitColumnAtWhitespace.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_SplitColumnAtWhitespace.Text = "Split Column At Whitespace";
            this.mnuAddFilterColumns_SplitColumnAtWhitespace.Click += new System.EventHandler(this.mnuAddFilterColumns_SplitColumnAtWhitespace_Click);
            // 
            // mnuAddFilterColumns_SplitColumnAtLineBreaks
            // 
            this.mnuAddFilterColumns_SplitColumnAtLineBreaks.Name = "mnuAddFilterColumns_SplitColumnAtLineBreaks";
            this.mnuAddFilterColumns_SplitColumnAtLineBreaks.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_SplitColumnAtLineBreaks.Text = "Split Column At Line Breaks";
            this.mnuAddFilterColumns_SplitColumnAtLineBreaks.Click += new System.EventHandler(this.mnuAddFilterColumns_SplitColumnAtLineBreaks_Click);
            // 
            // mnuAddFilterColumns_SplitColumnAfterNWords
            // 
            this.mnuAddFilterColumns_SplitColumnAfterNWords.Name = "mnuAddFilterColumns_SplitColumnAfterNWords";
            this.mnuAddFilterColumns_SplitColumnAfterNWords.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_SplitColumnAfterNWords.Text = "Split Column After # Words";
            this.mnuAddFilterColumns_SplitColumnAfterNWords.Click += new System.EventHandler(this.mnuAddFilterColumns_SplitColumnAfterNWords_Click);
            // 
            // mnuAddFilterColumns_SplitColumnAfterNCharacters
            // 
            this.mnuAddFilterColumns_SplitColumnAfterNCharacters.Name = "mnuAddFilterColumns_SplitColumnAfterNCharacters";
            this.mnuAddFilterColumns_SplitColumnAfterNCharacters.Size = new System.Drawing.Size(241, 22);
            this.mnuAddFilterColumns_SplitColumnAfterNCharacters.Text = "Split Column After # Characters";
            this.mnuAddFilterColumns_SplitColumnAfterNCharacters.Click += new System.EventHandler(this.mnuAddFilterColumns_SplitColumnAfterNCharacters_Click);
            // 
            // mnuAddFilterRows
            // 
            this.mnuAddFilterRows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFilterRows_KeepRowRange,
            this.mnuAddFilterRows_KeepRowsBetween,
            this.mnuAddFilterRows_KeepRowsWhere,
            this.mnuAddFilterRows_RemoveRowRange,
            this.mnuAddFilterRows_RemoveRowsBetween,
            this.mnuAddFilterRows_RemoveRowsWhere,
            this.mnuAddFilterRows_MergeRows});
            this.mnuAddFilterRows.Name = "mnuAddFilterRows";
            this.mnuAddFilterRows.Size = new System.Drawing.Size(122, 22);
            this.mnuAddFilterRows.Text = "Rows";
            // 
            // mnuAddFilterRows_KeepRowRange
            // 
            this.mnuAddFilterRows_KeepRowRange.Name = "mnuAddFilterRows_KeepRowRange";
            this.mnuAddFilterRows_KeepRowRange.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_KeepRowRange.Text = "Keep Row Range";
            this.mnuAddFilterRows_KeepRowRange.Click += new System.EventHandler(this.mnuAddFilterRows_KeepRowRange_Click);
            // 
            // mnuAddFilterRows_KeepRowsBetween
            // 
            this.mnuAddFilterRows_KeepRowsBetween.Name = "mnuAddFilterRows_KeepRowsBetween";
            this.mnuAddFilterRows_KeepRowsBetween.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_KeepRowsBetween.Text = "Keep Rows Between";
            this.mnuAddFilterRows_KeepRowsBetween.Click += new System.EventHandler(this.mnuAddFilterRows_KeepRowsBetween_Click);
            // 
            // mnuAddFilterRows_KeepRowsWhere
            // 
            this.mnuAddFilterRows_KeepRowsWhere.Name = "mnuAddFilterRows_KeepRowsWhere";
            this.mnuAddFilterRows_KeepRowsWhere.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_KeepRowsWhere.Text = "Keep Rows Where";
            this.mnuAddFilterRows_KeepRowsWhere.Click += new System.EventHandler(this.mnuAddFilterRows_KeepRowsWhere_Click);
            // 
            // mnuAddFilterRows_RemoveRowRange
            // 
            this.mnuAddFilterRows_RemoveRowRange.Name = "mnuAddFilterRows_RemoveRowRange";
            this.mnuAddFilterRows_RemoveRowRange.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_RemoveRowRange.Text = "Remove Row Range";
            this.mnuAddFilterRows_RemoveRowRange.Click += new System.EventHandler(this.mnuAddFilterRows_RemoveRowRange_Click);
            // 
            // mnuAddFilterRows_RemoveRowsBetween
            // 
            this.mnuAddFilterRows_RemoveRowsBetween.Name = "mnuAddFilterRows_RemoveRowsBetween";
            this.mnuAddFilterRows_RemoveRowsBetween.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_RemoveRowsBetween.Text = "Remove Rows Between";
            this.mnuAddFilterRows_RemoveRowsBetween.Click += new System.EventHandler(this.mnuAddFilterRows_RemoveRowsBetween_Click);
            // 
            // mnuAddFilterRows_RemoveRowsWhere
            // 
            this.mnuAddFilterRows_RemoveRowsWhere.Name = "mnuAddFilterRows_RemoveRowsWhere";
            this.mnuAddFilterRows_RemoveRowsWhere.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_RemoveRowsWhere.Text = "Remove Rows Where";
            this.mnuAddFilterRows_RemoveRowsWhere.Click += new System.EventHandler(this.mnuAddFilterRows_RemoveRowsWhere_Click);
            // 
            // mnuAddFilterRows_MergeRows
            // 
            this.mnuAddFilterRows_MergeRows.Name = "mnuAddFilterRows_MergeRows";
            this.mnuAddFilterRows_MergeRows.Size = new System.Drawing.Size(196, 22);
            this.mnuAddFilterRows_MergeRows.Text = "Merge Rows";
            this.mnuAddFilterRows_MergeRows.Click += new System.EventHandler(this.mnuAddFilterRows_MergeRows_Click);
            // 
            // mnuAddFilterCells
            // 
            this.mnuAddFilterCells.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFilterCells_AddSetText,
            this.mnuAddFilterCells_CopyText,
            this.mnuAddFilterCells_ReplaceText,
            this.mnuAddFilterCells_ReformatNumbers,
            this.mnuAddFilterCells_ReformatDates});
            this.mnuAddFilterCells.Name = "mnuAddFilterCells";
            this.mnuAddFilterCells.Size = new System.Drawing.Size(122, 22);
            this.mnuAddFilterCells.Text = "Cells";
            // 
            // mnuAddFilterCells_AddSetText
            // 
            this.mnuAddFilterCells_AddSetText.Name = "mnuAddFilterCells_AddSetText";
            this.mnuAddFilterCells_AddSetText.Size = new System.Drawing.Size(175, 22);
            this.mnuAddFilterCells_AddSetText.Text = "Add / Set Text";
            this.mnuAddFilterCells_AddSetText.Click += new System.EventHandler(this.mnuAddFilterCells_AddSetText_Click);
            // 
            // mnuAddFilterCells_CopyText
            // 
            this.mnuAddFilterCells_CopyText.Name = "mnuAddFilterCells_CopyText";
            this.mnuAddFilterCells_CopyText.Size = new System.Drawing.Size(175, 22);
            this.mnuAddFilterCells_CopyText.Text = "Copy Text";
            this.mnuAddFilterCells_CopyText.Click += new System.EventHandler(this.mnuAddFilterCells_CopyText_Click);
            // 
            // mnuAddFilterCells_ReplaceText
            // 
            this.mnuAddFilterCells_ReplaceText.Name = "mnuAddFilterCells_ReplaceText";
            this.mnuAddFilterCells_ReplaceText.Size = new System.Drawing.Size(175, 22);
            this.mnuAddFilterCells_ReplaceText.Text = "Replace Text";
            this.mnuAddFilterCells_ReplaceText.Click += new System.EventHandler(this.mnuAddFilterCells_ReplaceText_Click);
            // 
            // mnuAddFilterCells_ReformatNumbers
            // 
            this.mnuAddFilterCells_ReformatNumbers.Name = "mnuAddFilterCells_ReformatNumbers";
            this.mnuAddFilterCells_ReformatNumbers.Size = new System.Drawing.Size(175, 22);
            this.mnuAddFilterCells_ReformatNumbers.Text = "Reformat Numbers";
            this.mnuAddFilterCells_ReformatNumbers.Click += new System.EventHandler(this.mnuAddFilterCells_ReformatNumbers_Click);
            // 
            // mnuAddFilterCells_ReformatDates
            // 
            this.mnuAddFilterCells_ReformatDates.Name = "mnuAddFilterCells_ReformatDates";
            this.mnuAddFilterCells_ReformatDates.Size = new System.Drawing.Size(175, 22);
            this.mnuAddFilterCells_ReformatDates.Text = "Reformat Dates";
            this.mnuAddFilterCells_ReformatDates.Click += new System.EventHandler(this.mnuAddFilterCells_ReformatDates_Click);
            // 
            // ExtractorForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 861);
            this.Controls.Add(this.tlpBase);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "ExtractorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extractor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtractorForm_FormClosing);
            this.Shown += new System.EventHandler(this.ExtractorForm_Shown);
            this.tlpBase.ResumeLayout(false);
            this.tlpBase.PerformLayout();
            this.pnlFilters.ResumeLayout(false);
            this.tlpFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtract0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractHeaders)).EndInit();
            this.flpOptions.ResumeLayout(false);
            this.flpOptions.PerformLayout();
            this.mnuAddFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectRegion;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvExtract0;
        private System.Windows.Forms.TableLayoutPanel tlpFilters;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnAddFilterEnd;
        private System.Windows.Forms.ContextMenuStrip mnuAddFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
        private System.Windows.Forms.CheckBox chkSplitLinesIntoRows;
        private System.Windows.Forms.DataGridView dgvExtractHeaders;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_InsertColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_KeepColumnRange;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_RemoveColumnRange;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_RemoveEmptyColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_MoveColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_MergeColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_SplitColumnAtWhitespace;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_SplitColumnAtLineBreaks;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_SplitColumnAfterNWords;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_SplitColumnAfterNCharacters;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterColumns_SplitColumnAtText;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterCells;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_KeepRowRange;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_RemoveRowRange;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_MergeRows;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_KeepRowsBetween;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_RemoveRowsBetween;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_KeepRowsWhere;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterRows_RemoveRowsWhere;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterCells_AddSetText;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterCells_CopyText;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterCells_ReplaceText;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterCells_ReformatNumbers;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFilterCells_ReformatDates;
    }
}