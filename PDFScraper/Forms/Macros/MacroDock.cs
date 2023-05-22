using PDFScrape.Hotkeys;
using PDFScrape.Macros;
using PDFScrape.Macros.Events;
using PDFScrape.Macros.Presentation;
using PDFScrape.PDF;
using PDFScraper.Forms.Overlay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;



namespace PDFScraper.Forms.Macros {

    public partial class MacroDock : DockContent {
        //================================================================================
        public const int                        PDF_DPI = 96;

        //--------------------------------------------------------------------------------
        public enum MacroMode {
            NONE,
            RECORDING,
            TESTING
        }

        
        //================================================================================
        private MacroForm                       mForm;

        private Macro                           mMacro;
        private string                          mPath = "";
        private Macro.EventDelegate             mMacroChangedDelegate = null;

        private MacroMode                       mMode = MacroMode.NONE;

        private PDFScrapeModel                  mSampleModel = null;

        private PDFReader                       mPDFReader = null;

        private Label                           mLoadingLabel = null;
        
        private bool                            mFloatPaneExists = false;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroDock(MacroForm form, Macro macro, string path = "") {
            // Initialise
            InitializeComponent();

            // Form
            mForm = form;

            // Macro
            mMacro = macro;
            mPath = path;
            if (string.IsNullOrEmpty(mPath))
                mMacro.IncrementChangeCount(); // Mark new macro as changed

            // Title
            UpdateTitle();

            // Fields
            txtSampleModelPath.DataBindings.Add("Text", mMacro, "SampleModelPath", false, DataSourceUpdateMode.OnPropertyChanged);
            txtSamplePDFPath.DataBindings.Add("Text", mMacro, "SamplePDFPath", false, DataSourceUpdateMode.OnPropertyChanged);
        }


        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void MacroDock_Shown(object sender, EventArgs e) {
            // Macro events
            mMacroChangedDelegate = (m) => OnMacroChanged(m);
            mMacro.Changed += mMacroChangedDelegate;

            // Mode
            SetMode(MacroMode.RECORDING);

            // Refresh 
            RefreshControls();
        }

        //--------------------------------------------------------------------------------
        private void MacroDock_FormClosing(object sender, FormClosingEventArgs e) {
            // Playing / recording
            if (mForm.CheckMacroIsPlayingOrRecording()) {
                e.Cancel = true;
                return;
            }

            // Save changes
            if (e.CloseReason == CloseReason.UserClosing) {
                if (mMacro.HasChanged) {
                    DialogResult result = MessageBox.Show("Save changes to following macros?\n  " + mMacro.Name, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        mForm.SaveMacro(this);
                    else if (result == DialogResult.Cancel) {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void MacroDock_FormClosed(object sender, FormClosedEventArgs e) {
            // Macro events
            mMacro.Changed -= mMacroChangedDelegate;
            mMacroChangedDelegate = null;

            // Dispose
            if (mPDFReader != null)
                mPDFReader.Dispose();
        }
        
        //--------------------------------------------------------------------------------
        private void MacroDock_DockStateChanged(object sender, EventArgs e) {
            if (!mFloatPaneExists && !(FloatPane is null)) {
                mFloatPaneExists = true;
                FloatPane.FloatWindow.Width = (int)((double)Width * 0.75);
                FloatPane.FloatWindow.Height = (int)((double)Height * 0.75);
            }
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnMacroChanged(Macro macro) {
            UpdateTitle();
        }


        // TITLE ================================================================================
        //--------------------------------------------------------------------------------
        public void UpdateTitle() {
            Text = (mMacro.HasChanged ? "*" : "") + mMacro.Name;
        }


        // MACRO ================================================================================
        //--------------------------------------------------------------------------------
        public Macro Macro { get { return mMacro; } }

        //--------------------------------------------------------------------------------
        public string Path {
            set { mPath = value; }
            get { return mPath; }
        }

        //--------------------------------------------------------------------------------
        public bool HasNoPath { get { return string.IsNullOrWhiteSpace(mPath); } }

        //--------------------------------------------------------------------------------
        private void RefreshMacroGrid() {
            // Refresh
            ShowLoadingLabel("Refreshing...");
            dgvMacro.Rows.Clear();
            AddMacroNodeChildrenToGrid(mMacro);
            HideLoadingLabel();

            MacroPresentationNode presentationNode = new MacroPresentationNode(mMacro);

            tlvMacro.CanExpandGetter = delegate(object o) { return ((MacroPresentationNode)o).HasChildren; };
            tlvMacro.ChildrenGetter = delegate (object o) { return ((MacroPresentationNode)o).Children; };

            List<MacroPresentationNode> macroRoots = new List<MacroPresentationNode>();
            macroRoots.Add(presentationNode);
            tlvMacro.Roots = macroRoots;

            tlvMacro.Expand(presentationNode);
        }
        
        //--------------------------------------------------------------------------------
        private void tlvMacro_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e) {
            MacroPresentationNode node = (MacroPresentationNode)e.Model;
            e.Item.BackColor = node.Colour;
        }
        
        //--------------------------------------------------------------------------------
        private void tlvMacro_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e) {
            if (e.ColumnIndex == 0)
                e.Item.Font = new Font(e.Item.Font, FontStyle.Bold);
        }

        //--------------------------------------------------------------------------------
        private void AddMacroElementToGrid(MacroElement element, long delay, string indent = "") {
            // Row
            dgvMacro.Rows.Add(indent + element.Information[0], String.Format("{0:0.00}", (delay / 10000.0)) + " ms   ", element.Information[1]);

            // Style
            dgvMacro.Rows[dgvMacro.Rows.Count - 1].DefaultCellStyle.BackColor = element.Colour;
            dgvMacro.Rows[dgvMacro.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;

            // Children
            if (element.IsNode)
                AddMacroNodeChildrenToGrid((MacroNode)element, indent + "  ");
        }

        //--------------------------------------------------------------------------------
        private void AddMacroMouseMoveEventToGrid(MacroMouseEvent startEvent, MacroMouseEvent endEvent, long delay, string indent = "") {
            // Parameters
            string parameters = "x1: " + startEvent.Position.X + ", y1: " + startEvent.Position.Y + ", x2: " + endEvent.Position.X + ", y2: " + endEvent.Position.Y;

            // Row
            dgvMacro.Rows.Add(indent + startEvent.Information[0], String.Format("{0:0.00}", (delay / 10000.0)) + " ms   ", parameters);
            dgvMacro.Rows[dgvMacro.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(191, 191, 191);
            dgvMacro.Rows[dgvMacro.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
        }

        //--------------------------------------------------------------------------------
        private void AddMacroNodeChildrenToGrid(MacroNode node, string indent = "") {
            // Tracking
            MacroMouseEvent startMouseMoveEvent = null;
            MacroMouseEvent endMouseMoveEvent = null;
            long previousDelay = 0;
            long delay = 0;

            // Elements
            for (int i = 0; i < node.ElementCount; ++i) {
                MacroElement element = node.Element(i);

                // Mouse move start event
                if ((element is MacroMouseEvent) && ((MacroMouseEvent)element).IsMoveAction) {
                    if (startMouseMoveEvent == null)
                        startMouseMoveEvent = (MacroMouseEvent)element;
                    endMouseMoveEvent = (MacroMouseEvent)element;
                }

                // Delays
                if (element is MacroDelayEvent) {
                    previousDelay = delay;
                    delay += ((MacroDelayEvent)element).Delay;
                }

                // Add
                if (!((element is MacroMouseEvent) && ((MacroMouseEvent)element).IsMoveAction) && !(element is MacroDelayEvent)) {
                    // Mouse event
                    if (startMouseMoveEvent != null) {
                        AddMacroMouseMoveEventToGrid(startMouseMoveEvent, endMouseMoveEvent, previousDelay, indent);
                        startMouseMoveEvent = null;
                        endMouseMoveEvent = null;
                        delay = Math.Max(delay - previousDelay, 0);
                    }

                    // Element
                    AddMacroElementToGrid(node.Element(i), delay, indent);
                    previousDelay = 0;
                    delay = 0;
                }
            }

            // Remaining mouse event
            if (startMouseMoveEvent != null)
                AddMacroMouseMoveEventToGrid(startMouseMoveEvent, endMouseMoveEvent, delay, indent);
        }


        // MODE ================================================================================
        //--------------------------------------------------------------------------------
        public void SetMode(MacroMode mode) {
            // Checks
            if (mMode == mode)
                return;

            // Set
            mMode = mode;

            // UI - reset
            btnRecordMode.FlatAppearance.BorderSize = 1;
            btnRecordMode.FlatAppearance.BorderColor = SystemColors.ControlDark;
            btnRecordMode.BackColor = SystemColors.ControlLight;
            btnTestMode.FlatAppearance.BorderSize = 1;
            btnTestMode.FlatAppearance.BorderColor = SystemColors.ControlDark;
            btnTestMode.BackColor = SystemColors.ControlLight;

            // UI
            switch (mMode) {
                case MacroMode.RECORDING:
                    btnRecordMode.FlatAppearance.BorderSize = 2;
                    btnRecordMode.FlatAppearance.BorderColor = Color.Maroon;
                    btnRecordMode.BackColor = Color.LightCoral;
                    break;

                case MacroMode.TESTING:
                    btnTestMode.FlatAppearance.BorderSize = 2;
                    btnTestMode.FlatAppearance.BorderColor = Color.Teal;
                    btnTestMode.BackColor = Color.Aquamarine;
                    break;
            }
        }

        //--------------------------------------------------------------------------------
        public MacroMode Mode { get { return mMode; } }
        public bool ModeIsRecording { get { return (mMode == MacroMode.RECORDING); } }
        public bool ModeIsTesting { get { return (mMode == MacroMode.TESTING); } }


        // SAMPLE MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel SampleModel { get { return mSampleModel; } }

        //--------------------------------------------------------------------------------
        private void txtSampleModelPath_TextChanged(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtSampleModelPath.Text)) {
                //if (mSampleModel != null)
                //    mSampleModel.Dispose();
                
                // Load
                ShowLoadingLabel("Loading Model...");
                try { mSampleModel = new PDFScrapeModel(txtSampleModelPath.Text); }
                catch (Exception ex) {
                    MessageBox.Show("Failed to open: " + ex.Message, "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                HideLoadingLabel();
            }
        }

        //--------------------------------------------------------------------------------
        private void btnSelectSampleModel_Click(object sender, EventArgs e) {
            if (dlgOpenModel.ShowDialog() == DialogResult.OK)
                txtSampleModelPath.Text = dlgOpenModel.FileName;
        }
        
        
        // SAMPLE PDF ================================================================================
        //--------------------------------------------------------------------------------
        public PDFReader SamplePDFReader { get { return mPDFReader; } }

        //--------------------------------------------------------------------------------
        private void txtSamplePDFPath_TextChanged(object sender, EventArgs e) {
            // Dispose
            if (mPDFReader != null)
                mPDFReader.Dispose();

            // Read
            if (!string.IsNullOrEmpty(txtSamplePDFPath.Text)) {
                ShowLoadingLabel("Loading PDF...");
                mPDFReader = new PDFReader(txtSamplePDFPath.Text, PDF_DPI);
                HideLoadingLabel();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnSelectSamplePDF_Click(object sender, EventArgs e) {
            if (dlgOpenPDF.ShowDialog() == DialogResult.OK)
                txtSamplePDFPath.Text = dlgOpenPDF.FileName;
        }


        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        public void SetControlsEnabled(bool enabled) {
            tlpMacro.Enabled = enabled;
        }

        //--------------------------------------------------------------------------------
        public void RefreshControls() {
            RefreshMacroGrid();
        }


        // LOADING LABEL ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowLoadingLabel(string text, int height = 150) {
            // Hide
            HideLoadingLabel();

            // Create
            mLoadingLabel = new Label() { Text = text, BorderStyle = BorderStyle.FixedSingle, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Height = height };
            mLoadingLabel.Font = new Font(mLoadingLabel.Font.FontFamily, 48, FontStyle.Bold);
            mLoadingLabel.Top = (Height / 2) - (mLoadingLabel.Height / 2);
            mLoadingLabel.Width = Width;

            // Show
            Controls.Add(mLoadingLabel);
            mLoadingLabel.BringToFront();
            mLoadingLabel.Refresh();
        }
        
        //--------------------------------------------------------------------------------
        public void HideLoadingLabel() {
            if (mLoadingLabel != null) {
                Controls.Remove(mLoadingLabel);
                mLoadingLabel.Dispose();
                mLoadingLabel = null;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnRecordMode_Click(object sender, EventArgs e) {
            SetMode(MacroMode.RECORDING);
            ActiveControl = null;
        }
        
        //--------------------------------------------------------------------------------
        private void btnTestMode_Click(object sender, EventArgs e) {
            SetMode(MacroMode.TESTING);
            ActiveControl = null;
        }
    }

}
