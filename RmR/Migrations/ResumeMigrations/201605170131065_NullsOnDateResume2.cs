namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullsOnDateResume2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resumes", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Resumes", "CompletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resumes", "CompletedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Resumes", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
