using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Models
{
    public class Role:BaseModel
    {
        public int Id { get; set; }
        public bool IsWorker { get; set; }
        public bool IsBloodDonator { get; set; }
        public virtual string UserEmail { get; set; }
        public virtual  User User { get; set; }
    }
}
