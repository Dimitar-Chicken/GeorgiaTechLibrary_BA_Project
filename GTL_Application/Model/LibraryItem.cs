using GTL_Application.Interfaces;
using System;

namespace GTL_Application.Model
{
    public class LibraryItem : ILibraryItem
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string SubjectArea { get; set; }
        public string ItemDescription { get; set; }
        public string TypeName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} by {1}", Title, Authors);
        }
    }
}