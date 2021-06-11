using DevelopigCommunityService.Models.AbstractClasses.Bassal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopigCommunityService.Interfaces
{
    public interface ITokenService
    {
        String CreateToken(AppUser appUser);

        void GetJWTClams(String JWTToken);
    }
}
