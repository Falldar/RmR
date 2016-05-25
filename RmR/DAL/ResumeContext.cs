using RmR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmR.DAL
{
    class ResumeContext : DbContext
    {
        public ResumeContext() : base("DefaultConnection")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
                    
        //    modelBuilder.Entity<Resume>()
        //       .HasMany(c => c.Client).WithMany(i => i.Resume)
        //       .Map(t => t.MapLeftKey("CourseID").MapRightKey("InstructorID").ToTable("CourseInstructor"));

        //    //The above code will create a junction (bridging) table called "CourseInstructor
        //    //with 2 FK columns CourseID, and InstructorID
        //    //CourseID -> Course
        //    //InstructorID -> Instructor
        //    //and a composite PK on CourseID + InstructorID
        //}
    }


}
