using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopigCommunityService.Models.AbstractClasses.Bassal
{
    abstract public class Person
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Password { get; set; }
        public String ConfPassword { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
      
    }
}
