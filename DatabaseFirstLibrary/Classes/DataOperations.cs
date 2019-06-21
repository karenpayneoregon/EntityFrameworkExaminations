using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using BaseConnectionLibrary;
using BaseConnectionLibrary.ConnectionClasses;

namespace DatabaseFirstLibrary.Classes
{
    public class DataOperations : SqlServerConnection
    {
        /// <summary>
        /// Given a new blog attach to the context and add to
        /// the database along with any post attached to the blog
        /// </summary>
        /// <param name="pBlog"></param>
        /// <returns></returns>
        public bool AddBlog(Blog pBlog)
        {
            mHasException = false;

            try
            {
                using (var context = new BloggingContext())
                {
                    context.Entry(pBlog).State = EntityState.Added;
                    return context.SaveChanges() == 4;
                }
            }
            catch (Exception ex)
            {
                mHasException = true;
                mLastException = ex;
                return false;
            }
        }

        public int AddBlog()
        {
            using (var context = new BloggingContext())
            {
                var blog = context.Blogs.Add(new Blog()
                {
                    Name = "All about EF",
                    Posts = new List<Post>()
                });

                blog.Posts = new List<Post>()
                {
                    new Post() {Title = "Parts 1", Content = "Let's get started"},
                    new Post() {Title = "Parts 2", Content = "History of EF"},
                    new Post() {Title = "Parts 3", Content = "Different model types"}
                };

                context.SaveChanges();

                // get post count without returning data
                return context.Blogs
                    .Where(o => o.BlogId == blog.BlogId)
                    .SelectMany(o => o.Posts)
                    .Count();
            }
        }
        /// <summary>
        /// Removes blogs and children post records.
        /// </summary>
        /// <remarks>
        /// Can be done with EF but a tad slower then the data provider.
        /// </remarks>
        public void RemoveAllBlogs()
        {
            var sqlStatements = "DELETE FROM dbo.Blogs;DBCC CHECKIDENT ('dbo.Blogs',RESEED, 0)";

            DatabaseServer = ".\\SQLEXPRESS";
            DefaultCatalog = "DatabaseFirst.Blogging";

            using (var cn = new SqlConnection {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand {Connection = cn, CommandText =  sqlStatements})
                {
                    
                    cn.Open();
                    cmd.ExecuteNonQuery();

                }
            }

        }
    }
}
