namespace PDFScrapeTest.Forms {
    partial class RegionPropertiesDock {
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
            this.chkMandatory = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnTestScrape = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkMandatory
            // 
            this.chkMandatory.AutoSize = true;
            this.chkMandatory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMandatory.Location = new System.Drawing.Point(11, 38);
            this.chkMandatory.Name = "chkMandatory";
            this.chkMandatory.Size = new System.Drawing.Size(76, 17);
            this.chkMandatory.TabIndex = 0;
            this.chkMandatory.Text = "Mandatory";
            this.chkMandatory.UseVisualStyleBackColor = true;
            this.chkMandatory.CheckedChanged += new System.EventHandler(this.chkMandatory_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(73, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnTestScrape
            // 
            this.btnTestScrape.Location = new System.Drawing.Point(73, 61);
            this.btnTestScrape.Name = "btnTestScrape";
            this.btnTestScrape.Size = new System.Drawing.Size(150, 23);
            this.btnTestScrape.TabIndex = 2;
            this.btnTestScrape.Text = "Test Scrape";
            this.btnTestScrape.UseVisualStyleBackColor = true;
            this.btnTestScrape.Click += new System.EventHandler(this.btnTestScrape_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // RegionPropertiesDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.CloseButton = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTestScrape);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkMandatory);
            this.Name = "RegionPropertiesDock";
            this.Text = "Region";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMandatory;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnTestScrape;
        private System.Windows.Forms.Label label1;
    }
}