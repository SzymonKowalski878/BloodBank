using BloodBank.Web.DTO.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.DTO.Users
{
    public class ReturnUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ReturnRolesDTO Role { get; set; }
    }
}
