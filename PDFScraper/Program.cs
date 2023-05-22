using PDFScraper.Forms;
using PDFScraper.Forms2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper {

    static class Program {
        //================================================================================
        //--------------------------------------------------------------------------------
        [STAThread]
        static void Main() {
            // Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Run
            Application.Run(new LaunchForm());
            Application.Run(new MainForm());
        }
    }
}
