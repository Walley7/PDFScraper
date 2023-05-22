namespace PDFScraper.Forms.Macros {
    partial class MacroPlayingControlForm {
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnUnpause = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDataInputSpeed = new System.Windows.Forms.Label();
            this.trkDataInputSpeed = new System.Windows.Forms.TrackBar();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trkSpeed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDataInputSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = global::PDFScraper.Properties.Resources.macro_resume;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(145, 33);
            this.btnBack.TabIndex = 33;
            this.btnBack.TabStop = false;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Image = global::PDFScraper.Properties.Resources.macro_play;
            this.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlay.Location = new System.Drawing.Point(5, 41);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(145, 33);
            this.btnPlay.TabIndex = 35;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Image = global::PDFScraper.Properties.Resources.macro_stop;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(5, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(145, 33);
            this.btnStop.TabIndex = 43;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "Stop";
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Image = global::PDFScraper.Properties.Resources.macro_pause;
            this.btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPause.Location = new System.Drawing.Point(5, 73);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(145, 33);
            this.btnPause.TabIndex = 44;
            this.btnPause.TabStop = false;
            this.btnPause.Text = "Pause";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnUnpause
            // 
            this.btnUnpause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnpause.Image = global::PDFScraper.Properties.Resources.macro_unpause;
            this.btnUnpause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnpause.Location = new System.Drawing.Point(5, 73);
            this.btnUnpause.Name = "btnUnpause";
            this.btnUnpause.Size = new System.Drawing.Size(145, 33);
            this.btnUnpause.TabIndex = 45;
            this.btnUnpause.TabStop = false;
            this.btnUnpause.Text = "Unpause";
            this.btnUnpause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnpause.UseVisualStyleBackColor = true;
            this.btnUnpause.Click += new System.EventHandler(this.btnUnpause_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDataInputSpeed);
            this.panel1.Controls.Add(this.trkDataInputSpeed);
            this.panel1.Controls.Add(this.lblSpeed);
            this.panel1.Controls.Add(this.trkSpeed);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 78);
            this.panel1.TabIndex = 48;
            // 
            // lblDataInputSpeed
            // 
            this.lblDataInputSpeed.AutoSize = true;
            this.lblDataInputSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataInputSpeed.Location = new System.Drawing.Point(2, 43);
            this.lblDataInputSpeed.Name = "lblDataInputSpeed";
            this.lblDataInputSpeed.Size = new System.Drawing.Size(69, 13);
            this.lblDataInputSpeed.TabIndex = 51;
            this.lblDataInputSpeed.Text = "Input (1000x)";
            // 
            // trkDataInputSpeed
            // 
            this.trkDataInputSpeed.LargeChange = 1;
            this.trkDataInputSpeed.Location = new System.Drawing.Point(65, 40);
            this.trkDataInputSpeed.Maximum = 3;
            this.trkDataInputSpeed.Name = "trkDataInputSpeed";
            this.trkDataInputSpeed.Size = new System.Drawing.Size(92, 45);
            this.trkDataInputSpeed.TabIndex = 50;
            this.trkDataInputSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkDataInputSpeed.Value = 3;
            this.trkDataInputSpeed.ValueChanged += new System.EventHandler(this.trkDataInputSpeed_ValueChanged);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.Location = new System.Drawing.Point(2, 22);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(50, 13);
            this.lblSpeed.TabIndex = 49;
            this.lblSpeed.Text = "Main (1x)";
            // 
            // trkSpeed
            // 
            this.trkSpeed.LargeChange = 1;
            this.trkSpeed.Location = new System.Drawing.Point(65, 19);
            this.trkSpeed.Maximum = 4;
            this.trkSpeed.Name = "trkSpeed";
            this.trkSpeed.Size = new System.Drawing.Size(92, 45);
            this.trkSpeed.TabIndex = 48;
            this.trkSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkSpeed.ValueChanged += new System.EventHandler(this.trkSpeed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speeds";
            // 
            // MacroPlayingControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 183);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnUnpause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(171, 217);
            this.MinimumSize = new System.Drawing.Size(171, 217);
            this.Name = "MacroPlayingControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Macro - Playing";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MacroPlayingControlForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDataInputSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnUnpause;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDataInputSpeed;
        private System.Windows.Forms.TrackBar trkDataInputSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar trkSpeed;
        private System.Windows.Forms.Label label1;
    }
}