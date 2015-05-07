using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kava.Models
{
    public class GrandCompany
    {
        public GrandCompanyData OrderoftheTwinAdder { get; set; }
        public GrandCompanyData Maelstrom { get; set; }
        public GrandCompanyData ImmortalFlames { get; set; }
    }
    public class GrandCompanyData
    {
        public string Company { get; set; }
        public string Icon { get; set; }
        public string Rank { get; set; }
    }
}
