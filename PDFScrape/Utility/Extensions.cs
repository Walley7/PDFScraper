using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Utility {

    public static class Extensions {
        // DICTIONARY ================================================================================
        //--------------------------------------------------------------------------------
        public static TValue Put<TKey, TValue>(this IDictionary<TKey, TValue> col, TKey key, TValue value) {
            TValue oldVal = col.Get(key);
            col[key] = value;
            return oldVal;
        }

        //--------------------------------------------------------------------------------
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            TValue value = default(TValue);
            if (key != null)
                dictionary.TryGetValue(key, out value);
            return value;
        }


        // RECTANGLE ================================================================================
        //--------------------------------------------------------------------------------
        public static float IntersectLineAmount(this iText.Kernel.Geom.Rectangle rectangle, float x1, float y1, float x2, float y2) {
            // Clip
            Tuple<Point, Point> clippedLine = UGeneral.ClipLineSegment(rectangle, new Point(x1, y1), new Point(x2, y2));

            // Length
            if (clippedLine == null)
                return 0.0f;
            else
                return (float)UGeneral.LineLength(clippedLine.Item1.x, clippedLine.Item1.y, clippedLine.Item2.x, clippedLine.Item2.y);
        }
    }

}
