namespace Demo22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Identity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Identity");
        }
    }
}
