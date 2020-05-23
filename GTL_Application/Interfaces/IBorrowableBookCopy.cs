using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GTL_Application.Interfaces
{
    public interface IBorrowableBookCopy
    {
        public SecureString ID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
    }
}
