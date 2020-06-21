using BloodBank.Web.DTO.BloodTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.DTO.BloodDonators
{
    public class AddBloodDonatorDTO
    {
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string HomeAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string BloodGroupName { get; set; }
        public string Pesel { get; set; }
    }
}
