using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogger.Models
{
    public class Blog
    {
        public long id { get; set; }
        public String title { get; set; }
        public DateTime date { get; set; }
        public String content { get; set; }
        public virtual List<Tag> tags { get; set; }
        public virtual List<Comment> comments { get; set; }
        
        public DateTime creationTS { get; set; }
        public DateTime updatedTS { get; set; }

        public Blog()
        {
            tags = new List<Tag>();
            comments = new List<Comment>();
        }

    }
}