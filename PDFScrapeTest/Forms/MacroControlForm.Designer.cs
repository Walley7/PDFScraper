namespace PDFScrapeTest.Forms {
    partial class MacroControlForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroControlForm));
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnStartPlaying = new System.Windows.Forms.Button();
            this.btnStopPlaying = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddInput = new System.Windows.Forms.Button();
            this.trkSpeed = new System.Windows.Forms.TrackBar();
            this.lblSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Image = ((System.Drawing.Image)(resources.GetObject("btnStartRecording.Image")));
            this.btnStartRecording.Location = new System.Drawing.Point(10, 10);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(40, 30);
            this.btnStartRecording.TabIndex = 0;
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Enabled = false;
            this.btnStopRecording.Image = ((System.Drawing.Image)(resources.GetObject("btnStopRecording.Image")));
            this.btnStopRecording.Location = new System.Drawing.Point(10, 10);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(40, 30);
            this.btnStopRecording.TabIndex = 1;
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnStartPlaying
            // 
            this.btnStartPlaying.Image = ((System.Drawing.Image)(resources.GetObject("btnStartPlaying.Image")));
            this.btnStartPlaying.Location = new System.Drawing.Point(49, 10);
            this.btnStartPlaying.Name = "btnStartPlaying";
            this.btnStartPlaying.Size = new System.Drawing.Size(40, 30);
            this.btnStartPlaying.TabIndex = 2;
            this.btnStartPlaying.UseVisualStyleBackColor = true;
            this.btnStartPlaying.Click += new System.EventHandler(this.btnStartPlaying_Click);
            // 
            // btnStopPlaying
            // 
            this.btnStopPlaying.Enabled = false;
            this.btnStopPlaying.Image = ((System.Drawing.Image)(resources.GetObject("btnStopPlaying.Image")));
            this.btnStopPlaying.Location = new System.Drawing.Point(49, 10);
            this.btnStopPlaying.Name = "btnStopPlaying";
            this.btnStopPlaying.Size = new System.Drawing.Size(40, 30);
            this.btnStopPlaying.TabIndex = 3;
            this.btnStopPlaying.UseVisualStyleBackColor = true;
            this.btnStopPlaying.Click += new System.EventHandler(this.btnStopPlaying_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(75, 46);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(130, 20);
            this.txtStatus.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Status";
            // 
            // btnResume
            // 
            this.btnResume.Enabled = false;
            this.btnResume.Image = ((System.Drawing.Image)(resources.GetObject("btnResume.Image")));
            this.btnResume.Location = new System.Drawing.Point(88, 10);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(40, 30);
            this.btnResume.TabIndex = 6;
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(166, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(40, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddInput
            // 
            this.btnAddInput.Image = ((System.Drawing.Image)(resources.GetObject("btnAddInput.Image")));
            this.btnAddInput.Location = new System.Drawing.Point(127, 10);
            this.btnAddInput.Name = "btnAddInput";
            this.btnAddInput.Size = new System.Drawing.Size(40, 30);
            this.btnAddInput.TabIndex = 8;
            this.btnAddInput.UseVisualStyleBackColor = true;
            this.btnAddInput.Click += new System.EventHandler(this.btnAddInput_Click);
            // 
            // trkSpeed
            // 
            this.trkSpeed.LargeChange = 1;
            this.trkSpeed.Location = new System.Drawing.Point(75, 72);
            this.trkSpeed.Maximum = 3;
            this.trkSpeed.Name = "trkSpeed";
            this.trkSpeed.Size = new System.Drawing.Size(128, 45);
            this.trkSpeed.TabIndex = 9;
            this.trkSpeed.Value = 3;
            this.trkSpeed.ValueChanged += new System.EventHandler(this.trkSpeed_ValueChanged);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(7, 75);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(62, 13);
            this.lblSpeed.TabIndex = 10;
            this.lblSpeed.Text = "Speed (1.0)";
            // 
            // MacroControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 107);
            this.ControlBox = false;
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.trkSpeed);
            this.Controls.Add(this.btnAddInput);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnStartRecording);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.btnStartPlaying);
            this.Controls.Add(this.btnStopPlaying);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStatus);
            this.KeyPreview = true;
            this.Name = "MacroControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Macro";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MacroControlForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartRecording;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.Button btnStartPlaying;
        private System.Windows.Forms.Button btnStopPlaying;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddInput;
        private System.Windows.Forms.TrackBar trkSpeed;
        private System.Windows.Forms.Label lblSpeed;
    }
}