namespace RmR.Migrations.ResumeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResumeName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resumes", "ResumeName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resumes", "ResumeName", c => c.Int(nullable: false));
        }
    }
}
