using GTL_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GTL_Application.Model
{
    public class BorrowableBookCopy : IBorrowableBookCopy
    {
        public SecureString ID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }

        public override string ToString()
        {
            return string.Format("{0} by {1}", Title, Authors);
        }
    }
}
