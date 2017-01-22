namespace University.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change2Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnrollCourses", "GradeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnrollCourses", "GradeName");
        }
    }
}
