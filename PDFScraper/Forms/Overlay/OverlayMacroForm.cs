using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Overlay {

    public partial class OverlayMacroForm : Form {
        //================================================================================
        // May be useful later, if this form doesn't behave on other versions of Windows:
        // https://www.vesic.org/english/blog-eng/net/full-screen-maximize/
        //================================================================================
        private List<OverlayMacroCursorText>    mCursorTexts = new List<OverlayMacroCursorText>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public OverlayMacroForm() {
            // Initialise
            InitializeComponent();

            // Transparency key
            TransparencyKey = Color.Fuchsia;

            // Transparency
            int initialStyle = (int)UWindows.GetWindowLong(this.Handle, UWindows.GWL_EXSTYLE);
            UWindows.SetWindowLong(this.Handle, UWindows.GWL_EXSTYLE, new IntPtr(initialStyle | UWindows.WS_EX_LAYERED | UWindows.WS_EX_TRANSPARENT));
        }


        // TIMER ================================================================================
        //--------------------------------------------------------------------------------
        private void tmrTimer_Tick(object sender, EventArgs e) {
            // Cursor texts - update
            UpdateCursorTexts();

            // Cursor texts - remove expired
            mCursorTexts.RemoveAll(c => c == null);
        }

        
        // DRAWING ================================================================================
        //--------------------------------------------------------------------------------
        private void OverlayMacroForm_Paint(object sender, PaintEventArgs e) {
            // Cursor texts
            foreach (OverlayMacroCursorText c in mCursorTexts) {
                c.Draw(e.Graphics);
            }
        }

        
        // CURSOR TEXTS ================================================================================
        //--------------------------------------------------------------------------------
        public OverlayMacroCursorText AddCursorText(int renderPriority, int x, int y, long duration) {
            OverlayMacroCursorText cursorText = new OverlayMacroCursorText(CreateGraphics(), renderPriority, x, y, duration);
            int insertIndex = mCursorTexts.FindLastIndex(c => c.RenderPriority <= cursorText.RenderPriority) + 1;
            mCursorTexts.Insert(insertIndex, cursorText);
            return cursorText;
        }

        //--------------------------------------------------------------------------------
        public OverlayMacroCursorText AddCursorText(int x, int y, long duration) { return AddCursorText(0, x, y, duration); }
        
        //--------------------------------------------------------------------------------
        public void RemoveAllCursorTexts(bool update = true) {
            if (update)
                UpdateCursorTexts();
            mCursorTexts.Clear();
        }
        
        //--------------------------------------------------------------------------------
        public void RemoveAllCursorTexts(int renderPriority, bool update = true) {
            // Update
            if (update)
                UpdateCursorTexts();

            // Clear
            for (int i = 0; i < mCursorTexts.Count; ++i) {
                if ((mCursorTexts[i] != null) && (mCursorTexts[i].RenderPriority == renderPriority))
                    mCursorTexts[i] = null;
            }

            mCursorTexts.RemoveAll(c => c == null);
        }
        
        //--------------------------------------------------------------------------------
        private void UpdateCursorTexts() {
            // Cursor texts - update
            for (int i = 0; i < mCursorTexts.Count; ++i) {
                if (!mCursorTexts[i].Update(this))
                    mCursorTexts[i] = null;
            }
        }
        
        //--------------------------------------------------------------------------------
        internal int CursorTextReversePosition(OverlayMacroCursorText cursorText, bool allRenderPriorities = false) {
            // Position
            int position = 0;

            for (int i = mCursorTexts.Count - 1; i >= 0; --i) {
                if (mCursorTexts[i] != null) {
                    if (mCursorTexts[i] == cursorText)
                        return position;
                    else if (allRenderPriorities || (mCursorTexts[i].RenderPriority == cursorText.RenderPriority))
                        ++position;
                }
            }

            // Not found
            return -1;
        }
    }

}
