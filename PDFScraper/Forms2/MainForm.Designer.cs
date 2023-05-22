namespace PDFScraper.Forms2 {
    partial class MainForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.mnuMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.btnMenu_Open = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenu_Save = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenu_SaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenu_Exit = new DevExpress.XtraBars.BarButtonItem();
            this.btnRibbon_File_New = new DevExpress.XtraBars.BarButtonItem();
            this.btnRibbon_File_Open = new DevExpress.XtraBars.BarButtonItem();
            this.btnRibbon_File_Save = new DevExpress.XtraBars.BarButtonItem();
            this.btnRibbon_File_SaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.btnNew_Model = new DevExpress.XtraBars.BarButtonItem();
            this.btnNew_Macro = new DevExpress.XtraBars.BarButtonItem();
            this.btnNew_Scraper = new DevExpress.XtraBars.BarButtonItem();
            this.ripRibbon = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgRibbon_File = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rsbStatus = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.mgrDockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.mgrDocumentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tbvTabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.dlgOpenFile = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.dlgSaveModel = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.dlgSaveMacro = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.dlgSaveScraper = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.mnuNew = new DevExpress.XtraBars.PopupMenu(this.components);
            this.mnuMenu_New = new DevExpress.XtraBars.BarSubItem();
            this.btnMenu_New_Model = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenu_New_Macro = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenu_New_Scraper = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mnuMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgrDockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgrDocumentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbvTabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mnuNew)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.mnuMenu;
            this.ribbon.ApplicationButtonImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbon.ApplicationButtonImageOptions.Image")));
            this.ribbon.ApplicationButtonText = "File";
            this.ribbon.ApplicationCaption = "PDF Scraper";
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnRibbon_File_New,
            this.btnRibbon_File_Open,
            this.btnRibbon_File_Save,
            this.btnRibbon_File_SaveAs,
            this.btnMenu_Open,
            this.btnMenu_Save,
            this.btnMenu_SaveAs,
            this.btnMenu_Exit,
            this.btnNew_Model,
            this.btnNew_Macro,
            this.btnNew_Scraper,
            this.mnuMenu_New,
            this.btnMenu_New_Model,
            this.btnMenu_New_Macro,
            this.btnMenu_New_Scraper});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 24;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ripRibbon});
            this.ribbon.RibbonCaptionAlignment = DevExpress.XtraBars.Ribbon.RibbonCaptionAlignment.Left;
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbon.Size = new System.Drawing.Size(1164, 79);
            this.ribbon.StatusBar = this.rsbStatus;
            // 
            // mnuMenu
            // 
            this.mnuMenu.ItemLinks.Add(this.mnuMenu_New);
            this.mnuMenu.ItemLinks.Add(this.btnMenu_Open);
            this.mnuMenu.ItemLinks.Add(this.btnMenu_Save);
            this.mnuMenu.ItemLinks.Add(this.btnMenu_SaveAs);
            this.mnuMenu.ItemLinks.Add(this.btnMenu_Exit, true);
            this.mnuMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesText;
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Ribbon = this.ribbon;
            // 
            // btnMenu_Open
            // 
            this.btnMenu_Open.Caption = "Open";
            this.btnMenu_Open.Id = 12;
            this.btnMenu_Open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_Open.ImageOptions.Image")));
            this.btnMenu_Open.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_Open.ImageOptions.LargeImage")));
            this.btnMenu_Open.Name = "btnMenu_Open";
            this.btnMenu_Open.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMenu_Open_ItemClick);
            // 
            // btnMenu_Save
            // 
            this.btnMenu_Save.Caption = "Save";
            this.btnMenu_Save.Id = 13;
            this.btnMenu_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_Save.ImageOptions.Image")));
            this.btnMenu_Save.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_Save.ImageOptions.LargeImage")));
            this.btnMenu_Save.Name = "btnMenu_Save";
            this.btnMenu_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMenu_Save_ItemClick);
            // 
            // btnMenu_SaveAs
            // 
            this.btnMenu_SaveAs.Caption = "Save As";
            this.btnMenu_SaveAs.Id = 14;
            this.btnMenu_SaveAs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_SaveAs.ImageOptions.Image")));
            this.btnMenu_SaveAs.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_SaveAs.ImageOptions.LargeImage")));
            this.btnMenu_SaveAs.Name = "btnMenu_SaveAs";
            this.btnMenu_SaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMenu_SaveAs_ItemClick);
            // 
            // btnMenu_Exit
            // 
            this.btnMenu_Exit.Caption = "Exit";
            this.btnMenu_Exit.Id = 15;
            this.btnMenu_Exit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_Exit.ImageOptions.Image")));
            this.btnMenu_Exit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_Exit.ImageOptions.LargeImage")));
            this.btnMenu_Exit.Name = "btnMenu_Exit";
            this.btnMenu_Exit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuMenu_Exit_ItemClick);
            // 
            // btnRibbon_File_New
            // 
            this.btnRibbon_File_New.Id = 4;
            this.btnRibbon_File_New.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_New.ImageOptions.Image")));
            this.btnRibbon_File_New.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_New.ImageOptions.LargeImage")));
            this.btnRibbon_File_New.Name = "btnRibbon_File_New";
            this.btnRibbon_File_New.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRibbon_File_New_ItemClick);
            // 
            // btnRibbon_File_Open
            // 
            this.btnRibbon_File_Open.Id = 5;
            this.btnRibbon_File_Open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_Open.ImageOptions.Image")));
            this.btnRibbon_File_Open.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_Open.ImageOptions.LargeImage")));
            this.btnRibbon_File_Open.Name = "btnRibbon_File_Open";
            this.btnRibbon_File_Open.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRibbon_File_Open_ItemClick);
            // 
            // btnRibbon_File_Save
            // 
            this.btnRibbon_File_Save.Id = 6;
            this.btnRibbon_File_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_Save.ImageOptions.Image")));
            this.btnRibbon_File_Save.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_Save.ImageOptions.LargeImage")));
            this.btnRibbon_File_Save.Name = "btnRibbon_File_Save";
            this.btnRibbon_File_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRibbon_File_Save_ItemClick);
            // 
            // btnRibbon_File_SaveAs
            // 
            this.btnRibbon_File_SaveAs.Id = 7;
            this.btnRibbon_File_SaveAs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_SaveAs.ImageOptions.Image")));
            this.btnRibbon_File_SaveAs.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRibbon_File_SaveAs.ImageOptions.LargeImage")));
            this.btnRibbon_File_SaveAs.Name = "btnRibbon_File_SaveAs";
            this.btnRibbon_File_SaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRibbon_File_SaveAs_ItemClick);
            // 
            // btnNew_Model
            // 
            this.btnNew_Model.Caption = "Model";
            this.btnNew_Model.Id = 16;
            this.btnNew_Model.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew_Model.ImageOptions.Image")));
            this.btnNew_Model.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNew_Model.ImageOptions.LargeImage")));
            this.btnNew_Model.Name = "btnNew_Model";
            this.btnNew_Model.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_Model_ItemClick);
            // 
            // btnNew_Macro
            // 
            this.btnNew_Macro.Caption = "Macro";
            this.btnNew_Macro.Id = 17;
            this.btnNew_Macro.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew_Macro.ImageOptions.Image")));
            this.btnNew_Macro.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNew_Macro.ImageOptions.LargeImage")));
            this.btnNew_Macro.Name = "btnNew_Macro";
            this.btnNew_Macro.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_Macro_ItemClick);
            // 
            // btnNew_Scraper
            // 
            this.btnNew_Scraper.Caption = "Scraper";
            this.btnNew_Scraper.Id = 18;
            this.btnNew_Scraper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew_Scraper.ImageOptions.Image")));
            this.btnNew_Scraper.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNew_Scraper.ImageOptions.LargeImage")));
            this.btnNew_Scraper.Name = "btnNew_Scraper";
            this.btnNew_Scraper.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_Scraper_ItemClick);
            // 
            // ripRibbon
            // 
            this.ripRibbon.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgRibbon_File});
            this.ripRibbon.Name = "ripRibbon";
            this.ripRibbon.Text = "Ribbon";
            // 
            // rpgRibbon_File
            // 
            this.rpgRibbon_File.ItemLinks.Add(this.btnRibbon_File_New);
            this.rpgRibbon_File.ItemLinks.Add(this.btnRibbon_File_Open);
            this.rpgRibbon_File.ItemLinks.Add(this.btnRibbon_File_Save);
            this.rpgRibbon_File.ItemLinks.Add(this.btnRibbon_File_SaveAs);
            this.rpgRibbon_File.Name = "rpgRibbon_File";
            this.rpgRibbon_File.Text = "ribbonPageGroup1";
            // 
            // rsbStatus
            // 
            this.rsbStatus.Location = new System.Drawing.Point(0, 822);
            this.rsbStatus.Name = "rsbStatus";
            this.rsbStatus.Ribbon = this.ribbon;
            this.rsbStatus.Size = new System.Drawing.Size(1164, 31);
            // 
            // mgrDockManager
            // 
            this.mgrDockManager.DockingOptions.ShowCaptionImage = true;
            this.mgrDockManager.Form = this;
            this.mgrDockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            this.mgrDockManager.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.mgrDockManager_ClosingPanel);
            this.mgrDockManager.ClosedPanel += new DevExpress.XtraBars.Docking.DockPanelEventHandler(this.mgrDockManager_ClosedPanel);
            // 
            // mgrDocumentManager
            // 
            this.mgrDocumentManager.ContainerControl = this;
            this.mgrDocumentManager.View = this.tbvTabbedView;
            this.mgrDocumentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tbvTabbedView});
            // 
            // tbvTabbedView
            // 
            this.tbvTabbedView.DocumentAdded += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(this.tbvTabbedView_DocumentAdded);
            this.tbvTabbedView.BeginFloating += new DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventHandler(this.tbvTabbedView_BeginFloating);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "All files (*.*)|*.*|Macro Files (*.psmcr)|*.psmcr|Model Files (*.psmdl)|*.psmdl";
            // 
            // dlgSaveModel
            // 
            this.dlgSaveModel.Filter = "Model Files (*.psmdl)|*.psmdl";
            // 
            // dlgSaveMacro
            // 
            this.dlgSaveMacro.Filter = "Macro Files (*.psmcr)|*.psmcr";
            // 
            // dlgSaveScraper
            // 
            this.dlgSaveScraper.Filter = "Scraper Files (*.psscr)|*.psscr";
            // 
            // mnuNew
            // 
            this.mnuNew.ItemLinks.Add(this.btnNew_Model);
            this.mnuNew.ItemLinks.Add(this.btnNew_Macro);
            this.mnuNew.ItemLinks.Add(this.btnNew_Scraper);
            this.mnuNew.MenuCaption = "New";
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Ribbon = this.ribbon;
            this.mnuNew.ShowCaption = true;
            // 
            // mnuMenu_New
            // 
            this.mnuMenu_New.Caption = "New";
            this.mnuMenu_New.Id = 20;
            this.mnuMenu_New.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuMenu_New.ImageOptions.Image")));
            this.mnuMenu_New.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("mnuMenu_New.ImageOptions.LargeImage")));
            this.mnuMenu_New.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMenu_New_Model),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMenu_New_Macro),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMenu_New_Scraper)});
            this.mnuMenu_New.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.mnuMenu_New.Name = "mnuMenu_New";
            // 
            // btnMenu_New_Model
            // 
            this.btnMenu_New_Model.Caption = "Model";
            this.btnMenu_New_Model.Id = 21;
            this.btnMenu_New_Model.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_New_Model.ImageOptions.Image")));
            this.btnMenu_New_Model.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_New_Model.ImageOptions.LargeImage")));
            this.btnMenu_New_Model.Name = "btnMenu_New_Model";
            this.btnMenu_New_Model.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMenu_New_Model_ItemClick);
            // 
            // btnMenu_New_Macro
            // 
            this.btnMenu_New_Macro.Caption = "Macro";
            this.btnMenu_New_Macro.Id = 22;
            this.btnMenu_New_Macro.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_New_Macro.ImageOptions.Image")));
            this.btnMenu_New_Macro.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_New_Macro.ImageOptions.LargeImage")));
            this.btnMenu_New_Macro.Name = "btnMenu_New_Macro";
            this.btnMenu_New_Macro.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMenu_New_Macro_ItemClick);
            // 
            // btnMenu_New_Scraper
            // 
            this.btnMenu_New_Scraper.Caption = "Scraper";
            this.btnMenu_New_Scraper.Id = 23;
            this.btnMenu_New_Scraper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu_New_Scraper.ImageOptions.Image")));
            this.btnMenu_New_Scraper.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMenu_New_Scraper.ImageOptions.LargeImage")));
            this.btnMenu_New_Scraper.Name = "btnMenu_New_Scraper";
            this.btnMenu_New_Scraper.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMenu_New_Scraper_ItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 853);
            this.Controls.Add(this.rsbStatus);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.rsbStatus;
            this.Text = "PDF Scraper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mnuMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgrDockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgrDocumentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbvTabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mnuNew)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar rsbStatus;
        private DevExpress.XtraBars.Ribbon.RibbonPage ripRibbon;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgRibbon_File;
        private DevExpress.XtraBars.BarButtonItem btnRibbon_File_New;
        private DevExpress.XtraBars.BarButtonItem btnRibbon_File_Open;
        private DevExpress.XtraBars.BarButtonItem btnRibbon_File_Save;
        private DevExpress.XtraBars.BarButtonItem btnRibbon_File_SaveAs;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu mnuMenu;
        private DevExpress.XtraBars.BarButtonItem btnMenu_Open;
        private DevExpress.XtraBars.BarButtonItem btnMenu_Save;
        private DevExpress.XtraBars.BarButtonItem btnMenu_SaveAs;
        private DevExpress.XtraBars.BarButtonItem btnMenu_Exit;
        private DevExpress.XtraBars.Docking.DockManager mgrDockManager;
        private DevExpress.XtraBars.Docking2010.DocumentManager mgrDocumentManager;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tbvTabbedView;
        private DevExpress.XtraEditors.XtraOpenFileDialog dlgOpenFile;
        private DevExpress.XtraEditors.XtraSaveFileDialog dlgSaveModel;
        private DevExpress.XtraEditors.XtraSaveFileDialog dlgSaveMacro;
        private DevExpress.XtraEditors.XtraSaveFileDialog dlgSaveScraper;
        private DevExpress.XtraBars.PopupMenu mnuNew;
        private DevExpress.XtraBars.BarButtonItem btnNew_Model;
        private DevExpress.XtraBars.BarButtonItem btnNew_Macro;
        private DevExpress.XtraBars.BarButtonItem btnNew_Scraper;
        private DevExpress.XtraBars.BarSubItem mnuMenu_New;
        private DevExpress.XtraBars.BarButtonItem btnMenu_New_Model;
        private DevExpress.XtraBars.BarButtonItem btnMenu_New_Macro;
        private DevExpress.XtraBars.BarButtonItem btnMenu_New_Scraper;
    }
}