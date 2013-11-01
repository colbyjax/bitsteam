using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogger.Models
{
    public class Tag
    {
        public long id { get; set; }
        public String name { get; set; }
        public virtual List<Blog> blogs { get; set; }
        
       //public String changeDB { get; set; }
    }
}