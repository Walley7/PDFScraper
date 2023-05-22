using PDFScrape.Hotkeys;
using PDFScrape.Macros;
using Silence.Simulation.Native;
//using Silence.Macro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;



namespace PDFScrapeTest.Forms {

    public partial class MacroDock : DockContent {
        //================================================================================
        private HotkeyRegistrator               mHotkeyRegistrator = new HotkeyRegistrator();

        private Macro                           mMacro = new Macro();
        private MacroRecorder                   mMacroRecorder = new MacroRecorder();
        private MacroPlayer                     mMacroPlayer = new MacroPlayer();

        private MacroControlForm                mMacroControlForm;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroDock() {
            // Initialise
            InitializeComponent();
            
            // Hotkeys
            mHotkeyRegistrator.KeyPressed += new EventHandler<HotkeyEvent>(OnHotkeyPressed);
            //mHotkeyRegistrator.RegisterHotkey(HotkeyEvent.Modifier.Ctrl | HotkeyEvent.Modifier.Shift, Keys.Q);
            mHotkeyRegistrator.RegisterHotkey(HotkeyEvent.Modifier.None, Keys.Pause);

            // Macros
            mMacroRecorder.KeysToIgnore.Add((int)MacroKey.PAUSE);

            // Macro control form
            mMacroControlForm = new MacroControlForm(mMacro, mMacroRecorder, mMacroPlayer);
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnHotkeyPressed(object sender, HotkeyEvent e) {
            // Pause
            mMacroRecorder.Pause();
            mMacroPlayer.Pause();

            // Control form
            mMacroControlForm.Show();
            mMacroControlForm.OnShown();
            mMacroControlForm.Activate();
        }
        

        // TIMER ================================================================================
        //--------------------------------------------------------------------------------
        private void tmrOverlayDrawing_Tick(object sender, EventArgs e) {
            //Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            //graphics.DrawEllipse(Pens.Green, Cursor.Position.X - 10, Cursor.Position.Y - 10, 20, 20);
        }
        

        // SCREENSHOTS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnScreenshot_Click(object sender, EventArgs e) {
            // Screen
            Rectangle screenArea = Screen.PrimaryScreen.Bounds;
            
            // Screenshot
            Bitmap screenshot = new Bitmap(screenArea.Width, screenArea.Height, PixelFormat.Format32bppArgb);
            Graphics screenshotGraphics = Graphics.FromImage(screenshot);
            screenshotGraphics.CopyFromScreen(screenArea.Left, screenArea.Top, 0, 0, screenArea.Size);
            picScreenshot.Image = screenshot;
        }
        

        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public Macro Macro { get { return mMacro; } }
        public MacroRecorder MacroRecorder { get { return mMacroRecorder; } }
        public MacroPlayer MacroPlayer { get { return mMacroPlayer; } }
    }

}
