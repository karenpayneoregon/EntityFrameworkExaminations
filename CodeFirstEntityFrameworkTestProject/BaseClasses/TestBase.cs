using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseFirstLibrary;

namespace CodeFirstEntityFrameworkTestProject.BaseClasses
{
    public class TestBase
    {
        public Blog ThreePostBlog()
        {
            var blog = new Blog()
            {
                Name = "All about EF",
                Posts = new List<Post>()
            };

            blog.Posts = new List<Post>()
            {
                new Post() {Title = "Parts 1", Content = "Let's get started"},
                new Post() {Title = "Parts 2", Content = "History of EF"},
                new Post() {Title = "Parts 3", Content = "Different model types"}
            };

            return blog;
        }
    }
}
