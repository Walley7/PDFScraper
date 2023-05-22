using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.IText {

    public class TextChunkComparer : IComparer<TextChunk> {
        //================================================================================
        private IComparer<ITextChunkLocation>   mLocationComparer;


        //================================================================================
        //--------------------------------------------------------------------------------
        public TextChunkComparer(IComparer<ITextChunkLocation> locationComparer) {
            mLocationComparer = locationComparer;
        }


        // COMPARISON ================================================================================
        //--------------------------------------------------------------------------------
        public virtual int Compare(TextChunk first, TextChunk second) {
            return mLocationComparer.Compare(first.GetLocation(), second.GetLocation());
        }
    }

}
