using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Models
{
    public class BloodDonator:BaseModel
    {
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string HomeAdress { get; set; }
        public string PhoneNumber { get; set; }
        public int AmmountOfBloodDonated { get; set; }
        public string Pesel { get; set; }
        public virtual string BloodGroupName { get; set; }
        public virtual BloodType BloodType { get; set; }
        public virtual string UserEmail { get; set; }
        public virtual User User { get; set; }
    }
}
