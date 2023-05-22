namespace PDFScrapeTest.Forms {
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
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPDF = new System.Windows.Forms.Panel();
            this.picPDF = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.txtPDFPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectPDF = new System.Windows.Forms.Button();
            this.tlpPDFControls = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPDFControlsCentre = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numPDFPage = new System.Windows.Forms.NumericUpDown();
            this.btnAddRegion = new System.Windows.Forms.Button();
            this.dlgOpenPDF = new System.Windows.Forms.OpenFileDialog();
            this.tlpBase.SuspendLayout();
            this.pnlPDF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tlpPDFControls.SuspendLayout();
            this.pnlPDFControlsCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDFPage)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpBase
            // 
            this.tlpBase.ColumnCount = 1;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Controls.Add(this.pnlPDF, 0, 2);
            this.tlpBase.Controls.Add(this.pnlTop, 0, 0);
            this.tlpBase.Controls.Add(this.tlpPDFControls, 0, 1);
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(0, 0);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 3;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Size = new System.Drawing.Size(800, 450);
            this.tlpBase.TabIndex = 1;
            // 
            // pnlPDF
            // 
            this.pnlPDF.AutoScroll = true;
            this.pnlPDF.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnlPDF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPDF.Controls.Add(this.picPDF);
            this.pnlPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPDF.Location = new System.Drawing.Point(3, 68);
            this.pnlPDF.Name = "pnlPDF";
            this.pnlPDF.Size = new System.Drawing.Size(794, 379);
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
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.tlpTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(794, 29);
            this.pnlTop.TabIndex = 2;
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 3;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTop.Controls.Add(this.txtPDFPath, 1, 0);
            this.tlpTop.Controls.Add(this.label1, 0, 0);
            this.tlpTop.Controls.Add(this.btnSelectPDF, 2, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(0, 0);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpTop.Size = new System.Drawing.Size(792, 27);
            this.tlpTop.TabIndex = 0;
            // 
            // txtPDFPath
            // 
            this.txtPDFPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPDFPath.Location = new System.Drawing.Point(103, 3);
            this.txtPDFPath.Name = "txtPDFPath";
            this.txtPDFPath.Size = new System.Drawing.Size(586, 20);
            this.txtPDFPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PDF Path";
            // 
            // btnSelectPDF
            // 
            this.btnSelectPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectPDF.Location = new System.Drawing.Point(694, 2);
            this.btnSelectPDF.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.btnSelectPDF.Name = "btnSelectPDF";
            this.btnSelectPDF.Size = new System.Drawing.Size(96, 22);
            this.btnSelectPDF.TabIndex = 2;
            this.btnSelectPDF.Text = "Select PDF";
            this.btnSelectPDF.UseVisualStyleBackColor = true;
            this.btnSelectPDF.Click += new System.EventHandler(this.btnSelectPDF_Click);
            // 
            // tlpPDFControls
            // 
            this.tlpPDFControls.ColumnCount = 3;
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPDFControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPDFControls.Controls.Add(this.pnlPDFControlsCentre, 1, 0);
            this.tlpPDFControls.Controls.Add(this.btnAddRegion, 2, 0);
            this.tlpPDFControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPDFControls.Location = new System.Drawing.Point(0, 35);
            this.tlpPDFControls.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPDFControls.Name = "tlpPDFControls";
            this.tlpPDFControls.RowCount = 1;
            this.tlpPDFControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPDFControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPDFControls.Size = new System.Drawing.Size(800, 30);
            this.tlpPDFControls.TabIndex = 3;
            // 
            // pnlPDFControlsCentre
            // 
            this.pnlPDFControlsCentre.Controls.Add(this.label2);
            this.pnlPDFControlsCentre.Controls.Add(this.numPDFPage);
            this.pnlPDFControlsCentre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPDFControlsCentre.Location = new System.Drawing.Point(350, 0);
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
            // btnAddRegion
            // 
            this.btnAddRegion.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddRegion.Location = new System.Drawing.Point(697, 3);
            this.btnAddRegion.Name = "btnAddRegion";
            this.btnAddRegion.Size = new System.Drawing.Size(100, 24);
            this.btnAddRegion.TabIndex = 3;
            this.btnAddRegion.Text = "Add Region";
            this.btnAddRegion.UseVisualStyleBackColor = true;
            this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegion_Click);
            // 
            // dlgOpenPDF
            // 
            this.dlgOpenPDF.Filter = "CSV Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // ModelDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.CloseButton = false;
            this.Controls.Add(this.tlpBase);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "ModelDock";
            this.Text = "Model";
            this.tlpBase.ResumeLayout(false);
            this.pnlPDF.ResumeLayout(false);
            this.pnlPDF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tlpPDFControls.ResumeLayout(false);
            this.pnlPDFControlsCentre.ResumeLayout(false);
            this.pnlPDFControlsCentre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPDFPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Panel pnlPDF;
        private System.Windows.Forms.PictureBox picPDF;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.TextBox txtPDFPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectPDF;
        private System.Windows.Forms.OpenFileDialog dlgOpenPDF;
        private System.Windows.Forms.TableLayoutPanel tlpPDFControls;
        private System.Windows.Forms.Panel pnlPDFControlsCentre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPDFPage;
        private System.Windows.Forms.Button btnAddRegion;
    }
}