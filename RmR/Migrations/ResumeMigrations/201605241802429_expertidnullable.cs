namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expertidnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resumes", "ExpertID", "dbo.Users");
            DropIndex("dbo.Resumes", new[] { "ExpertID" });
            AlterColumn("dbo.Resumes", "ExpertID", c => c.Int());
            CreateIndex("dbo.Resumes", "ExpertID");
            AddForeignKey("dbo.Resumes", "ExpertID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resumes", "ExpertID", "dbo.Users");
            DropIndex("dbo.Resumes", new[] { "ExpertID" });
            AlterColumn("dbo.Resumes", "ExpertID", c => c.Int(nullable: false));
            CreateIndex("dbo.Resumes", "ExpertID");
            AddForeignKey("dbo.Resumes", "ExpertID", "dbo.Users", "ID", cascadeDelete: false);
        }
    }
}
