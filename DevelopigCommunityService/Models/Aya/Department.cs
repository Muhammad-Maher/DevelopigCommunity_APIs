using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopigCommunityService.Models.Aya
{
    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        [JsonIgnore]
        public bool IsActive { get; set; }

    }
}
