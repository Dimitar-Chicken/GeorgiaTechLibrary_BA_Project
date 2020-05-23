using System;
using System.Collections.Generic;
using System.Text;

namespace GTL_Application.Interfaces
{
    public interface ILibraryItem
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string SubjectArea { get; set; }
        public string ItemDescription { get; set; }
        public string TypeName { get; set; }
    }
}
