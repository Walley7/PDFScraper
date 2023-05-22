using PDFScrape.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PDFScraper.Forms.Overlay {

    public partial class OverlayMessageForm : Form {
        //================================================================================
        public enum FormPosition {
            BOTTOM_MIDDLE
        }


        //================================================================================
        private FormPosition                    mFormPosition;

        private int                             mHeight;


        //================================================================================
        //--------------------------------------------------------------------------------
        public OverlayMessageForm(string message = "", FormPosition formPosition = FormPosition.BOTTOM_MIDDLE, int height = 25) {
            // Initialise
            InitializeComponent();

            // Form position / dimensions
            mFormPosition = formPosition;
            mHeight = height;

            // Message
            lblMessage.Text = message;

            // Size
            Height = height;

            // Transparency
            int initialStyle = (int)UWindows.GetWindowLong(this.Handle, UWindows.GWL_EXSTYLE);
            UWindows.SetWindowLong(this.Handle, UWindows.GWL_EXSTYLE, new IntPtr(initialStyle | UWindows.WS_EX_LAYERED | UWindows.WS_EX_TRANSPARENT));
            UWindows.SetLayeredWindowAttributes(this.Handle, 0, 200, UWindows.LWA_ALPHA);

            // Other method - doesn't allow click through of form body though
            //Color backColor = Color.White;
            //TransparencyKey = backColor;
            //Opacity = 200.0f / 255.0f;
        }

        
        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void OverlayMessageForm_Shown(object sender, EventArgs e) {
            // Screen area
            Rectangle screenArea = Screen.FromPoint(Cursor.Position).WorkingArea;
            int middleX = screenArea.Left + ((screenArea.Right - screenArea.Left) / 2);
            int middleY = screenArea.Top + ((screenArea.Bottom - screenArea.Top) / 2);
            
            // Height (for some reason it resets to a larger size unless set manually here)
            Height = mHeight;

            // Position
            switch (mFormPosition) {
                case FormPosition.BOTTOM_MIDDLE:
                    Location = new Point(middleX - (Width / 2), screenArea.Bottom - Height - 10);
                    break;
            }
        }

        
        // PROPERTIES ================================================================================
        //--------------------------------------------------------------------------------
        public string Message {
            set { lblMessage.Text = value; }
            get { return lblMessage.Text; }
        }

        //--------------------------------------------------------------------------------
        public Image Image {
            set { picImage.Image = value; }
            get { return picImage.Image; }
        }
    }
}
