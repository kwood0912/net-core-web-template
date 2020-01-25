using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Web.Models
{
    public class AdminViewModel
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Name { get; set; }
    }
}
