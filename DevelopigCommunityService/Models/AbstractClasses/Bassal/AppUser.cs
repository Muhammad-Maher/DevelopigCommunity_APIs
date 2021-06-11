using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopigCommunityService.Models.AbstractClasses.Bassal
{
    abstract public class AppUser
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public DateTime StartAccess { get; set; }
        public DateTime EndAccess { get; set; }

    }
}
