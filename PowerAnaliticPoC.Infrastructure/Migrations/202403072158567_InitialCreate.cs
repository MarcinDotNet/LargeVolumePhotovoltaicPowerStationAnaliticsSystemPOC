namespace PowerAnaliticPoC.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PowerGeneratorDetailDatas",
                c => new
                    {
                        GeneratorId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        CurrentProduction = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeStamp ,t.GeneratorId });
            CreateIndex("dbo.PowerGeneratorDetailDatas", new string[] { "GeneratorId", "TimeStamp" }, false, "IX_PowerGeneratorDetailDatas_GeneratorID_TimeStamp");
            CreateIndex("dbo.PowerGeneratorDetailDatas", new string[] { "TimeStamp" }, false, "IX_PowerGeneratorDetailDatas_TimeStamp");

            CreateTable(
                "dbo.PowerGenerators",
                c => new
                    {
                        GeneratorId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                        Location = c.String(maxLength: 40),
                        ExpectedCurrent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GeneratorId);
            
            CreateTable(
                "dbo.PowerGeneratorTimeRangeDatas",
                c => new
                    {
                        GeneratorId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        TimeRange = c.Int(nullable: false),
                        CurrentProduction = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeStamp, t.TimeRange, t.GeneratorId });
            CreateIndex("dbo.PowerGeneratorTimeRangeDatas", new string[] { "GeneratorId", "TimeRange", "TimeStamp" }, false, "IX_PowerGeneratorTimeRangeDatas_GeneratorID_TimeRange_TimeStamp");
            CreateIndex("dbo.PowerGeneratorTimeRangeDatas", new string[] { "TimeRange", "TimeStamp" }, false, "IX_PowerGeneratorTimeRangeDatas_TimeRange_TimeStamp");



        }

        public override void Down()
        {
            DropTable("dbo.PowerGeneratorTimeRangeDatas");
            DropTable("dbo.PowerGenerators");
            DropTable("dbo.PowerGeneratorDetailDatas");
            //indexes will be removed automatically
        }
    }
}
