using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Models
{
    public class User:BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Role Role { get; set; }
        public virtual BloodDonator BloodDonator { get; set; }
    }
}
