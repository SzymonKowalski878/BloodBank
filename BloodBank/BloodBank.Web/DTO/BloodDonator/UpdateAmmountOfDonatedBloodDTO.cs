using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.DTO.BloodDonator
{
    public class UpdateAmmountOfDonatedBloodDTO
    {
        public string Pesel { get; set; }
        public int AmmountOfBlood { get; set; }
    }
}
