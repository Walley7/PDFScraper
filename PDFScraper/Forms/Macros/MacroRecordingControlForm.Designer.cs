namespace PDFScraper.Forms.Macros {
    partial class MacroRecordingControlForm {
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
            this.btnDataInput = new System.Windows.Forms.Button();
            this.btnNextBranch = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnEndRepetition = new System.Windows.Forms.Button();
            this.btnEndBranching = new System.Windows.Forms.Button();
            this.btnBeginBranching = new System.Windows.Forms.Button();
            this.btnBeginRepetition = new System.Windows.Forms.Button();
            this.btnBegin = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnUnpause = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDataInput
            // 
            this.btnDataInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataInput.Image = global::PDFScraper.Properties.Resources.macro_add_input;
            this.btnDataInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataInput.Location = new System.Drawing.Point(5, 277);
            this.btnDataInput.Name = "btnDataInput";
            this.btnDataInput.Size = new System.Drawing.Size(145, 33);
            this.btnDataInput.TabIndex = 38;
            this.btnDataInput.TabStop = false;
            this.btnDataInput.Text = "Data Input";
            this.btnDataInput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDataInput.UseVisualStyleBackColor = true;
            this.btnDataInput.Click += new System.EventHandler(this.btnDataInput_Click);
            // 
            // btnNextBranch
            // 
            this.btnNextBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextBranch.Image = global::PDFScraper.Properties.Resources.macro_add_branch;
            this.btnNextBranch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNextBranch.Location = new System.Drawing.Point(21, 241);
            this.btnNextBranch.Name = "btnNextBranch";
            this.btnNextBranch.Size = new System.Drawing.Size(129, 33);
            this.btnNextBranch.TabIndex = 37;
            this.btnNextBranch.TabStop = false;
            this.btnNextBranch.Text = "Next Branch";
            this.btnNextBranch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNextBranch.UseVisualStyleBackColor = true;
            this.btnNextBranch.Click += new System.EventHandler(this.btnNextBranch_Click);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = global::PDFScraper.Properties.Resources.macro_resume;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(145, 33);
            this.btnBack.TabIndex = 32;
            this.btnBack.TabStop = false;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnEndRepetition
            // 
            this.btnEndRepetition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndRepetition.Image = global::PDFScraper.Properties.Resources.macro_stop_repeating;
            this.btnEndRepetition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEndRepetition.Location = new System.Drawing.Point(5, 141);
            this.btnEndRepetition.Name = "btnEndRepetition";
            this.btnEndRepetition.Size = new System.Drawing.Size(145, 33);
            this.btnEndRepetition.TabIndex = 39;
            this.btnEndRepetition.TabStop = false;
            this.btnEndRepetition.Text = "End Repetition";
            this.btnEndRepetition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEndRepetition.UseVisualStyleBackColor = true;
            this.btnEndRepetition.Click += new System.EventHandler(this.btnEndRepetition_Click);
            // 
            // btnEndBranching
            // 
            this.btnEndBranching.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndBranching.Image = global::PDFScraper.Properties.Resources.macro_stop_branching;
            this.btnEndBranching.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEndBranching.Location = new System.Drawing.Point(5, 209);
            this.btnEndBranching.Name = "btnEndBranching";
            this.btnEndBranching.Size = new System.Drawing.Size(145, 33);
            this.btnEndBranching.TabIndex = 40;
            this.btnEndBranching.TabStop = false;
            this.btnEndBranching.Text = "End Branching";
            this.btnEndBranching.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEndBranching.UseVisualStyleBackColor = true;
            this.btnEndBranching.Click += new System.EventHandler(this.btnEndBranching_Click);
            // 
            // btnBeginBranching
            // 
            this.btnBeginBranching.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginBranching.Image = global::PDFScraper.Properties.Resources.macro_record_branching1;
            this.btnBeginBranching.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBeginBranching.Location = new System.Drawing.Point(5, 177);
            this.btnBeginBranching.Name = "btnBeginBranching";
            this.btnBeginBranching.Size = new System.Drawing.Size(145, 33);
            this.btnBeginBranching.TabIndex = 36;
            this.btnBeginBranching.TabStop = false;
            this.btnBeginBranching.Text = "Begin Branching";
            this.btnBeginBranching.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBeginBranching.UseVisualStyleBackColor = true;
            this.btnBeginBranching.Click += new System.EventHandler(this.btnBeginBranching_Click);
            // 
            // btnBeginRepetition
            // 
            this.btnBeginRepetition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginRepetition.Image = global::PDFScraper.Properties.Resources.macro_record_repeating;
            this.btnBeginRepetition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBeginRepetition.Location = new System.Drawing.Point(5, 109);
            this.btnBeginRepetition.Name = "btnBeginRepetition";
            this.btnBeginRepetition.Size = new System.Drawing.Size(145, 33);
            this.btnBeginRepetition.TabIndex = 35;
            this.btnBeginRepetition.TabStop = false;
            this.btnBeginRepetition.Text = "Begin Repetition";
            this.btnBeginRepetition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBeginRepetition.UseVisualStyleBackColor = true;
            this.btnBeginRepetition.Click += new System.EventHandler(this.btnBeginRepetition_Click);
            // 
            // btnBegin
            // 
            this.btnBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.Image = global::PDFScraper.Properties.Resources.macro_record;
            this.btnBegin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBegin.Location = new System.Drawing.Point(5, 41);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(145, 33);
            this.btnBegin.TabIndex = 34;
            this.btnBegin.TabStop = false;
            this.btnBegin.Text = "Begin";
            this.btnBegin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Image = global::PDFScraper.Properties.Resources.macro_pause;
            this.btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPause.Location = new System.Drawing.Point(5, 73);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(145, 33);
            this.btnPause.TabIndex = 33;
            this.btnPause.TabStop = false;
            this.btnPause.Text = "Pause";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnd.Image = global::PDFScraper.Properties.Resources.macro_stop;
            this.btnEnd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnd.Location = new System.Drawing.Point(5, 41);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(145, 33);
            this.btnEnd.TabIndex = 42;
            this.btnEnd.TabStop = false;
            this.btnEnd.Text = "End";
            this.btnEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnUnpause
            // 
            this.btnUnpause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnpause.Image = global::PDFScraper.Properties.Resources.macro_unpause;
            this.btnUnpause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnpause.Location = new System.Drawing.Point(5, 73);
            this.btnUnpause.Name = "btnUnpause";
            this.btnUnpause.Size = new System.Drawing.Size(145, 33);
            this.btnUnpause.TabIndex = 41;
            this.btnUnpause.TabStop = false;
            this.btnUnpause.Text = "Unpause";
            this.btnUnpause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnpause.UseVisualStyleBackColor = true;
            this.btnUnpause.Click += new System.EventHandler(this.btnUnpause_Click);
            // 
            // MacroRecordingControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 315);
            this.ControlBox = false;
            this.Controls.Add(this.btnDataInput);
            this.Controls.Add(this.btnNextBranch);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnEndRepetition);
            this.Controls.Add(this.btnEndBranching);
            this.Controls.Add(this.btnBeginBranching);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnUnpause);
            this.Controls.Add(this.btnBegin);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnBeginRepetition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(171, 354);
            this.MinimumSize = new System.Drawing.Size(171, 354);
            this.Name = "MacroRecordingControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Macro - Recording";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MacroRecordingControlForm_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnDataInput;
        private System.Windows.Forms.Button btnNextBranch;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnEndRepetition;
        private System.Windows.Forms.Button btnEndBranching;
        private System.Windows.Forms.Button btnBeginBranching;
        private System.Windows.Forms.Button btnBeginRepetition;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnUnpause;
    }
}