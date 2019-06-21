using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseFirstLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeFirstEntityFrameworkTestProject.BaseClasses
{
    public class TestBase
    {
        protected TestContext TestContextInstance;
        public TestContext TestContext
        {
            get => TestContextInstance;
            set
            {
                TestContextInstance = value;
                TestResults.Add(TestContext);
            }
        }

        public static IList<TestContext> TestResults;
        public Blog ThreePostBlog()
        {
            var blog = new Blog()
            {
                Title = "Learning EF",
                Name = "Karen 1",
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
