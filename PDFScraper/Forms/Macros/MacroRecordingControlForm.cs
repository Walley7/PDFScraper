using PDFScrape.Macros;
using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Macros {

    public partial class MacroRecordingControlForm : Form {
        //================================================================================
        public const int                        X_OFFSET_FROM_CURSOR = -72;
        public const int                        Y_OFFSET_FROM_CURSOR = -52;


        //================================================================================
        private MacroRecorder                   mMacroRecorder = null;

        private Macro                           mMacro = null;
        private PDFScrapeModel                  mSampleModel = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroRecordingControlForm(MacroRecorder recorder) {
            // Initialise
            InitializeComponent();

            // Recorder
            mMacroRecorder = recorder;
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        public void OnShow() {
            // Desired position
            Point desiredPosition = new Point(Cursor.Position.X + X_OFFSET_FROM_CURSOR, Cursor.Position.Y + Y_OFFSET_FROM_CURSOR);

            // Position
            Rectangle screenArea = Screen.FromPoint(Cursor.Position).WorkingArea;
            Location = new Point(Math.Min(Math.Max(desiredPosition.X, screenArea.Left), screenArea.Right - Size.Width),
                                 Math.Min(Math.Max(desiredPosition.Y, screenArea.Top), screenArea.Bottom - Size.Height));

            // Recorder
            mMacroRecorder.Disable();

            // Buttons
            UpdateButtonStates();
        }

        //--------------------------------------------------------------------------------
        public void OnHide() {
            // Recorder
            mMacroRecorder.Enable();

            // Macro
            //if (!mMacroRecorder.Recording)
            //    mMacro = null;
        }


        // SHOWING ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowDialog(Macro macro, PDFScrapeModel sampleModel) {
            // Macro / sample model
            mMacro = macro;
            mSampleModel = sampleModel;

            // Show
            OnShow();
            ShowDialog();
        }


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public Macro Macro { get { return mMacro; } }


        // BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnBack_Click(object sender, EventArgs e) {
            OnHide();
            Hide();
        }
        
        //--------------------------------------------------------------------------------
        private void btnBegin_Click(object sender, EventArgs e) {
            // Variables
            bool clear = true;

            // Confirmation
            if (mMacro.ElementCount != 0) {
                DialogResult dialogResult = MessageBox.Show("Clear existing macro?", "Clear?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel)
                    return;
                clear = (dialogResult == DialogResult.Yes);               
            }

            // Begin
            mMacroRecorder.Start(mMacro, clear);
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnEnd_Click(object sender, EventArgs e) {
            // End
            mMacroRecorder.Stop();
            UpdateButtonStates();
        }

        //--------------------------------------------------------------------------------
        private void btnPause_Click(object sender, EventArgs e) {
            // Pause
            mMacroRecorder.Pause();
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnUnpause_Click(object sender, EventArgs e) {
            // Unpause
            mMacroRecorder.Unpause();
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnBeginRepetition_Click(object sender, EventArgs e) {
            // Begin repetition
            MacroRecordingBeginRepetitionForm beginRepetitionForm = new MacroRecordingBeginRepetitionForm(mSampleModel);
            if (beginRepetitionForm.ShowDialog() == DialogResult.OK)
                mMacroRecorder.BeginRepetition(beginRepetitionForm.Mode, beginRepetitionForm.ExtractorName);
            beginRepetitionForm.Dispose();

            // Button states
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnEndRepetition_Click(object sender, EventArgs e) {
            // End repetition
            mMacroRecorder.EndRepetition();
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnBeginBranching_Click(object sender, EventArgs e) {
            // Forms
            MacroRecordingBeginBranchingForm beginBranchingForm = new MacroRecordingBeginBranchingForm(mMacroRecorder, mSampleModel);
            MacroRecordingNextBranchForm nextBranchForm = new MacroRecordingNextBranchForm();

            // Branching
            if (beginBranchingForm.ShowDialog() == DialogResult.OK) {
                if (nextBranchForm.ShowDialog() == DialogResult.OK) {
                    mMacroRecorder.BeginBranching(beginBranchingForm.SourceType, beginBranchingForm.Extractor, beginBranchingForm.RepetitionIndex, beginBranchingForm.ColumnSpecifier);
                    mMacroRecorder.BeginBranch(nextBranchForm.Condition, nextBranchForm.ConditionArgument, nextBranchForm.Final);
                }
            }

            // Dispose
            beginBranchingForm.Dispose();
            nextBranchForm.Dispose();

            // Button states
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnEndBranching_Click(object sender, EventArgs e) {
            // End branching
            mMacroRecorder.EndBranch();
            mMacroRecorder.EndBranching();

            // Button states
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnNextBranch_Click(object sender, EventArgs e) {
            // Form
            MacroRecordingNextBranchForm nextBranchForm = new MacroRecordingNextBranchForm();

            // Branch
            if (nextBranchForm.ShowDialog() == DialogResult.OK) {
                mMacroRecorder.EndBranch();
                mMacroRecorder.BeginBranch(nextBranchForm.Condition, nextBranchForm.ConditionArgument, nextBranchForm.Final);
            }

            // Dispose
            nextBranchForm.Dispose();

            // Button states
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnDataInput_Click(object sender, EventArgs e) {
            // Form
            MacroRecordingDataInputForm dataInputForm = new MacroRecordingDataInputForm(mMacroRecorder, mSampleModel);

            // Data input
            if (dataInputForm.ShowDialog() == DialogResult.OK)
                mMacroRecorder.AddDataInput(dataInputForm.SourceType, dataInputForm.Extractor, dataInputForm.RepetitionIndex, dataInputForm.ColumnSpecifier);

            // Dispose
            dataInputForm.Dispose();

            // Button states
            UpdateButtonStates();
        }

        //--------------------------------------------------------------------------------
        private void UpdateButtonStates() {
            // Begin / end
            btnBegin.Visible = btnBegin.Enabled = !mMacroRecorder.Recording;
            btnEnd.Visible = btnEnd.Enabled = mMacroRecorder.Recording;

            // Pause / unpause
            btnPause.Visible = !mMacroRecorder.Recording || !mMacroRecorder.Paused;
            btnPause.Enabled = mMacroRecorder.Recording && !mMacroRecorder.Paused;
            btnUnpause.Visible = btnUnpause.Enabled = mMacroRecorder.Recording && mMacroRecorder.Paused;

            // Repetitions
            btnBeginRepetition.Text = "Begin Repetition " + (mMacroRecorder.RepetitionDepth + 1);
            btnBeginRepetition.Enabled = mMacroRecorder.Recording;
            btnEndRepetition.Text = "End Repetition " + Math.Max(mMacroRecorder.RepetitionDepth, 1);
            btnEndRepetition.Enabled = mMacroRecorder.Recording && mMacroRecorder.InRepetition;

            // Branches
            btnBeginBranching.Text = "Begin Branching " + (mMacroRecorder.BranchingDepth + 1);
            btnBeginBranching.Enabled = mMacroRecorder.Recording;
            btnEndBranching.Text = "End Branching " + Math.Max(mMacroRecorder.BranchingDepth, 1);
            btnEndBranching.Enabled = mMacroRecorder.Recording && (mMacroRecorder.InBranching || mMacroRecorder.InBranch);
            btnNextBranch.Enabled = mMacroRecorder.InBranching || mMacroRecorder.InBranch;

            // Data input
            btnDataInput.Enabled = mMacroRecorder.Recording;

            // Focus
            Focus();
        }
        

        // KEY PRESSES ================================================================================
        //--------------------------------------------------------------------------------
        private void MacroRecordingControlForm_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case '1':
                    // Back
                    btnBack.PerformClick();
                    break;
                case '2':
                    // Begin / end
                    if (btnBegin.Enabled)
                        btnBegin.PerformClick();
                    else if (btnEnd.Enabled)
                        btnEnd.PerformClick();
                    break;
                case '3':
                    // Pause / unpause
                    if (btnPause.Enabled)
                        btnPause.PerformClick();
                    else if (btnUnpause.Enabled)
                        btnUnpause.PerformClick();
                    break;
                case '4':
                    // Begin repetition
                    if (btnBeginRepetition.Enabled)
                        btnBeginRepetition.PerformClick();
                    break;
                case '5':
                    // End repetition
                    if (btnEndRepetition.Enabled)
                        btnEndRepetition.PerformClick();
                    break;
                case '6':
                    // Begin branching
                    if (btnBeginBranching.Enabled)
                        btnBeginBranching.PerformClick();
                    break;
                case '7':
                    // End branching
                    if (btnEndBranching.Enabled)
                        btnEndBranching.PerformClick();
                    break;
                case '8':
                    // Next branch
                    if (btnNextBranch.Enabled)
                        btnNextBranch.PerformClick();
                    break;
                case '9':
                    // Data input
                    if (btnDataInput.Enabled)
                        btnDataInput.PerformClick();
                    break;
            }
        }
    }

}
