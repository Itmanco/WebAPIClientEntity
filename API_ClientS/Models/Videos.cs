using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public partial class Videos
    {
        public Videos()
        {
            Comments = new HashSet<Comments>();
            Rates = new HashSet<Rates>();
        }
        [Key]
        public Guid IdVideo { get; set; }
        public Guid IdUser { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public Users IdUserNavigation { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Rates> Rates { get; set; }

    }
}
