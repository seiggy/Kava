using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kava.Models
{
    public class ClassJob
    {
        public string ExpCurrent { get; set; }
        public string ExpLevel { get; set; }
        public int ExpTotal { get; set; }
        public string Icon { get; set; }
        public string IconQueryString { get; set; }
        public string IconTimestamp { get; set; }
        public int Id { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public int RealId { get; set; }

    }
}
