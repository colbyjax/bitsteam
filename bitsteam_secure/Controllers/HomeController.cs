using Blogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogger.Controllers
{
    public class HomeController : Controller
    {
        private BloggerContext db = new BloggerContext("bitsteam-db");
        BlogViewData blogViewData;

        public HomeController()
        {
            blogViewData = new BlogViewData();
            
            // Fill four of the most recent blogs
            blogViewData.allBlogs = (from blog in db.Blogs
                                     orderby blog.date descending
                                     select blog).Take(4).ToList();

            blogViewData.allTags = (from tag in db.Tags
                                    orderby tag.name ascending
                                    select tag).Take(4).ToList();
        }
        
        //
        // GET: /Home/

        public ActionResult Index()
        {            
            // Build all the Data the View will need

            // Start with the Blog
            Blog latestBlog = (from blog in db.Blogs.Include("Comments")
                               orderby blog.date descending
                               select blog).FirstOrDefault();

            blogViewData.currentBlog = latestBlog;

            return View(blogViewData);
        }

        /**
         * Invoked when a Specific Blog is chosen
         * */


        public ActionResult ChooseBlog(long blog_id)
        {
            Blog chosenBlog = (from blog in db.Blogs.Include("Comments")
                               where blog.id == blog_id
                               select blog).FirstOrDefault();
            blogViewData.currentBlog = chosenBlog;

            return View("Index", blogViewData);
        }

        public ActionResult ChooseTag(long tag_id)
        {
            return RedirectToAction("ChooseTag", "Report", new { tagId = tag_id });
        }

        [Authorize]
        public ActionResult AddComment(long blog_id, string new_comment)
        {
            Blog blog = null;

            if (ModelState.IsValid)
            {
                blog = db.Blogs.Find(blog_id);
                blog.comments.Add(new Comment
                {
                    author = User.Identity.Name,
                    date = System.DateTime.Now,
                    content = new_comment,
                    creationTS = System.DateTime.Now,
                    updatedTS = System.DateTime.Now
                });

                db.SaveChanges();
            }

            return PartialView("_Comments", blog.comments);
        }

        [Authorize]
        public ActionResult DeleteComment(long blog_id, long comment_id)
        {
            Blog blog = null;
            // Find the Comment and Remove it
            var deleteMe = db.Comments.Single(c => c.id == comment_id);

            // Check if the appropriate user is deleting
            if (deleteMe.author.Equals(User.Identity.Name))
            {
                // Remove the Comment from the Blog
                blog = db.Blogs.Find(blog_id);
                blog.comments.Remove(deleteMe);

                // Now Remove the Comment from the Comments Table
                db.Comments.Remove(deleteMe);

                db.SaveChanges();
                return PartialView("_Comments", blog.comments);
            }
            else
            {
                return View("NotAuthorized");
            }
            

        }

        public ActionResult ChooseDate(long month, long day)
        {
            int currentYear = DateTime.Now.Year;
            return RedirectToAction("ChooseDate", "Report", new {month = month, day = day, year = currentYear});
        }

        public ActionResult About()
        {
            return View("About", blogViewData);
        }

        public ActionResult FAQ()
        {
            return View("FAQ", blogViewData);
        }

        public ActionResult Test()
        {
            return View("Test");
        }

    }


}
