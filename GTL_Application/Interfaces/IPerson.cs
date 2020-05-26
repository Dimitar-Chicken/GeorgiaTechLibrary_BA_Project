using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GTL_Application.Interfaces
{
    public interface IPerson
    {
        public SecureString SSN { get; set; }
        public string PersonName { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
    }
}
