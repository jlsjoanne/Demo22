namespace Demo22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "Identity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Identity", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "Gender");
        }
    }
}
