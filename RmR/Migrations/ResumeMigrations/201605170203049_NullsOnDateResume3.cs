namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullsOnDateResume3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resumes", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resumes", "CreatedOn", c => c.DateTime());
        }
    }
}
