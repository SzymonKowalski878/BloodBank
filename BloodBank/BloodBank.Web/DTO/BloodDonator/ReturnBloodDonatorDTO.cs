using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.DTO.BloodDonator
{
    public class ReturnBloodDonatorDTO
    {
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string HomeAdress { get; set; }
        public string PhoneNumber { get; set; }
        public int AmmountOfBloodDonated { get; set; }
        public string Pesel { get; set; }
    }
}
