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
    }
}
