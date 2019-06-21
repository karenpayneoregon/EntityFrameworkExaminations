using System;
using System.Collections.Generic;
using System.Linq;
using CodeFirstEntityFrameworkTestProject.BaseClasses;
using DatabaseFirstLibrary;
using DatabaseFirstLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeFirstEntityFrameworkTestProject
{
    [TestClass(), TestCategory("EF6 code first with existing db")]
    public class UnitTest1 : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            Console.WriteLine(TestContext.TestName);
        }
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        /// <summary>
        /// Demonstration for adding a parent record and several
        /// child records connected.
        /// </summary>
        [TestMethod]
        public void AddBlogWithPosts_ConnectedTest() 
        {
            var ops = new DataOperations();
            Assert.IsTrue(ops.AddBlog() == 3, 
                "Expected three post for adding new blog connected");
        }
        /// <summary>
        /// Demonstration for adding a parent record and several
        /// child records disconnected
        /// NOTE: Passing true to AddBlog second parameter will log EF work.
        /// </summary>
        [TestMethod]
        public void AddBlogWithPosts_DisconnectedTest()
        {
            var ops = new DataOperations();
            Assert.IsTrue(ops.AddBlog(ThreePostBlog(),true), 
                "Expected adding blog disconnection to function");

        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            var ops = new DataOperations();
            ops.RemoveAllBlogs();
        }
    }
}
