using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public partial class Logins
    {
        [Key]
        public Guid IdLogin { get; set; }
        public Guid IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Users IdUserNavigation { get; set; }
    }
}
