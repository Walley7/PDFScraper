using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using PDFScrape.PDF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Scraping {

    public class PDFScrapeRegion2 {
        //================================================================================
        private PDFScrapeModel2                  mModel;

        private string                          mName;

        private int                             mPage;

        private int                             mX;
        private int                             mY;
        private int                             mWidth;
        private int                             mHeight;

        private bool                            mMandatory = true;


        //================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeRegion2(PDFScrapeModel2 model, string name, int page = 1, int x = 0, int y = 0, int width = 0, int height = 0) {
            mModel = model;
            mName = name;
            mPage = page;
            mX = x;
            mY = y;
            mWidth = width;
            mHeight = height;
        }


        // MODEL ================================================================================
        //--------------------------------------------------------------------------------
        public PDFScrapeModel2 Model { get { return mModel; } }


        // NAME ================================================================================
        //--------------------------------------------------------------------------------
        public bool SetName(string name) {
            if (!mModel.HasRegion(name)) {
                mName = name;
                return true;
            }
            else
                return false;
        }

        //--------------------------------------------------------------------------------
        public string Name { get { return mName; } }


        // PAGE ================================================================================
        //--------------------------------------------------------------------------------
        public int Page { set { mPage = value; } get { return mPage; } }


        // DIMENSIONS ================================================================================
        //--------------------------------------------------------------------------------
        public int X { set { mX = value; } get { return mX; } }
        public int Y { set { mY = value; } get { return mY; } }
        public int Width { set { mWidth = value; } get { return mWidth; } }
        public int Height { set { mHeight = value; } get { return mHeight; } }
        public Rectangle Dimensions { get { return new Rectangle(mX, mY, mWidth, mHeight); } }


        // RULES ================================================================================
        //--------------------------------------------------------------------------------
        public bool Mandatory { set { mMandatory = value; } get { return mMandatory; } }


        // SCRAPING ================================================================================
        //--------------------------------------------------------------------------------
        public string Scrape(PDFReader2 reader) {
            // Filter
            //var pageRotatedBox = pdfDocument.GetFirstPage().GetPageSizeWithRotation(); - may be relevant at some point
            TextRegionEventFilter regionFilter = new TextRegionEventFilter(
                new iText.Kernel.Geom.Rectangle((float)((X * reader.XScale) + reader.XTranslate),
                                                (float)(reader.CropBox.GetHeight() - ((Y + Height) * reader.YScale) + reader.YTranslate),
                                                (float)(Width * reader.XScale), (float)(Height * reader.YScale))
            );

            // Scrape
            ITextExtractionStrategy strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy(), regionFilter);
            /*try {*/ return PdfTextExtractor.GetTextFromPage(reader.Document.GetPage(mPage), strategy); /*}
            catch (Exception) {  }*/
        }
    }

}
