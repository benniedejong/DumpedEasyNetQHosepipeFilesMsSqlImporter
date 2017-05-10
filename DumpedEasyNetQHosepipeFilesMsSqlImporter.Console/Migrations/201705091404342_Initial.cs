namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EasyNetQHosepipeDumpeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageFilePath = c.String(maxLength: 2048),
                        RawMessage = c.String(),
                        PropertiesFilePath = c.String(maxLength: 2048),
                        RawProperties = c.String(),
                        InfoFilePath = c.String(maxLength: 2048),
                        RawInfo = c.String(),
                        Info_Id = c.Int(),
                        Properties_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EasyNetQHosepipeDumpedInfoes", t => t.Info_Id)
                .ForeignKey("dbo.EasyNetQHosepipeDumpedProperties", t => t.Properties_Id)
                .Index(t => t.Info_Id)
                .Index(t => t.Properties_Id);
            
            CreateTable(
                "dbo.EasyNetQHosepipeDumpedInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsumerTag = c.String(),
                        DeliverTag = c.Long(nullable: false),
                        Redelivered = c.Boolean(nullable: false),
                        Exchange = c.String(),
                        RoutingKey = c.String(),
                        Queue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EasyNetQHosepipeDumpedProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentType = c.String(),
                        ContentEncoding = c.String(),
                        Headers = c.String(),
                        DeliveryMode = c.Byte(nullable: false),
                        Priority = c.Byte(nullable: false),
                        CorrelationId = c.String(),
                        ReplyTo = c.String(),
                        Expiration = c.String(),
                        MessageId = c.String(),
                        Timestamp = c.Long(nullable: false),
                        Type = c.String(),
                        UserId = c.String(),
                        AppId = c.String(),
                        ClusterId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EasyNetQHosepipeDumpeds", "Properties_Id", "dbo.EasyNetQHosepipeDumpedProperties");
            DropForeignKey("dbo.EasyNetQHosepipeDumpeds", "Info_Id", "dbo.EasyNetQHosepipeDumpedInfoes");
            DropIndex("dbo.EasyNetQHosepipeDumpeds", new[] { "Properties_Id" });
            DropIndex("dbo.EasyNetQHosepipeDumpeds", new[] { "Info_Id" });
            DropTable("dbo.EasyNetQHosepipeDumpedProperties");
            DropTable("dbo.EasyNetQHosepipeDumpedInfoes");
            DropTable("dbo.EasyNetQHosepipeDumpeds");
        }
    }
}
