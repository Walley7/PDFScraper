namespace PDFScraper.Forms.Model {
    partial class ExtractorRegionForm {
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
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPDFControls = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPDFControlsCentre = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numPDFPage = new System.Windows.Forms.NumericUpDown();
            this.chkWholePage = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRedFiltered = new System.Windows.Forms.Label();
            this.pnlPDF = new System.Windows.Forms.Panel();
            this.picPDF = new System.Windows.Forms.PictureBox();
            this.mnuRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRightClick_AddColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRightClick_RemoveColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpBase.SuspendLayout();
            this.tlpPDFControls.SuspendLayout();
            this.pnlPDFControlsCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDFPage)).BeginInit();
            this.pnlPDF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).BeginInit();
            this.mnuRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpBase
            // 
            this.tlpBase.ColumnCount = 1;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Controls.Add(this.tlpPDFControls, 0, 1);
            this.tlpBase.Controls.Add(this.pnlPDF, 0, 0);
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(0, 0);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 2;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpBase.Size = new System.Drawing.Size(984, 761);
            this.tlpBase.TabIndex = 2;
            // 
            // tlpPDFControls
            // 
            this.tlpPDFControls.ColumnCount = 4;
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPDFControls.Controls.Add(this.pnlPDFControlsCentre, 0, 0);
            this.tlpPDFControls.Controls.Add(this.chkWholePage, 1, 0);
            this.tlpPDFControls.Controls.Add(this.btnClose, 3, 0);
            this.tlpPDFControls.Controls.Add(this.lblRedFiltered, 2, 0);
            this.tlpPDFControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPDFControls.Location = new System.Drawing.Point(0, 731);
            this.tlpPDFControls.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPDFControls.Name = "tlpPDFControls";
            this.tlpPDFControls.RowCount = 1;
            this.tlpPDFControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPDFControls.Size = new System.Drawing.Size(984, 30);
            this.tlpPDFControls.TabIndex = 4;
            // 
            // pnlPDFControlsCentre
            // 
            this.pnlPDFControlsCentre.Controls.Add(this.label2);
            this.pnlPDFControlsCentre.Controls.Add(this.numPDFPage);
            this.pnlPDFControlsCentre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPDFControlsCentre.Location = new System.Drawing.Point(0, 0);
            this.pnlPDFControlsCentre.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPDFControlsCentre.Name = "pnlPDFControlsCentre";
            this.pnlPDFControlsCentre.Size = new System.Drawing.Size(100, 30);
            this.pnlPDFControlsCentre.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Page:";
            // 
            // numPDFPage
            // 
            this.numPDFPage.Location = new System.Drawing.Point(44, 5);
            this.numPDFPage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPDFPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPDFPage.Name = "numPDFPage";
            this.numPDFPage.Size = new System.Drawing.Size(50, 20);
            this.numPDFPage.TabIndex = 0;
            this.numPDFPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPDFPage.ValueChanged += new System.EventHandler(this.numPDFPage_ValueChanged);
            // 
            // chkWholePage
            // 
            this.chkWholePage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkWholePage.AutoSize = true;
            this.chkWholePage.Location = new System.Drawing.Point(122, 5);
            this.chkWholePage.Name = "chkWholePage";
            this.chkWholePage.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.chkWholePage.Size = new System.Drawing.Size(85, 19);
            this.chkWholePage.TabIndex = 3;
            this.chkWholePage.Text = "Whole Page";
            this.chkWholePage.UseVisualStyleBackColor = true;
            this.chkWholePage.CheckedChanged += new System.EventHandler(this.chkWholePage_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(917, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRedFiltered
            // 
            this.lblRedFiltered.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRedFiltered.AutoSize = true;
            this.lblRedFiltered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedFiltered.ForeColor = System.Drawing.Color.DarkRed;
            this.lblRedFiltered.Location = new System.Drawing.Point(601, 8);
            this.lblRedFiltered.Name = "lblRedFiltered";
            this.lblRedFiltered.Size = new System.Drawing.Size(310, 13);
            this.lblRedFiltered.TabIndex = 6;
            this.lblRedFiltered.Text = "Red/pink tones being shown in greyscale for visibility";
            this.lblRedFiltered.Visible = false;
            // 
            // pnlPDF
            // 
            this.pnlPDF.AutoScroll = true;
            this.pnlPDF.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnlPDF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPDF.Controls.Add(this.picPDF);
            this.pnlPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPDF.Location = new System.Drawing.Point(3, 3);
            this.pnlPDF.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlPDF.Name = "pnlPDF";
            this.pnlPDF.Size = new System.Drawing.Size(978, 728);
            this.pnlPDF.TabIndex = 1;
            // 
            // picPDF
            // 
            this.picPDF.Location = new System.Drawing.Point(0, 0);
            this.picPDF.Name = "picPDF";
            this.picPDF.Size = new System.Drawing.Size(50, 50);
            this.picPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPDF.TabIndex = 0;
            this.picPDF.TabStop = false;
            this.picPDF.Paint += new System.Windows.Forms.PaintEventHandler(this.picPDF_Paint);
            this.picPDF.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPDF_MouseDown);
            this.picPDF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPDF_MouseMove);
            this.picPDF.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPDF_MouseUp);
            // 
            // mnuRightClick
            // 
            this.mnuRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRightClick_AddColumn,
            this.mnuRightClick_RemoveColumn});
            this.mnuRightClick.Name = "mnuRightClick";
            this.mnuRightClick.Size = new System.Drawing.Size(164, 48);
            this.mnuRightClick.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuRightClick_ItemClicked);
            // 
            // mnuRightClick_AddColumn
            // 
            this.mnuRightClick_AddColumn.Name = "mnuRightClick_AddColumn";
            this.mnuRightClick_AddColumn.Size = new System.Drawing.Size(163, 22);
            this.mnuRightClick_AddColumn.Text = "Add Column";
            // 
            // mnuRightClick_RemoveColumn
            // 
            this.mnuRightClick_RemoveColumn.Name = "mnuRightClick_RemoveColumn";
            this.mnuRightClick_RemoveColumn.Size = new System.Drawing.Size(163, 22);
            this.mnuRightClick_RemoveColumn.Text = "Remove Column";
            // 
            // ExtractorRegionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.tlpBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "ExtractorRegionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extractor Region";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtractorRegionForm_FormClosing);
            this.Load += new System.EventHandler(this.ExtractorRegionForm_Load);
            this.tlpBase.ResumeLayout(false);
            this.tlpPDFControls.ResumeLayout(false);
            this.tlpPDFControls.PerformLayout();
            this.pnlPDFControlsCentre.ResumeLayout(false);
            this.pnlPDFControlsCentre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDFPage)).EndInit();
            this.pnlPDF.ResumeLayout(false);
            this.pnlPDF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).EndInit();
            this.mnuRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Panel pnlPDF;
        private System.Windows.Forms.PictureBox picPDF;
        private System.Windows.Forms.TableLayoutPanel tlpPDFControls;
        private System.Windows.Forms.Panel pnlPDFControlsCentre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPDFPage;
        private System.Windows.Forms.CheckBox chkWholePage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRedFiltered;
        private System.Windows.Forms.ContextMenuStrip mnuRightClick;
        private System.Windows.Forms.ToolStripMenuItem mnuRightClick_AddColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuRightClick_RemoveColumn;
    }
}