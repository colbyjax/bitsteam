using bitsteam_secure.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blogger.Models
{
    public class BloggerInitializer : CreateDatabaseIfNotExists<BloggerContext> 
    //public class BloggerInitializer : DropCreateDatabaseIfModelChanges<BloggerContext>
    {
        protected override void Seed(BloggerContext context)
        {
            Tag j2ee = new Tag { name = "J2EE" };
            Tag asp = new Tag { name = "Asp.net MVC" };
            Tag zen = new Tag { name = "Zen" };
            
            context.Tags.Add(j2ee);
            context.Tags.Add(asp);
            context.Tags.Add(zen);

            Blog blog1 = new Blog
            {
                title = "Welcome",
                date = new DateTime(2013, 08, 15),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "_WelcomeBlog.cshtml"
            };

            blog1.tags.Add(zen);

            Comment comment1 = new Comment
            {
                author = "Peter",
                date = new DateTime(2013, 09, 15),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "Peter Says: Suspendisse ornare eros nec augue euismod id pharetra ante pharetra Quisque ut turpis tellus, vel aliquet lorem. Duis ultricies fringilla nulla, vitae faucibus"
            };

            Comment comment2 = new Comment
            {
                author = "Lois",
                date = new DateTime(2013, 10, 15),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "Phasellus sit amet nisi quis elit ultrices scelerisque nec faucibus ipsum. Vestibulum luctus"
            };

            Comment comment3 = new Comment
            {
                author = "Stewie",
                date = new DateTime(2013, 10, 15),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "Sed tellus lorem, lacinia vel lacinia nec, cursus non justo. Pellentesque dapibus viverra " 
            };

            blog1.comments.Add(comment1);
            blog1.comments.Add(comment2);
            blog1.comments.Add(comment3);

            Blog blog2 = new Blog
            {
                title = "Gitting Started",
                date = new DateTime(2013, 09, 1),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "_GittingStarted.cshtml"
            };
            blog2.tags.Add(j2ee);
            blog2.tags.Add(asp);

            Blog blog3 = new Blog
            {
                title = "Where To Start",
                date = new DateTime(2013, 09, 22),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "_WhereToStart.cshtml" 
            };

            blog3.tags.Add(zen);

            Comment comment4 = new Comment
            {
                author = "Meg",
                date = new DateTime(2013, 10, 15),
                creationTS = System.DateTime.Now,
                updatedTS = System.DateTime.Now,
                content = "Integer ornare molestie massa a egestas. Donec mauris ante, vehicula volutpat gravida at, mattis ac felis. " +
                    "Vivamus mi urna, gravida vel placerat nec, ullamcorper non nunc. Fusce a felis sed felis lacinia eleifend non "
            };

            blog3.comments.Add(comment4);

            context.Blogs.Add(blog1);
            context.Blogs.Add(blog2);
            context.Blogs.Add(blog3);

            // Now that we have some blogs, lets setup some users and Roles
 

            base.Seed(context);
        }

    }
}