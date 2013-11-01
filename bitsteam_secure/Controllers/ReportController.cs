using BitSteam.Models;
using Blogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitSteam.Controllers
{
    public class ReportController : Controller
    {
        // Define the model
        private BloggerContext db = new BloggerContext("bitsteam-db");
        ReportViewData model;

        // Constructor
        public ReportController()
        {
            //Instantiate the model & pull back the needed data
            model = new ReportViewData();
            model.allTags = (from tag in db.Tags
                             orderby tag.name ascending
                             select tag).ToList();

        }

        //
        // GET: /Report/
        // By Default Displays clickable Tags (Categories)
        public ActionResult Index()
        {
            return View("Categories",model);
        }

        public ActionResult Categories()
        {
            return View(model);
        }

        // Display all posts with the chosen tag
        public ActionResult ChooseTag(long tagId)
        {
            // Find all blogs with the Chosen Tag and set Current Tag to it.

            // Find Tag Object
            Tag currentTag = (Tag) db.Tags.Single(x => x.id == tagId);

            // Retrieve all blogs containing the Tag object
            List<Blog> blogs = db.Blogs.Where(x => x.tags.Any(y => y.id == tagId)).ToList();

            model.currentTag = currentTag;
            model.chosenBlogs = blogs;

            return View("Categories", model);
        }

        public ActionResult ChooseBlog(long blogId)
        {
            return RedirectToAction("ChooseBlog", "Home", new{ blog_Id=blogId}) ;
        }

        public ActionResult ChooseDate(long month, long day, long year)
        {
            // Set the chosenDate as a flag
            //model.chosenDate = month;

            DateTime startDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
            DateTime endDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));

            // Find all blogs that fall within the dates
            List<Blog> blogs = (from blog in db.Blogs
                                where blog.date > startDate &&
                                blog.date < endDate
                                orderby blog.date ascending
                                select blog).ToList();

            model.chosenBlogs = blogs;

            return View("Archives", model);

        }

        public ActionResult Archives()
        {
            return View("Archives", model);
        }
    }
}
