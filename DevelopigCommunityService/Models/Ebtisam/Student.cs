using DevelopigCommunityService.Models.AbstractClasses.Bassal;
using DevelopigCommunityService.Models.Aya;
using DevelopigCommunityService.Models.Somaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopigCommunityService.Models.Ebtisam
{

    enum IsGraduated{
       PostGraduate,Graduate,UnderGraduate
    }

    public class Student: AppUser
    {
        public String EducationBackground { get; set; }
        public virtual Department Department { get; set; }

        // category access

        public virtual ICollection<Project> Projects { get; set; }
            = new HashSet<Project>();

    }
}
