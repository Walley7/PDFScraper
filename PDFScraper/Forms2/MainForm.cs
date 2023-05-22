using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using PDFScraper.Forms2.Docks;
using DevExpress.XtraSplashScreen;
using System.IO;
using PDFScrape.PDF;
using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.Images;



namespace PDFScraper.Forms2 {

    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        //================================================================================
        //--------------------------------------------------------------------------------
        enum ScraperFileType {
            MODEL,
            MACRO
        }
        
        //--------------------------------------------------------------------------------
        public const string                     MODEL_ICON_NAME = "images/export/exporttopdf_16x16.png";
        public const string                     MACRO_ICON_NAME = "images/mode/mousemode_16x16.png";
        public const string                     SCRAPER_ICON_NAME = "images/actions/merge_16x16.png";
        public static readonly Size             DOCK_MINIMUM_FLOAT_SIZE = new Size(200, 200);


        //================================================================================
        private IOverlaySplashScreenHandle      mWaitOverlayHandle = null;

            
        //================================================================================
        //--------------------------------------------------------------------------------
        public MainForm() {
            InitializeComponent();
        }


        // EVENTS - MENU ================================================================================
        //--------------------------------------------------------------------------------
        private void btnMenu_New_Model_ItemClick(object sender, ItemClickEventArgs e) { NewModel(); }
        private void btnMenu_New_Macro_ItemClick(object sender, ItemClickEventArgs e) { NewMacro(); }
        private void btnMenu_New_Scraper_ItemClick(object sender, ItemClickEventArgs e) { NewScraper(); }
        private void btnMenu_Open_ItemClick(object sender, ItemClickEventArgs e) { OpenDocument(); }
        private void btnMenu_Save_ItemClick(object sender, ItemClickEventArgs e) { SaveDocument(); }
        private void btnMenu_SaveAs_ItemClick(object sender, ItemClickEventArgs e) { SaveDocument(true); }
        private void mnuMenu_Exit_ItemClick(object sender, ItemClickEventArgs e) { Close(); }


        // EVENTS - RIBBON ================================================================================
        //--------------------------------------------------------------------------------
        private void btnRibbon_File_New_ItemClick(object sender, ItemClickEventArgs e) { mnuNew.ShowPopup(new Point(Cursor.Position.X - 14, Cursor.Position.Y - 12)); }
        private void btnRibbon_File_Open_ItemClick(object sender, ItemClickEventArgs e) { OpenDocument(); }
        private void btnRibbon_File_Save_ItemClick(object sender, ItemClickEventArgs e) { SaveDocument(); }
        private void btnRibbon_File_SaveAs_ItemClick(object sender, ItemClickEventArgs e) { SaveDocument(true); }


        // EVENTS - NEW DOCUMENT ================================================================================
        //--------------------------------------------------------------------------------
        private void btnNew_Model_ItemClick(object sender, ItemClickEventArgs e) { NewModel(); }
        private void btnNew_Macro_ItemClick(object sender, ItemClickEventArgs e) { NewMacro(); }
        private void btnNew_Scraper_ItemClick(object sender, ItemClickEventArgs e) { NewScraper(); }


        // EVENTS - CLOSING ================================================================================
        //--------------------------------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Unsaved docks
            List<DockControl> unsavedDocks = UnsavedDocks();
            if (unsavedDocks.Count > 0) {
                string saveChangesText = "Save changes to the following documents?";
                unsavedDocks.ForEach(d => saveChangesText += "\n  " + d.DocumentName);

                DialogResult result = XtraMessageBox.Show(saveChangesText, "Chase Verification Tool", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    // Save
                    foreach (DockControl d in unsavedDocks) {
                        SaveDocument(d);
                    }
                }
                else if (result == DialogResult.Cancel) {
                    // Cancel
                    e.Cancel = true;
                    return;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) { }

        //--------------------------------------------------------------------------------
        private void mgrDockManager_ClosingPanel(object sender, DockPanelCancelEventArgs e) {
            // Dock
            DockControl dock = DockControl(e.Panel);

            // Save changes
            if (dock.HasChanged) {
                DialogResult result = XtraMessageBox.Show("Save changes to the following documents?\n  " + dock.DocumentName, "Chase Verification Tool", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    SaveDocument(dock);
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void mgrDockManager_ClosedPanel(object sender, DockPanelEventArgs e) {
            mgrDockManager.RemovePanel(e.Panel);
        }


        // DOCUMENTS ================================================================================
        //--------------------------------------------------------------------------------
        public void OpenDocument(string path) {
            switch (FileType(path)) {
                case ScraperFileType.MODEL:
                    OpenModel(path);
                    break;
                case ScraperFileType.MACRO:
                    OpenMacro(path);
                    break;
            }
        }

        //--------------------------------------------------------------------------------
        public void OpenDocument() {
            if (dlgOpenFile.ShowDialog() != DialogResult.OK)
                return;
            OpenDocument(dlgOpenFile.FileName);
        }

        //--------------------------------------------------------------------------------
        public void SaveDocument(DockControl dock, string path) {
            // Path
            dock.Path = path;

            // Save
            try { dock.Save(dock.Path); }
            catch (Exception e) { XtraMessageBox.Show("Failed to save: " + e.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            // Title
            dock.UpdateTitle();
        }
        
        //--------------------------------------------------------------------------------
        public void SaveDocument(DockControl dock, bool saveAs = false) {
            // Save dialog
            XtraSaveFileDialog saveDialog = null;
            if (dock is ModelDock)
                saveDialog = dlgSaveModel;

            // Path
            string path = dock.Path;

            // Save as
            if (saveAs || dock.HasNoPath) {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;
                path = saveDialog.FileName;
            }

            // Save
            SaveDocument(dock, path);
        }

        //--------------------------------------------------------------------------------
        public void SaveDocument(bool saveAs = false) {
            // Checks
            if (ActiveDock == null)
                return;

            // Save
            SaveDocument(ActiveDock, saveAs);
        }


        // MODELS ================================================================================
        //--------------------------------------------------------------------------------
        private void NewModel() {
            // Name
            string name = "";
            int i = 1;
            while (true) {
                name = "Untitled" + i;
                if (!HasDockControlByName(name))
                    break;
                ++i;
            }

            // Create / open
            OpenModelDock(new PDFScrapeModel(null, name));
        }

        //--------------------------------------------------------------------------------
        public void OpenModel(string filePath) {
            // Existing dock
            ModelDock dock = ModelDock(filePath);
            if (dock != null) {
                mgrDockManager.DockController.Activate(dock.DockPanel);
                return;
            }

            // Model
            PDFScrapeModel model;
            try { model = new PDFScrapeModel(filePath); }
            catch (Exception e) {
                XtraMessageBox.Show("Failed to open: " + e.Message, "PDF Scraper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Dock
            ShowWaitOverlay();
            OpenModelDock(model, filePath);
            HideWaitOverlay();
        }


        // MACROS ================================================================================
        //--------------------------------------------------------------------------------
        public void NewMacro() {

        }

        //--------------------------------------------------------------------------------
        public void OpenMacro(string filePath) {

        }


        // SCRAPERS ================================================================================
        //--------------------------------------------------------------------------------
        public void NewScraper() {

        }

        //--------------------------------------------------------------------------------
        public void OpenScraper(string filePath) {
            filePath = "";
        }


        // DOCKS ================================================================================
        //--------------------------------------------------------------------------------
        public void OpenModelDock(PDFScrapeModel model, string path = "") {
            // Panel
            DockPanel panel = mgrDockManager.AddPanel(DockingStyle.Float);
            panel.ImageOptions.Image = ImageResourceCache.Default.GetImage(MODEL_ICON_NAME);

            // Panel events
            /*panel.SizeChanged += (object sender, EventArgs e) => {
                if (panel.Width < DOCK_MINIMUM_FLOAT_SIZE.Width)
                    panel.Width = DOCK_MINIMUM_FLOAT_SIZE.Width;
                if (panel.Height < DOCK_MINIMUM_FLOAT_SIZE.Height)
                    panel.Height = DOCK_MINIMUM_FLOAT_SIZE.Height;

                if (panel.FloatForm != null) {
                    if (panel.FloatForm.Width < DOCK_MINIMUM_FLOAT_SIZE.Width)
                        panel.FloatForm.Width = DOCK_MINIMUM_FLOAT_SIZE.Width;
                    if (panel.FloatForm.Height < DOCK_MINIMUM_FLOAT_SIZE.Height)
                        panel.FloatForm.Height = DOCK_MINIMUM_FLOAT_SIZE.Height;
                }
            };*/

            // Dock
            ModelDock dock = new ModelDock(this, panel, model, path);
            panel.ControlContainer.Controls.Add(dock);
            dock.Dock = DockStyle.Fill;

            // Docking
            panel.DockedAsTabbedDocument = true;
        }

        //--------------------------------------------------------------------------------
        private void tbvTabbedView_DocumentAdded(object sender, DocumentEventArgs e) {
            if (e.Document.IsDockPanel) {
                DockControl dock = DocumentDockControl(e.Document);
                if (dock is ModelDock)
                    e.Document.ImageOptions.Image = ImageResourceCache.Default.GetImage(MODEL_ICON_NAME);
            }
        }
 
        //--------------------------------------------------------------------------------
        private void tbvTabbedView_BeginFloating(object sender, DocumentCancelEventArgs e) {
            if (e.Document.IsDockPanel)
                e.Document.FloatSize = DocumentDockPanel(e.Document).Size;
        }

        //--------------------------------------------------------------------------------
        public ModelDock ModelDock(string filePath) {
            return DockControl<ModelDock>(filePath);
        }
        
        //--------------------------------------------------------------------------------
        public DockControl ActiveDock {
            get {
                if (mgrDockManager.ActivePanel != null)
                    return DockControl(mgrDockManager.ActivePanel);
                else if (tbvTabbedView.ActiveDocument != null)
                    return DocumentDockControl(tbvTabbedView.ActiveDocument);
                else
                    return null;
            }
        }

        //--------------------------------------------------------------------------------
        public List<DockControl> UnsavedDocks() {
            List<DockControl> unsavedDocks = new List<DockControl>();
            foreach (DockPanel p in mgrDockManager.Panels) {
                DockControl dock = DockControl(p);
                if (dock != null && dock.HasChanged)
                    unsavedDocks.Add(dock);
            }

            return unsavedDocks;
        }

        //--------------------------------------------------------------------------------
        private DockControl DockControl(DockPanel panel) {
            return (panel.Tag is DockControl ? (DockControl)panel.Tag : null);
        }

        //--------------------------------------------------------------------------------
        private T DockControl<T>(string filePath) where T : DockControl {
            // Find
            foreach (DockPanel p in mgrDockManager.Panels) {
                DockControl dock = DockControl(p);
                if (dock != null && (dock is T)) {
                    if (dock.Path.Equals(filePath))
                        return (T)dock;
                }

                /*if (p.ControlContainer != null) {
                    foreach (Control c in p.ControlContainer.Controls) {
                        if (c is T) {
                            T dock = (T)c;
                            if ((dock != null) && dock.Path.Equals(filePath))
                                return dock;
                        }
                    }
                }*/
            }

            // Not found
            return default(T);
        }

        //--------------------------------------------------------------------------------
        private bool HasDockControlByName(string name) {
            // Find
            foreach (DockPanel p in mgrDockManager.Panels) {
                DockControl dock = DockControl(p);
                if (dock != null && dock.DocumentName.Equals(name))
                    return true;
            }
            
            // Not found
            return false;
        }

        //--------------------------------------------------------------------------------
        private DockPanel DocumentDockPanel(BaseDocument document) {
            if (document.Control is FloatForm)
                return (DockPanel)((FloatForm)document.Control).ActiveControl;
            else
                return (DockPanel)document.Control;
        }

        //--------------------------------------------------------------------------------
        private DockControl DocumentDockControl(BaseDocument document) { return DockControl(DocumentDockPanel(document)); }


        // FILES ================================================================================
        //--------------------------------------------------------------------------------
        private static ScraperFileType FileType(string filePath) {
            switch (Path.GetExtension(filePath).ToLower()) {
                case ".psmdl":
                    return ScraperFileType.MODEL;
                case ".psmcr":
                    return ScraperFileType.MACRO;
                default:
                    throw new ArgumentException();
            }
        }


        // WAIT OVERLAY ================================================================================
        //--------------------------------------------------------------------------------
        public void ShowWaitOverlay(DockPanel panel = null) {
            // Hide
            HideWaitOverlay();

            // Overlay
            if ((panel == null) || panel.DockedAsTabbedDocument)
                mWaitOverlayHandle = SplashScreenManager.ShowOverlayForm(this);
            else
                mWaitOverlayHandle = SplashScreenManager.ShowOverlayForm(panel);
        }
        
        //--------------------------------------------------------------------------------
        public void HideWaitOverlay() {
            if (mWaitOverlayHandle != null)
                SplashScreenManager.CloseOverlayForm(mWaitOverlayHandle);
        }
    }
}
