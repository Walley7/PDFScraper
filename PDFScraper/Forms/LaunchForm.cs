using PDFScraper.Forms.Macros;
using PDFScraper.Forms.Model;
using PDFScraper.Forms.Scraper;
using PDFScraper.Forms2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms {

    public partial class LaunchForm : Form {
        //================================================================================


        //================================================================================
        //--------------------------------------------------------------------------------
        public LaunchForm() {
            // Initialise
            InitializeComponent();
        }



        // LOCATIONS ================================================================================
        //--------------------------------------------------------------------------------
        public void MoveToCentre() {
            Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (Size.Width / 2),
                                 (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (Size.Height / 2));
        }


        // BUTTONS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnScraper_Click(object sender, EventArgs e) {
            ScraperForm form = new ScraperForm(this);
            form.Show();
            Hide();
        }
        
        //--------------------------------------------------------------------------------
        private void btnModelEditor_Click(object sender, EventArgs e) {
            ModelForm form = new ModelForm(this);
            form.Show();
            Hide();
        }
        
        //--------------------------------------------------------------------------------
        private void btnMacroEditor_Click(object sender, EventArgs e) {
            MacroForm form = new MacroForm(this);
            form.Show();
            Hide();
        }
        
        //--------------------------------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e) {
            Close();
        }
        
        //--------------------------------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e) {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }

}
