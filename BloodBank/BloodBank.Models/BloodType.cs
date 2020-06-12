using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Models
{
    public class BloodType:BaseModel
    {
        public string BloodGroupName { get; set; }
        public virtual BloodStorage BloodStorage { get; set; }
    }
}
