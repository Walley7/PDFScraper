namespace PDFScrapeTest.Forms {
    partial class ScrapeDock {
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
            this.btnScrape = new System.Windows.Forms.Button();
            this.btnInput = new System.Windows.Forms.Button();
            this.dlgOpenPDF = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnScrape
            // 
            this.btnScrape.Location = new System.Drawing.Point(12, 12);
            this.btnScrape.Name = "btnScrape";
            this.btnScrape.Size = new System.Drawing.Size(151, 75);
            this.btnScrape.TabIndex = 0;
            this.btnScrape.Text = "Scrape Data";
            this.btnScrape.UseVisualStyleBackColor = true;
            this.btnScrape.Click += new System.EventHandler(this.btnScrape_Click);
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(12, 93);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(170, 103);
            this.btnInput.TabIndex = 1;
            this.btnInput.Text = "Input Data";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // dlgOpenPDF
            // 
            this.dlgOpenPDF.Filter = "CSV Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            // 
            // ScrapeDock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.btnScrape);
            this.Name = "ScrapeDock";
            this.Text = "ScrapeDock";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScrape;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.OpenFileDialog dlgOpenPDF;
    }
}