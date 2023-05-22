using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Exceptions {

    class ConsistencyException : Exception {
        //================================================================================
        public ConsistencyException() { }
        public ConsistencyException(string message) : base(message) { }
        public ConsistencyException(string message, Exception inner) : base(message, inner) { }
    }

}
