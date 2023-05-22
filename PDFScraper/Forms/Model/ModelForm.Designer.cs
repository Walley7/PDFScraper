namespace PDFScraper.Forms.Model {
    partial class ModelForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
            this.flpMenuRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMacroEditor = new System.Windows.Forms.Button();
            this.btnModelEditor = new System.Windows.Forms.Button();
            this.btnScraper = new System.Windows.Forms.Button();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMenu = new System.Windows.Forms.TableLayoutPanel();
            this.flpMenuLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.dckMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.dlgOpenModel = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveModel = new System.Windows.Forms.SaveFileDialog();
            this.flpMenuRight.SuspendLayout();
            this.tlpMenu.SuspendLayout();
            this.flpMenuLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMenuRight
            // 
            this.flpMenuRight.Controls.Add(this.btnExit);
            this.flpMenuRight.Controls.Add(this.btnMacroEditor);
            this.flpMenuRight.Controls.Add(this.btnModelEditor);
            this.flpMenuRight.Controls.Add(this.btnScraper);
            this.flpMenuRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenuRight.Location = new System.Drawing.Point(300, 0);
            this.flpMenuRight.Margin = new System.Windows.Forms.Padding(0);
            this.flpMenuRight.Name = "flpMenuRight";
            this.flpMenuRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flpMenuRight.Size = new System.Drawing.Size(884, 41);
            this.flpMenuRight.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.Location = new System.Drawing.Point(816, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 35);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMacroEditor
            // 
            this.btnMacroEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMacroEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnMacroEditor.Image")));
            this.btnMacroEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMacroEditor.Location = new System.Drawing.Point(694, 3);
            this.btnMacroEditor.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.btnMacroEditor.Name = "btnMacroEditor";
            this.btnMacroEditor.Size = new System.Drawing.Size(120, 35);
            this.btnMacroEditor.TabIndex = 2;
            this.btnMacroEditor.Text = "Macro Editor";
            this.btnMacroEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMacroEditor.UseVisualStyleBackColor = true;
            this.btnMacroEditor.Click += new System.EventHandler(this.btnMacroEditor_Click);
            // 
            // btnModelEditor
            // 
            this.btnModelEditor.Enabled = false;
            this.btnModelEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModelEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnModelEditor.Image")));
            this.btnModelEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModelEditor.Location = new System.Drawing.Point(577, 3);
            this.btnModelEditor.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.btnModelEditor.Name = "btnModelEditor";
            this.btnModelEditor.Size = new System.Drawing.Size(115, 35);
            this.btnModelEditor.TabIndex = 1;
            this.btnModelEditor.Text = "Model Editor";
            this.btnModelEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModelEditor.UseVisualStyleBackColor = true;
            this.btnModelEditor.Click += new System.EventHandler(this.btnModelEditor_Click);
            // 
            // btnScraper
            // 
            this.btnScraper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScraper.Image = ((System.Drawing.Image)(resources.GetObject("btnScraper.Image")));
            this.btnScraper.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScraper.Location = new System.Drawing.Point(485, 3);
            this.btnScraper.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.btnScraper.Name = "btnScraper";
            this.btnScraper.Size = new System.Drawing.Size(90, 35);
            this.btnScraper.TabIndex = 0;
            this.btnScraper.Text = "Scraper";
            this.btnScraper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScraper.UseVisualStyleBackColor = true;
            this.btnScraper.Click += new System.EventHandler(this.btnScraper_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tlpMenu
            // 
            this.tlpMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tlpMenu.ColumnCount = 2;
            this.tlpMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMenu.Controls.Add(this.flpMenuRight, 1, 0);
            this.tlpMenu.Controls.Add(this.flpMenuLeft, 0, 0);
            this.tlpMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMenu.Location = new System.Drawing.Point(0, 0);
            this.tlpMenu.Name = "tlpMenu";
            this.tlpMenu.RowCount = 1;
            this.tlpMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMenu.Size = new System.Drawing.Size(1184, 41);
            this.tlpMenu.TabIndex = 9;
            // 
            // flpMenuLeft
            // 
            this.flpMenuLeft.Controls.Add(this.btnNew);
            this.flpMenuLeft.Controls.Add(this.btnOpen);
            this.flpMenuLeft.Controls.Add(this.btnSave);
            this.flpMenuLeft.Controls.Add(this.btnSaveAs);
            this.flpMenuLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMenuLeft.Location = new System.Drawing.Point(0, 0);
            this.flpMenuLeft.Margin = new System.Windows.Forms.Padding(0);
            this.flpMenuLeft.Name = "flpMenuLeft";
            this.flpMenuLeft.Size = new System.Drawing.Size(300, 41);
            this.flpMenuLeft.TabIndex = 4;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(70, 25);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.Location = new System.Drawing.Point(75, 3);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(70, 25);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(147, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveAs.Location = new System.Drawing.Point(219, 3);
            this.btnSaveAs.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(70, 25);
            this.btnSaveAs.TabIndex = 4;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // dckMain
            // 
            this.dckMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dckMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dckMain.Location = new System.Drawing.Point(0, 41);
            this.dckMain.Name = "dckMain";
            this.dckMain.Size = new System.Drawing.Size(1184, 920);
            this.dckMain.TabIndex = 11;
            // 
            // dlgOpenModel
            // 
            this.dlgOpenModel.Filter = "Model Files (*.psmdl)|*.psmdl|All files (*.*)|*.*";
            // 
            // dlgSaveModel
            // 
            this.dlgSaveModel.Filter = "Model Files (*.psmdl)|*.psmdl|All files (*.*)|*.*";
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 961);
            this.Controls.Add(this.dckMain);
            this.Controls.Add(this.tlpMenu);
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(720, 720);
            this.Name = "ModelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Scraper (Models)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModelForm_FormClosed);
            this.flpMenuRight.ResumeLayout(false);
            this.tlpMenu.ResumeLayout(false);
            this.flpMenuLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpMenuRight;
        private System.Windows.Forms.Button btnScraper;
        private System.Windows.Forms.Button btnModelEditor;
        private System.Windows.Forms.Button btnMacroEditor;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpMenu;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dckMain;
        private System.Windows.Forms.FlowLayoutPanel flpMenuLeft;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.OpenFileDialog dlgOpenModel;
        private System.Windows.Forms.SaveFileDialog dlgSaveModel;
    }
}