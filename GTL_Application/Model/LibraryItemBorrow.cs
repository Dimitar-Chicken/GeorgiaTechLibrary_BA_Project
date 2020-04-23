using System;
using System.Collections.Generic;
using System.Text;

namespace GTL_Application.Model
{
    public class LibraryItemBorrow
    {
        public string PersonName { get; set; }
        public string ISBN { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
