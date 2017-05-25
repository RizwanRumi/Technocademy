using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TechnocademyWebSolution.Models
{
    public class ProjectDB : DbContext
    {
        public ProjectDB() : base("AngularTest")
        {
            
        }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<VideoUpload> VideoUploads { get; set; }
    }
}