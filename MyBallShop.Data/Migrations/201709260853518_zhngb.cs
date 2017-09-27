namespace MyBallShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zhngb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MYBALLSHOP_ACCOUNT", "NAME", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MYBALLSHOP_ACCOUNT", "NAME");
        }
    }
}
