using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Webdriver.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public bool IsMr { get; set; }

        [StringLength(20)]
        public string FirsName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        public string Email { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Newsletter { get; set; }

        public bool Offers { get; set; }

        [StringLength(20)]
        public string Company { get; set; }

        [StringLength(20)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Address2 { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        public State State { get; set; }

        public int PostCode { get; set; }

        [StringLength(100)]
        public string AdditionalInfo { get; set; }

        public int PhoneHome { get; set; }

        public int PhoneMobile { get; set; }

        [StringLength(20)]
        public string MyAddressName { get; set; }

    }
}
