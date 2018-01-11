using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public partial class Rates
    {
        [Key]
        public Guid IdRate { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdVideo { get; set; }
        public int Rate { get; set; }
        public DateTime DateTime { get; set; }

        //public Users IdUserNavigation { get; set; }
        //public Videos IdVideoNavigation { get; set; }
    }
}
