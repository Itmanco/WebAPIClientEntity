using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models.Enum
{
    public enum UserType
    {
        [Description("Admin")]
        Admin = 0,

        [Description("Poster")]
        Poster = 1,

        [Description("Watcher")]
        Watcher = 2
    }
}
