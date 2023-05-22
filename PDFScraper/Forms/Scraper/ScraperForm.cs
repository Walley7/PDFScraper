using PDFScraper.Forms.Macros;
using PDFScraper.Forms.Model;
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



namespace PDFScraper.Forms.Scraper {
    
    public partial class ScraperForm : Form {
        //================================================================================
        private LaunchForm                      mLaunchForm;
        private bool                            mReturnToLaunchForm = true;

        private Label                           mLoadingLabel = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public ScraperForm(LaunchForm launchForm) {
            // MODULE: select files to import (part of scraper section)
            //         show a grid of all the values that will be extracted from them, for the given extractor
            //         including any detected errors or data issues
            // IMPORT OPTIONS:
            // - output a CSV.
            // - input via macro.
            // - run sql commands against a database.
            //   (this would require sets of table / field / extractor mappings - make this a post 1.0 feature).
            // - support for other databases.
            // - potentially support for specific software packages.
            
        //tips
        //e.g.
        //when ending macro, show "make sure macro returns to starting point that will match with running it again for multiple documents"
        //(have a picture box for tips too, in case an image is needed).
        //
        //Two checkboxes:
        //"show this tip"
        //"show tips" (affects all tips)
        //
        //possibly have a tips list section, where you can view them all and mark/unmark their enabled status.

            MessageBox.Show("PDFScrapeExecutor - also read comments above this");


            // Initialise
            InitializeComponent();

            // Launch form
            mLaunchForm = launchForm;

            // Theme
            dckMain.Theme = new VS2015DarkTheme();
        }


        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void ScraperForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (mReturnToLaunchForm) {
                mLaunchForm.MoveToCentre();
                mLaunchForm.Show();
            }
        }


        // NAVIGATION BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnScraper_Click(object sender, EventArgs e) {

        }

        //--------------------------------------------------------------------------------
        private void btnModelEditor_Click(object sender, EventArgs e) {

        }

        //--------------------------------------------------------------------------------
        private void btnMacroEditor_Click(object sender, EventArgs e) {

        }

        //--------------------------------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e) {

        }


        // DOCUMENT BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e) { }
        private void btnOpen_Click(object sender, EventArgs e) { }
        private void btnSave_Click(object sender, EventArgs e) { }
        private void btnSaveAs_Click(object sender, EventArgs e) { }
    }

}
