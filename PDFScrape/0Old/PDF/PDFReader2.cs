using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.PDF {

    public class PDFReader2 {
        //================================================================================
        private string                          mFilePath;

        private PdfReader                       mReader;
        private PdfDocument                     mDocument;

        private int                             mDPI;
        private Image[]                         mPageImages = null;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFReader2(string filePath, int dpi, bool readAllPageImages = true) {
            // File path
            mFilePath = filePath;

            // Reader / document
            mReader = new PdfReader(filePath);
            mDocument = new PdfDocument(mReader);

            // First page
            mDPI = dpi;
            ReadPageImages(1, readAllPageImages ? int.MaxValue : 1);
        }

        //--------------------------------------------------------------------------------
        public void Dispose() {
            // Images
            if (mPageImages != null) {
                foreach (Image i in mPageImages) {
                    i.Dispose();
                }
                mPageImages = null;
            }

            // Reader / document
            mDocument.Close();
            mReader.Close();
        }


        // PATH ================================================================================
        //--------------------------------------------------------------------------------
        public string FilePath { get { return mFilePath; } }


        // DOCUMENT ================================================================================
        //--------------------------------------------------------------------------------
        public PdfDocument Document { get { return mDocument; } }
        public iText.Kernel.Geom.Rectangle PageBox { get { return mDocument.GetFirstPage().GetMediaBox(); } }
        public iText.Kernel.Geom.Rectangle CropBox { get { return mDocument.GetFirstPage().GetCropBox(); } }


        // SCRAPING ================================================================================
        //--------------------------------------------------------------------------------
        // XScale and YScale scale from the dimensions of the image we laid out inputs
        // against to the dimensions of the pdf's crop box.
        // XTranslate and YTranslate translate from the top left margin of the media box
        // to the top left margin of the crop box.
        public double XScale { get { return (double)CropBox.GetWidth() / (double)PageImages[0].Width; } }
        public double YScale { get { return (double)CropBox.GetHeight() / (double)PageImages[0].Height; } }
        public double XTranslate { get { return ((double)PageBox.GetWidth() - (double)CropBox.GetWidth()) / 2.0; } }
        public double YTranslate { get { return ((double)PageBox.GetHeight() - (double)CropBox.GetHeight()) / 2.0;} }


        // IMAGES ================================================================================
        //--------------------------------------------------------------------------------
        public Image[] ReadPageImages(int startPage, int endPage) {
            // Checks
            if (startPage > endPage)
                throw new ArgumentException();
            if (startPage < 1)
                throw new IndexOutOfRangeException();

            // Ghostscript
            //string dllPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "gsdll32.dll");
            //GhostscriptVersionInfo ghostscriptVersionInfo = new GhostscriptVersionInfo(dllPath);
            GhostscriptVersionInfo ghostscriptVersionInfo = new GhostscriptVersionInfo("gsdll32.dll");

            // Rasteriser
            GhostscriptRasterizer rasteriser = new GhostscriptRasterizer();
            rasteriser.Open(FilePath, ghostscriptVersionInfo, false);

            // Checks
            if (endPage > rasteriser.PageCount)
                endPage = rasteriser.PageCount;

            // Images
            mPageImages = new Image[(endPage - startPage) + 1];
            for (int i = startPage; i <= endPage; ++i) {
                mPageImages[i - 1] = rasteriser.GetPage(mDPI, mDPI, i);
            }

            return mPageImages;
        }

        //--------------------------------------------------------------------------------
        public Image[] PageImages { get { return mPageImages; } }
    }

}
