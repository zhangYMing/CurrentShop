namespace MyBallShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MYBALLSHOP_ACCOUNT",
                c => new
                    {
                        GUID = c.String(nullable: false, maxLength: 128),
                        EMAILID = c.String(nullable: false, maxLength: 64, unicode: false),
                        PASSWORD = c.String(nullable: false, maxLength: 8000, unicode: false),
                        PHONENUMBER = c.String(maxLength: 8000, unicode: false),
                        ADDRESS = c.String(maxLength: 4000),
                        M_GUID = c.String(nullable: false, maxLength: 64, unicode: false),
                        CREATEUSERID = c.String(maxLength: 64, unicode: false),
                        CREATETIME = c.DateTime(),
                        MODIFYUSERID = c.String(maxLength: 64, unicode: false),
                        MODIFYTIME = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.GUID, t.EMAILID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MYBALLSHOP_ACCOUNT");
        }
    }
}
