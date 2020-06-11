using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Models
{
    public class BloodStorage:BaseModel
    {
        public int Id { get; set; }
        public int StoredAmmount { get; set; }
        public virtual string BloodGroupName { get; set; }
        public virtual BloodType BloodType { get; set; }
    }
}
