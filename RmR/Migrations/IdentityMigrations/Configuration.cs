namespace RmR.Migrations.IdentityMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RmR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\IdentityMigrations";
            ContextKey = "RmR.Models.ApplicationDbContext";
        }

        protected override void Seed(RmR.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //1. Add admin role
            if ((!context.Roles.Any(r => r.Name == "admin")))
            {
                //role does not exist - create it
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roleToInsert = new IdentityRole { Name = "admin" };
                roleManager.Create(roleToInsert);
            }

            //2. Add student role
            if ((!context.Roles.Any(r => r.Name == "user")))
            {
                //role does not exist - create it
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roleToInsert = new IdentityRole { Name = "client" };
                roleManager.Create(roleToInsert);
            }

            //3. Add Instructor role
            if ((!context.Roles.Any(r => r.Name == "expert")))
            {
                //role does not exist - create it
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roleToInsert = new IdentityRole { Name = "expert" };
                roleManager.Create(roleToInsert);
            }

            //4. add admin user and assign admin role
            if ((!context.Users.Any(u => u.UserName == "admin@rmr.com")))
            {
                //admin user does not exist - create it
                var userStore = new UserStore<Models.ApplicationUser>(context);
                var userManager = new UserManager<Models.ApplicationUser>(userStore);
                var userToInsert = new Models.ApplicationUser
                {
                    UserName = "admin@rmr.com",
                    Email = "admin@rmr.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "Admin@123456");

                //assign admin user to admin role
                userManager.AddToRole(userToInsert.Id, "admin");
            }

        }
    }
}
