using PDFScrape.Data;
using PDFScrape.Macros;
using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Macros {

    public partial class MacroPlayingControlForm : Form {
        //================================================================================
        public const int                        X_OFFSET_FROM_CURSOR = -72;
        public const int                        Y_OFFSET_FROM_CURSOR = -52;


        //================================================================================
        private MacroPlayer                     mMacroPlayer = null;

        private Macro                           mMacro = null;
        private PDFScrapeModel                  mSampleModel = null;
        private PDFReader                       mSamplePDFReader = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroPlayingControlForm(MacroPlayer player) {
            // Initialise
            InitializeComponent();

            // Recorder
            mMacroPlayer = player;
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

            // Player
            mMacroPlayer.Disable();

            // Buttons
            UpdateButtonStates();
        }

        //--------------------------------------------------------------------------------
        public void OnHide() {
            // Player
            mMacroPlayer.Enable();
        }


        // SHOWING ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowDialog(Macro macro, PDFScrapeModel sampleModel, PDFReader samplePDFReader) {
            // Macro / sample model / sample pdf reader
            mMacro = macro;
            mSampleModel = sampleModel;
            mSamplePDFReader = samplePDFReader;

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
        private void btnPlay_Click(object sender, EventArgs e) {
            // Scrape
            ScrapeSet scrapeSet = null;
            if ((mSampleModel != null) && (mSamplePDFReader != null))
                scrapeSet = mSampleModel.Extract(mSamplePDFReader);

            // Play
            mMacroPlayer.Start(mMacro, scrapeSet, 1.0f / Speed, 1.0f / DataInputSpeed);
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnStop_Click(object sender, EventArgs e) {
            // Stop
            mMacroPlayer.Stop();
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnPause_Click(object sender, EventArgs e) {
            // Pause
            mMacroPlayer.Pause();
            UpdateButtonStates();
        }
        
        //--------------------------------------------------------------------------------
        private void btnUnpause_Click(object sender, EventArgs e) {
            // Unpause
            mMacroPlayer.Unpause();
            UpdateButtonStates();
        }

        //--------------------------------------------------------------------------------
        private void UpdateButtonStates() {
            // Play / stop
            btnPlay.Visible = btnPlay.Enabled = !mMacroPlayer.Playing;
            btnStop.Visible = btnStop.Enabled = mMacroPlayer.Playing;

            // Pause / unpause
            btnPause.Visible = !mMacroPlayer.Playing || !mMacroPlayer.Paused;
            btnPause.Enabled = mMacroPlayer.Playing && !mMacroPlayer.Paused;
            btnUnpause.Visible = btnUnpause.Enabled = mMacroPlayer.Playing && mMacroPlayer.Paused;

            // Speed
            trkSpeed.Enabled = btnPlay.Enabled;
            trkDataInputSpeed.Enabled = btnPlay.Enabled;

            // Focus
            Focus();
        }


        // SPEEDS ================================================================================
        //--------------------------------------------------------------------------------
        private void trkSpeed_ValueChanged(object sender, EventArgs e) {
            lblSpeed.Text = "Main (" + Speed.ToString() + "x)";
        }

        //--------------------------------------------------------------------------------
        public float Speed {
            get {
                switch (trkSpeed.Value) {
                    case 0:     return 1.0f;
                    case 1:     return 2.0f;
                    case 2:     return 3.0f;
                    case 3:     return 5.0f;
                    case 4:     return 10.0f;
                    default:    return 1.0f;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void trkDataInputSpeed_ValueChanged(object sender, EventArgs e) {
            lblDataInputSpeed.Text = "Input (" + DataInputSpeed.ToString() + "x)";
        }

        //--------------------------------------------------------------------------------
        public float DataInputSpeed {
            get {
                switch (trkDataInputSpeed.Value) {
                    case 0:     return 1.0f;
                    case 1:     return 10.0f;
                    case 2:     return 100.0f;
                    case 3:     return 1000.0f;
                    default:    return 1.0f;
                }
            }
        }
        
        
        // KEY PRESSES ================================================================================
        //--------------------------------------------------------------------------------
        private void MacroPlayingControlForm_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case '1':
                    // Back
                    btnBack.PerformClick();
                    break;
                case '2':
                    // Play / stop
                    if (btnPlay.Enabled)
                        btnPlay.PerformClick();
                    else if (btnStop.Enabled)
                        btnStop.PerformClick();
                    break;
                case '3':
                    // Pause / unpause
                    if (btnPause.Enabled)
                        btnPause.PerformClick();
                    else if (btnUnpause.Enabled)
                        btnUnpause.PerformClick();
                    break;
            }
        }
    }

}
