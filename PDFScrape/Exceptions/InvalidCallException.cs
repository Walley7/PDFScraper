using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Exceptions {

    public class InvalidCallException : Exception {
        //================================================================================
        public InvalidCallException() { }
        public InvalidCallException(string message) : base(message) { }
        public InvalidCallException(string message, Exception inner) : base(message, inner) { }
    }

}
