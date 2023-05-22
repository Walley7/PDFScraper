using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScraper.Forms2.Docks {

    public class DockControl : XtraUserControl {
        //================================================================================
        private MainForm                        mForm;
        private DockPanel                       mDockPanel;

        private string                          mPath = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public DockControl() { }

        //--------------------------------------------------------------------------------
        public DockControl(MainForm form, DockPanel dockPanel, string path = "") {
            // Form / dock panel
            mForm = form;
            mDockPanel = dockPanel;
            mDockPanel.Tag = this;

            // Path
            mPath = path;

            // Events            
            this.ParentChanged += OnCreate;
            this.HandleDestroyed += OnDestroy;
        }


        // EVENTS ================================================================================
        //--------------------------------------------------------------------------------
        protected virtual void OnCreate(object sender, EventArgs e) { }
        protected virtual void OnDestroy(object sender, EventArgs e) { }


        // FORM / DOCK PANEL ================================================================================
        //--------------------------------------------------------------------------------
        public MainForm Form { get { return mForm; } }
        public DockPanel DockPanel { get { return mDockPanel; } }
        public virtual void UpdateTitle() { }


        // DOCUMENT ================================================================================
        //--------------------------------------------------------------------------------
        public virtual string DocumentName { get { throw new NotImplementedException(); } }
        public virtual bool HasChanged { get { throw new NotImplementedException(); } }

        //--------------------------------------------------------------------------------
        public string Path {
            set { mPath = value; }
            get { return mPath; }
        }

        //--------------------------------------------------------------------------------
        public bool HasNoPath { get { return string.IsNullOrWhiteSpace(mPath); } }


        // SAVING ================================================================================
        //--------------------------------------------------------------------------------
        public virtual void Save(string path) { throw new NotImplementedException(); }
    }
}
