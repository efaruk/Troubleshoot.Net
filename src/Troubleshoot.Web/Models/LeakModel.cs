using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Troubleshoot.Web.Models
{
    public class LeakModel: BaseModel
    {
        public long TotalBytes { get; set; }
    }
}