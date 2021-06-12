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
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public int Age { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }

        private int _data;

        private byte[] _PasswordHash;
        public byte[] PasswordHash { set { _PasswordHash = value; } }

        private byte[] _PasswordSalt;
        public byte[] PasswordSalt { set { _PasswordSalt = value; } }
        public byte[] Photo { get; set; }

     
        public DateTime StartAccess { get; set; }
        public DateTime EndAccess { get; set; }



        public byte[] GetPasswordSalt()
        {
            return _PasswordSalt;
        }

        public byte[] GetPasswordHash()
        {
            return _PasswordHash;
        }

    }
}
