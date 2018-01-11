using API_ClientS.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Logins = new HashSet<Logins>();
            Rates = new HashSet<Rates>();
            Videos = new HashSet<Videos>();
        }
        [Key]
        public Guid IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Logins> Logins { get; set; }
        public ICollection<Rates> Rates { get; set; }
        public ICollection<Videos> Videos { get; set; }
    }
}
