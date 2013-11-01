using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogger.Models
{
    public class Comment
    {
        public long id { get; set; }
        public string author { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }
        public Blog blog { get; set; }

        public DateTime creationTS { get; set; }
        public DateTime updatedTS { get; set; }

    }
}