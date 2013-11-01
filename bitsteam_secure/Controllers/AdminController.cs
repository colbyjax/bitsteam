using bitsteam_secure.Models;
using Blogger.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitSteam.Controllers
{
    [Authorize(Roles="Admins")]
    public class AdminController : Controller
    {
        private BloggerContext db = new BloggerContext("bitsteam-db");
        BlogViewData model;
        //
        // GET: /Admin/

        public AdminController()
        {
            model = new BlogViewData();

            // Fill four of the most recent blogs
            model.allTags = (from tag in db.Tags
                                    orderby tag.name ascending
                                    select tag).ToList();
        }

        [Authorize]
        public ActionResult Index()
        {

            return View("Admin", model);
        }

        [Authorize]
        public ActionResult Save(BlogViewData model, string blog_title, string blog_content)
        {
            Blog newBlog = new Blog();
            newBlog.title = blog_title;
            newBlog.date = System.DateTime.Now;
            newBlog.content = blog_content;
            newBlog.creationTS = System.DateTime.Now;
            newBlog.updatedTS = System.DateTime.Now;
            int[] tagIds = model.SelectedTags;

            List<Tag> selectedTags = new List<Tag>();
            // Get the Tag objects from the DB
            for (int i = 0; i < tagIds.Count(); i++)
            {
                int currentTag = tagIds[i];
                Tag t = (from tag in db.Tags
                         where tag.id == currentTag
                         select tag).FirstOrDefault();
                selectedTags.Add(t);
            }

            // Add the Tag Objects to the Blog Object
            newBlog.tags.AddRange(selectedTags);

            db.Blogs.Add(newBlog);
            db.SaveChanges();

            //Reset the Tags
            model.allTags = (from tag in db.Tags
                             orderby tag.name ascending
                             select tag).ToList();

            return RedirectToAction("Index");
            
        }

        public ActionResult AddTag(string new_tag)
        {
            Tag newTag = new Tag();
            newTag.name = new_tag;
            db.Tags.Add(newTag);
            db.SaveChanges();

            //Reset the Tags
            model.allTags = (from tag in db.Tags
                             orderby tag.name ascending
                             select tag).ToList();
            return RedirectToAction("Index");
        }

        public ActionResult AddUserRole()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            // Create Admin Role
            string roleName = "Admins";
            IdentityResult roleResult;
            roleResult = IdentityResult.Failed();

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));

            }

            // Create User root
            var user = new ApplicationUser();
            user.UserName = "root";
            var userResult = UserManager.Create(user, "douyahoo2");

            // Add User to Role
            if (userResult.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }
           

            roleName = "Users";

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            // Create User colby
            user = new ApplicationUser();
            user.UserName = "colby";
            userResult = UserManager.Create(user, "douyahoo2");

            // Add User to Role
            if (userResult.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }

            return View("Index");
        }
    }
}
