using Blogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitSteam.Models
{
    /***
     * Use for all Reporting on the blogs including Viewing categories and archives
     **/
    public class ReportViewData
    {
        public Tag currentTag { get; set; }

        public virtual List<Tag> allTags { get; set; }
        public virtual List<Blog> chosenBlogs { get; set; }

        public ReportViewData()
        {
            allTags = new List<Tag>();
            chosenBlogs = new List<Blog>();
        }
    }
}