using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public partial class Comments
    {
        [Key]
        public Guid IdComment { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdVideo { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
