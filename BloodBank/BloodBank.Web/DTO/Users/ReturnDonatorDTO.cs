using BloodBank.Web.DTO.BloodDonator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.DTO.Users
{
    public class ReturnDonatorDTO
    {
        public string Email { get; set; }
        public ReturnBloodDonatorDTO BloodDonator { get; set; }
    }
}
