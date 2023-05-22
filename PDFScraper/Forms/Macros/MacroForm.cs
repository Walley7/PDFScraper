using PDFScrape.Exceptions;
using PDFScrape.Hotkeys;
using PDFScrape.Macros;
using PDFScraper.Forms.Model;
using PDFScraper.Forms.Overlay;
using PDFScraper.Forms.Scraper;
using System;
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

    public partial class MacroForm : Form {
        //================================================================================
        public const string                     OVERLAY_MACRO_IMAGE_PATH = "Media/icon_macro_small.png";
        public const string                     OVERLAY_RECORDING_IMAGE_PATH = "Media/icon_macro_recording_small.png";
        public const string                     OVERLAY_PLAYING_IMAGE_PATH = "Media/icon_macro_playing_small.png";
        public const string                     OVERLAY_PAUSED_IMAGE_PATH = "Media/icon_macro_paused_small.png";

        public const int                        OVERLAY_MINIMUM_CURSOR_SIZE = -5; // As small as possible without being noticeable, to absorb initial resize rate
        public const int                        OVERLAY_MAXIMUM_CURSOR_SIZE = 1000; // No upper limit
        public const double                     OVERLAY_CURSOR_RESIZE_RATE = 3;
        public const double                     OVERLAY_POSITIONED_RESIZE_DECAY_FACTOR = 0.9;

        public const string                     OVERLAY_POSITIONED_TEXT_FONT_NAME = "Microsoft Sans Serif";
        public const int                        OVERLAY_POSITIONED_TEXT_FONT_SIZE = 12;
        public const int                        OVERLAY_POSITIONED_TEXT_MARGIN = 17;
        public const double                     OVERLAY_POSITIONED_COLOUR_CHANGE_RATE = 0.015; //3.0 / 256.0;

        public const string                     OVERLAY_CENTRAL_TEXT_FONT_NAME = "Microsoft Sans Serif";
        public const int                        OVERLAY_CENTRAL_TEXT_FONT_SIZE = 24;
        public const double                     OVERLAY_CENTRAL_COLOUR_CHANGE_RATE = 0.015; //3.0 / 256.0;


        //================================================================================
        private LaunchForm                      mLaunchForm;
        private bool                            mReturnToLaunchForm = true;

        private Label                           mLoadingLabel = null;

        private MacroRecorder                   mMacroRecorder = new MacroRecorder();
        private MacroPlayer                     mMacroPlayer = new MacroPlayer();

        private HotkeyRegistrator               mHotkeyRegistrator = new HotkeyRegistrator();

        private MacroRecordingControlForm       mRecordingControlForm;
        private MacroPlayingControlForm         mPlayingControlForm;

        private OverlayMessageForm              mOverlayMessageForm = new OverlayMessageForm();
        private OverlayMacroForm                mOverlayMacroForm = null;

        private int                             mLastCursorX;
        private int                             mLastCursorY;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MacroForm(LaunchForm launchForm) {
            // Initialise
            InitializeComponent();

            // Launch form
            mLaunchForm = launchForm;

            // Theme
            dckMain.Theme = new VS2015DarkTheme();

            // Macro recorder
            mMacroRecorder.KeysToIgnore.Add((int)MacroKey.PAUSE);

            // Recording control form
            mRecordingControlForm = new MacroRecordingControlForm(mMacroRecorder);

            // Playing control form
            mPlayingControlForm = new MacroPlayingControlForm(mMacroPlayer);
        }


        // FORM ================================================================================
        //--------------------------------------------------------------------------------      
        private void MacroForm_Shown(object sender, EventArgs eventArgs) {
            // Hotkeys
            mHotkeyRegistrator.KeyPressed += new EventHandler<HotkeyEvent>(OnHotkeyPressed);
            mHotkeyRegistrator.RegisterHotkey(HotkeyEvent.Modifier.None, Keys.Pause);

            // Recorder
            mMacroRecorder.StartedRecording += (r, n) => OnStartedRecording(r, n);
            mMacroRecorder.StoppedRecording += (r, n) => OnStoppedRecording(r, n);
            mMacroRecorder.PausedRecording += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.UnpausedRecording += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.BeganRepetition += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.EndedRepetition += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.BeganBranching += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.EndedBranching += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.BeganBranch += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.EndedBranch += (r, n) => OnRecordingChange(r, n);
            mMacroRecorder.AddedDataInput += (r, n) => OnRecordingChange(r, n);

            // Player
            mMacroPlayer.StartedPlaying += (p, e) => OnStartedPlaying(p, e);
            mMacroPlayer.StoppedPlaying += (p, e) => InvokeOnStoppedPlaying(p, e);
            mMacroPlayer.PausedPlaying += (p, e) => OnPlayingChange(p, e);
            mMacroPlayer.UnpausedPlaying += (p, e) => OnPlayingChange(p, e);
            mMacroPlayer.PlayingElement += (p, e) => InvokeOnPlayingElement(p, e);
        }
        
        //--------------------------------------------------------------------------------       
        private void MacroForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Unsaved docks
            List<MacroDock> unsavedDocks = new List<MacroDock>();
            foreach (IDockContent d in dckMain.Documents) {
                MacroDock dock = (MacroDock)d;
                if (dock.Macro.HasChanged)
                    unsavedDocks.Add(dock);
            }

            // Save changes
            if (unsavedDocks.Count > 0) {
                string saveChangesText = "Save changes to following macros?";
                unsavedDocks.ForEach(d => saveChangesText += "\n  " + d.Name);

                DialogResult result = MessageBox.Show(saveChangesText, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    // Save
                    foreach (MacroDock d in unsavedDocks) {
                        SaveMacro(d);
                    }
                }
                else if (result == DialogResult.Cancel) {
                    // Cancel
                    e.Cancel = true;
                    mReturnToLaunchForm = true;
                    return;
                }
            }
        }

        //--------------------------------------------------------------------------------       
        private void MacroForm_FormClosed(object sender, FormClosedEventArgs e) {
            // Dispose
            mHotkeyRegistrator.Dispose();
            mMacroPlayer.Dispose();
            mMacroRecorder.Dispose();
            mOverlayMessageForm.Dispose();
            HideMacroOverlay();

            // Launch form
            if (mReturnToLaunchForm) {
                mLaunchForm.MoveToCentre();
                mLaunchForm.Show();
            }
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        private void OnHotkeyPressed(object sender, HotkeyEvent e) {
            // Checks
            if (dckMain.ActiveDocument == null || !(dckMain.ActiveDocument is MacroDock))
                return;
            if (mRecordingControlForm.Visible)
                return;

            // Macro dock
            MacroDock macroDock = (MacroDock)dckMain.ActiveDocument;
            if ((mMacroRecorder.Recording && mMacroRecorder.Macro != macroDock.Macro) /*|| (mMacroPlayer.Playing && mMacroPlay.Macro != macroDock.Macro)*/) {
                MessageBox.Show("Cannot record and/or play two macros at once - please finish recording/playing the current macro.", "Cannot Record/Play", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Control form
            if (macroDock.ModeIsRecording)
                mRecordingControlForm.ShowDialog(macroDock.Macro, macroDock.SampleModel);
            else if (macroDock.ModeIsTesting)
                mPlayingControlForm.ShowDialog(macroDock.Macro, macroDock.SampleModel, macroDock.SamplePDFReader);
        }

        //--------------------------------------------------------------------------------
        private void OnStartedRecording(MacroRecorder recorder, MacroNode node) {
            // Controls
            SetDocksEnabled(false);

            // Message overlay
            ShowMessageOverlay(recorder.Macro.Name + ": Recording (depth " + recorder.NodeDepth + ")", OVERLAY_RECORDING_IMAGE_PATH);
        }

        //--------------------------------------------------------------------------------
        private void OnStoppedRecording(MacroRecorder recorder, MacroNode node) {
            // Message overlay
            HideMessageOverlay();

            // Controls
            SetDocksEnabled(true);
            MacroDock(node.Macro).RefreshControls();
        }

        //--------------------------------------------------------------------------------
        private void OnRecordingChange(MacroRecorder recorder, MacroNode node) {
            // Checks
            if (!recorder.Recording)
                return;

            // State
            if (recorder.Paused)
                ShowMessageOverlay(recorder.Macro.Name + ": Paused", OVERLAY_PAUSED_IMAGE_PATH);
            else if (recorder.InRepetition)
                ShowMessageOverlay(recorder.Macro.Name + ": Recording Repetition (depth " + recorder.NodeDepth + ")", OVERLAY_RECORDING_IMAGE_PATH);
            else if (recorder.InBranching)
                ShowMessageOverlay(recorder.Macro.Name + ": Recording Branching (depth " + recorder.NodeDepth + ")", OVERLAY_RECORDING_IMAGE_PATH);
            else if (recorder.InBranch)
                ShowMessageOverlay(recorder.Macro.Name + ": Recording Branch (depth " + recorder.NodeDepth + ")", OVERLAY_RECORDING_IMAGE_PATH);
            else
                ShowMessageOverlay(recorder.Macro.Name + ": Recording (depth " + recorder.NodeDepth + ")", OVERLAY_RECORDING_IMAGE_PATH);
        }

        //--------------------------------------------------------------------------------
        private void OnStartedPlaying(MacroPlayer player, MacroElement element) {
            // Controls
            SetDocksEnabled(false);

            // Message overlay
            ShowMessageOverlay(player.Macro.Name + ": Testing", OVERLAY_PLAYING_IMAGE_PATH);

            // Macro overlay
            ShowMacroOverlay();
        }

        //--------------------------------------------------------------------------------
        private void InvokeOnStoppedPlaying(MacroPlayer player, MacroElement element) {
            BeginInvoke((MethodInvoker)delegate { OnStoppedPlaying(player, element); }); // Begin invoke doesn't wait for completion
        }

        //--------------------------------------------------------------------------------
        private void OnStoppedPlaying(MacroPlayer player, MacroElement element) {
            // Overlays
            HideMessageOverlay();
            HideMacroOverlay();

            // Controls
            SetDocksEnabled(true);
            //MacroDock(element.Macro).RefreshControls();
        }

        //--------------------------------------------------------------------------------
        private void OnPlayingChange(MacroPlayer player, MacroElement element) {
            // Checks
            if (!player.Playing)
                return;

            // State
            if (player.Paused)
                ShowMessageOverlay(player.Macro.Name + ": Paused", OVERLAY_PAUSED_IMAGE_PATH);
            else
                ShowMessageOverlay(player.Macro.Name + ": Testing", OVERLAY_PLAYING_IMAGE_PATH);
        }

        //--------------------------------------------------------------------------------
        private void InvokeOnPlayingElement(MacroPlayer player, MacroElement element) {
            BeginInvoke((MethodInvoker)delegate { OnPlayingElement(player, element); });
        }

        //--------------------------------------------------------------------------------
        private void OnPlayingElement(MacroPlayer player, MacroElement element) {  
            if (element.Overlay == MacroElement.OverlayType.POSITIONED)
                AddMacroOverlayPositionedCursorText((int)element.OverlayX, (int)element.OverlayY, element.OverlayMessage, element.OverlayColour1, element.OverlayColour2, element.OverlayDuration);
            else if (element.Overlay == MacroElement.OverlayType.CENTRAL)
                AddMacroOverlayCentralText(element.OverlayMessage, element.OverlayColour1, element.OverlayColour2, element.OverlayDuration);
        }


        // NAVIGATION BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnScraper_Click(object sender, EventArgs e) {
            // Checks
            if (CheckMacroIsPlayingOrRecording())
                return;
            
            // Close
            mReturnToLaunchForm = false;
            Close();

            // Scraper
            if (IsDisposed) {
                ScraperForm form = new ScraperForm(mLaunchForm);
                form.Show();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnModelEditor_Click(object sender, EventArgs e) {
            // Checks
            if (CheckMacroIsPlayingOrRecording())
                return;

            // Close
            mReturnToLaunchForm = false;
            Close();

            // Model editor
            if (IsDisposed) {
                ModelForm form = new ModelForm(mLaunchForm);
                form.Show();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnMacroEditor_Click(object sender, EventArgs e) {
            // Checks
            if (CheckMacroIsPlayingOrRecording())
                return;
            
            // Close
            mReturnToLaunchForm = false;
            Close();

            // Macro editor
            if (IsDisposed) {
                MacroForm form = new MacroForm(mLaunchForm);
                form.Show();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e) {
            // Checks
            if (CheckMacroIsPlayingOrRecording())
                return;

            // Exit
            Close();
        }


        // DOCUMENT BUTTONS ================================================================================
        //------------------------ --------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e) {
            if (CheckMacroIsPlayingOrRecording())
                return;
            NewMacro();
        }

        //--------------------------------------------------------------------------------
        private void btnOpen_Click(object sender, EventArgs e) {
            if (CheckMacroIsPlayingOrRecording())
                return;
            OpenMacro();
        }

        //--------------------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e) {
            if (CheckMacroIsPlayingOrRecording())
                return;
            SaveMacro();
        }

        //--------------------------------------------------------------------------------
        private void btnSaveAs_Click(object sender, EventArgs e) {
            if (CheckMacroIsPlayingOrRecording())
                return;
            SaveMacro(true);
        }


        // MACROS ================================================================================
        //--------------------------------------------------------------------------------
        public void NewMacro() {
            // Name
            string name = "";
            int i = 1;
            while (true) {
                name = "Untitled " + i;

                bool foundName = false;
                foreach (IDockContent d in dckMain.Documents) {
                    if (((MacroDock)d).Macro.Name.Equals(name)) {
                        foundName = true;
                        break;
                    }
                }

                if (!foundName)
                    break;
                ++i;
            }

            // Create / open
            OpenMacroDock(new Macro(null, name));
        }

        //--------------------------------------------------------------------------------
        public void OpenMacro() {
            // Path
            if (dlgOpenMacro.ShowDialog() != DialogResult.OK)
                return;

            // Existing documents
            foreach (DockContent d in dckMain.Contents) {
                MacroDock dock = (MacroDock)d;
                if (dock.Path.Equals(dlgOpenMacro.FileName)) {
                    d.Activate();
                    return;
                }
            }

            // Macro
            Macro macro;
            try { macro = new Macro(dlgOpenMacro.FileName); }
            catch (Exception e) {
                MessageBox.Show("Failed to open: " + e.Message, "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Dock
            ShowLoadingLabel("Loading Macro...");
            OpenMacroDock(macro, dlgOpenMacro.FileName);
            HideLoadingLabel();
        }

        //--------------------------------------------------------------------------------
        public void SaveMacro(bool saveAs = false) {
            // Checks
            if (dckMain.ActiveDocument == null || !(dckMain.ActiveDocument is MacroDock))
                return;

            // Save
            MacroDock macroDock = (MacroDock)dckMain.ActiveDocument;
            SaveMacro(macroDock, saveAs);
        }

        //--------------------------------------------------------------------------------
        public void SaveMacro(MacroDock macroDock, bool saveAs = false) {
            // Save as
            if (saveAs || macroDock.HasNoPath) {
                if (dlgSaveMacro.ShowDialog() != DialogResult.OK)
                    return;
                macroDock.Path = dlgSaveMacro.FileName;
            }

            // Save
            try { macroDock.Macro.SaveToJSON(macroDock.Path); }
            catch (Exception e) { MessageBox.Show("Failed to save: " + e.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            // Title
            macroDock.UpdateTitle();
        }


        // MACRO DOCKS ================================================================================
        //--------------------------------------------------------------------------------
        private void OpenMacroDock(Macro macro, string path = "") {
            MacroDock dock = new MacroDock(this, macro, path);
            dock.Show(dckMain, DockState.Document);
        }
        
        //--------------------------------------------------------------------------------
        private MacroDock MacroDock(MacroNode node) {
            foreach (IDockContent d in dckMain.Documents) {
                if (((MacroDock)d).Macro == node)
                    return (MacroDock)d;
            }
            return null;
        }

        //--------------------------------------------------------------------------------
        private void SetDocksEnabled(bool enabled) {
            foreach (IDockContent d in dckMain.Documents) {
                //((MacroDock)d).Enabled = enabled; // This causes problems with updating of the active document
                ((MacroDock)d).SetControlsEnabled(enabled);
            }
        }

        //--------------------------------------------------------------------------------
        private void RefreshDockControls() {
            foreach (IDockContent d in dckMain.Documents) {
                ((MacroDock)d).RefreshControls();
            }
        }


        // PLAYING / RECORDING ================================================================================
        //--------------------------------------------------------------------------------
        public bool CheckMacroIsPlayingOrRecording() {
            if (mMacroRecorder.Recording || mMacroPlayer.Playing) {
                MessageBox.Show("Please finish recording/playing the current macro.", "Finish Recording/Playing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
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


        // OVERLAY MESSAGE ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowMacroOverlay() {
            //mOverlayMacroForm.RemoveAllLowerCursorTexts();
            //mOverlayMacroForm.RemoveAllCursorTexts();
            mLastCursorX = Screen.PrimaryScreen.WorkingArea.Right / 2;
            mLastCursorY = Screen.PrimaryScreen.WorkingArea.Bottom / 2;
            mOverlayMacroForm = new OverlayMacroForm();
            mOverlayMacroForm.Show();
        }

        //--------------------------------------------------------------------------------
        public void HideMacroOverlay() {
            if (mOverlayMacroForm != null) {
                mOverlayMacroForm.Hide();
                mOverlayMacroForm.Dispose();
                mOverlayMacroForm = null;
            }
        }

        //--------------------------------------------------------------------------------
        public void AddMacroOverlayPositionedCursorText(int cursorX, int cursorY, string text, Color colour1, Color colour2, long duration = 2 * TimeSpan.TicksPerSecond) {
            // Add
            OverlayMacroCursorText cursorText = mOverlayMacroForm.AddCursorText(cursorX, cursorY, duration);
            mLastCursorX = cursorX;
            mLastCursorY = cursorY;

            // Cursor
            cursorText.ShowCursor(OVERLAY_MINIMUM_CURSOR_SIZE, OVERLAY_MAXIMUM_CURSOR_SIZE, OVERLAY_CURSOR_RESIZE_RATE);

            // Text
            if (text != null)
                cursorText.ShowText(text, new Font(OVERLAY_POSITIONED_TEXT_FONT_NAME, OVERLAY_POSITIONED_TEXT_FONT_SIZE), OVERLAY_POSITIONED_TEXT_MARGIN);

            // Colour
            cursorText.ColourChangeRate = OVERLAY_POSITIONED_COLOUR_CHANGE_RATE;
            cursorText.CursorResizeDecayFactor = OVERLAY_POSITIONED_RESIZE_DECAY_FACTOR;
            cursorText.Colour1 = colour1;
            cursorText.Colour2 = colour2;
            cursorText.OneWayColourChange = true;
            cursorText.ColourDimming = true;
        }

        //--------------------------------------------------------------------------------
        public void AddMacroOverlayPositionedText(int cursorX, int cursorY, string text, Color colour1, Color colour2, long duration = 2 * TimeSpan.TicksPerSecond) {
            // Add
            OverlayMacroCursorText cursorText = mOverlayMacroForm.AddCursorText(cursorX, cursorY, duration);

            // Text
            if (text != null)
                cursorText.ShowText(text, new Font(OVERLAY_POSITIONED_TEXT_FONT_NAME, OVERLAY_POSITIONED_TEXT_FONT_SIZE), 0);

            // Colour
            cursorText.ColourChangeRate = OVERLAY_POSITIONED_COLOUR_CHANGE_RATE;
            cursorText.CursorResizeDecayFactor = OVERLAY_POSITIONED_RESIZE_DECAY_FACTOR;
            cursorText.Colour1 = colour1;
            cursorText.Colour2 = colour2;
            cursorText.OneWayColourChange = true;
            cursorText.ColourDimming = true;
        }

        //--------------------------------------------------------------------------------
        public void AddMacroOverlayPositionedText(string text, Color colour1, Color colour2, long duration = 2 * TimeSpan.TicksPerSecond) {
            AddMacroOverlayPositionedCursorText(mLastCursorX, mLastCursorY, text, colour1, colour2, duration);
        }

        //--------------------------------------------------------------------------------
        public void AddMacroOverlayCentralText(string text, Color colour1, Color colour2, long duration = 2 * TimeSpan.TicksPerSecond) {
            // Remove existing
            mOverlayMacroForm.RemoveAllCursorTexts(-1);

            // Add
            OverlayMacroCursorText cursorText = mOverlayMacroForm.AddCursorText(-1, Screen.PrimaryScreen.WorkingArea.Right / 2,
                                                                                Screen.PrimaryScreen.WorkingArea.Bottom - 35 - 25, duration); //Screen.PrimaryScreen.WorkingArea.Bottom * 1 / 4, duration);

            // Text
            if (text != null) {
                cursorText.ShowText(text, new Font(OVERLAY_CENTRAL_TEXT_FONT_NAME, OVERLAY_CENTRAL_TEXT_FONT_SIZE), 0);
                cursorText.X = cursorText.X - (int)Math.Round(cursorText.TextDimensions.Width / 2);
            }

            // Colour
            cursorText.ColourChangeRate = OVERLAY_CENTRAL_COLOUR_CHANGE_RATE;
            cursorText.Colour1 = colour1;
            cursorText.Colour2 = colour2;
            cursorText.OneWayColourChange = true;
            cursorText.ColourDimming = true;
        }


        // OVERLAY MESSAGE ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowMessageOverlay(string message, string imagePath = OVERLAY_MACRO_IMAGE_PATH) {
            mOverlayMessageForm.Message = message;
            mOverlayMessageForm.Image = (imagePath != null ? Image.FromFile(imagePath) : null);
            mOverlayMessageForm.Show();
        }
        
        //--------------------------------------------------------------------------------
        public void HideMessageOverlay() {
            mOverlayMessageForm.Hide();
        }
        
        //--------------------------------------------------------------------------------
        public bool MessageOverlayShowing { get { return mOverlayMessageForm.Visible; } }
    }

}
