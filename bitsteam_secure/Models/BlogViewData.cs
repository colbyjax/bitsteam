using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Blogger.Models
{
    public class BlogViewData
    {
        public Blog currentBlog { get; set; }

        public virtual List<Tag> allTags { get; set; }
        public virtual List<Comment> allComments { get; set; }
        public virtual List<Blog> allBlogs { get; set; }

        public int[] SelectedTags {get;set;}


        public BlogViewData()
        {
            allTags = new List<Tag>();
            allComments = new List<Comment>();
            allBlogs = new List<Blog>();
        }
    }
}