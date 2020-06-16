using BloodBank.Web.DTO.BloodDonators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.DTO.Users
{
    public class AddDonatorDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public AddBloodDonatorDTO BloodDonator { get; set; }
    }
}
