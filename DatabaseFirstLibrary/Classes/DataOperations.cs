using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using BaseConnectionLibrary.ConnectionClasses;

namespace DatabaseFirstLibrary.Classes
{
    public class DataOperations : SqlServerConnection
    {
        /// <summary>
        /// Determine State by if the blog id is 0 or not, if
        /// 0 then add else update.
        /// </summary>
        /// <param name="pBlog"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
        /// </remarks>
        public bool AddOrUpdateBlog(Blog pBlog)
        {
            mHasException = false;

            try
            {
                using (var context = new BloggingContext())
                {
                    context.Entry(pBlog).State = pBlog.BlogId == 0 ? EntityState.Added : EntityState.Modified;
                    return context.SaveChanges() == 4;
                }
            }
            catch (DbEntityValidationException vex)
            {
                /*
                 * Start of validation
                 */
                var errors = vex.EntityValidationErrors;
                mHasException = true;
                mLastException = vex;
                return false;
            }
            catch (Exception ex)
            {
                mHasException = true;
                mLastException = ex;
                return false;
            }
        }

        /// <summary>
        /// Given a new blog attach to the context and add to
        /// the database along with any post attached to the blog
        /// </summary>
        /// <param name="pBlog"></param>
        /// <param name="pLog">If true write log to output window or output window</param>
        /// <returns></returns>
        public bool AddBlog(Blog pBlog, bool pLog = false)
        {
            mHasException = false;

            try
            {
                using (var context = new BloggingContext())
                {
                    if (pLog)
                    {
                        context.Database.Log = Console.Write;
                    }

                    context.Entry(pBlog).State = EntityState.Added;
                    return context.SaveChanges() == 4;
                }
            }
            catch (DbEntityValidationException vex)
            {
                /*
                 * Start of validation
                 */
                var errors = vex.EntityValidationErrors;
                mHasException = true;
                mLastException = vex;
                return false;
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
                    Title = "Dummy",
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
                var test =  context.Blogs
                    .Where(o => o.BlogId == blog.BlogId)
                    .SelectMany(o => o.Posts)
                    .Count();
                return test;
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
