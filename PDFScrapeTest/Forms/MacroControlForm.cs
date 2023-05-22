using PDFScrape.Macros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScrapeTest.Forms {

    public partial class MacroControlForm : Form {
        //================================================================================
        private Macro                           mMacro;
        private MacroRecorder                   mRecorder;
        private MacroPlayer                     mPlayer;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroControlForm(Macro macro, MacroRecorder recorder, MacroPlayer player) {
            // Initialise
            InitializeComponent();

            // Macro
            mMacro = macro;
            mRecorder = recorder;
            mRecorder.Macro = macro;
            mPlayer = player;
            mPlayer.Macro = macro;
        }
        
        
        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        public void OnShown() {
            // Pause
            mRecorder.Pause();
            mPlayer.Pause();

            // Position
            Rectangle screenArea = Screen.FromPoint(Cursor.Position).WorkingArea;
            Location = new Point(Math.Min(Math.Max(Cursor.Position.X - (Size.Width / 2), screenArea.Left), screenArea.Right - Size.Width),
                                 Math.Min(Math.Max(Cursor.Position.Y - (Size.Height / 2), screenArea.Top), screenArea.Bottom - Size.Height));

            // Controls
            btnStartRecording.Visible = btnStartRecording.Enabled = !mRecorder.Recording && !mPlayer.Playing;
            btnStopRecording.Enabled = mRecorder.Recording;
            btnStartPlaying.Visible = btnStartPlaying.Enabled = !mRecorder.Recording && !mPlayer.Playing;
            btnStopPlaying.Enabled = mPlayer.Playing;
            btnResume.Enabled = (mRecorder.Recording && mRecorder.Paused) || (mPlayer.Playing && mPlayer.Paused);
            btnAddInput.Enabled = mRecorder.Recording;

            // Status
            if (mRecorder.Recording)
                txtStatus.Text = !mRecorder.Paused ? "Recording..." : "Recording (paused)...";
            else if (mPlayer.Playing)
                txtStatus.Text = !mPlayer.Paused ? "Playing..." : "Playing (paused)...";
            else
                txtStatus.Text = "";
        }
        
        //--------------------------------------------------------------------------------
        private void MacroControlForm_KeyPress(object sender, KeyPressEventArgs e) {
            //MessageBox.Show("K: " + e.KeyChar);
        }
        
        //--------------------------------------------------------------------------------
        private void btnStartRecording_Click(object sender, EventArgs e) {
            // Hide
            Hide();
            
            // Start recording
            mRecorder.Start();
        }
        
        //--------------------------------------------------------------------------------
        private void btnStopRecording_Click(object sender, EventArgs e) {
            // Hide
            Hide();
            
            // Stop recording
            mRecorder.Stop();
            mRecorder.Macro.AddKeyUpsForUnreleasedKeys();
        }
        
        //--------------------------------------------------------------------------------
        private void btnStartPlaying_Click(object sender, EventArgs e) {
            // Hide
            Hide();

            // Start playing
            mPlayer.PlayAsync(Speed);
        }
        
        //--------------------------------------------------------------------------------
        private void btnStopPlaying_Click(object sender, EventArgs e) {
            // Hide
            Hide();

            // Stop playing
            mPlayer.Stop();
        }
        
        //--------------------------------------------------------------------------------
        private void btnResume_Click(object sender, EventArgs e) {
            // Hide
            Hide();

            // Resume
            mRecorder.Unpause();
            mPlayer.Unpause();
        }
        
        //--------------------------------------------------------------------------------
        private void btnAddInput_Click(object sender, EventArgs e) {
            // Hide
            Hide();

            // Input
            mRecorder.AddDataInputEvent("test");
            mRecorder.Unpause();
            mPlayer.Unpause();
            //mRecorder.AddInputEvents("test(123...)");
        }
        
        //--------------------------------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e) {
            // Hide
            Hide();
        }
        
        //--------------------------------------------------------------------------------
        private void trkSpeed_ValueChanged(object sender, EventArgs e) {
            lblSpeed.Text = "Speed (" + Speed + ")";
        }
        

        // SPEED ================================================================================
        //--------------------------------------------------------------------------------
        private float Speed {
            get {
                switch (trkSpeed.Value) {
                    case 0:     return 0.03f;
                    case 1:     return 0.1f;
                    case 2:     return 0.3f;
                    default:    return 1.0f;
                }
            }
        }
    }

}
