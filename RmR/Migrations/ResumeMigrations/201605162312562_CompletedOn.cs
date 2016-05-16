namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resumes", "CompletedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resumes", "CompletedOn");
        }
    }
}
