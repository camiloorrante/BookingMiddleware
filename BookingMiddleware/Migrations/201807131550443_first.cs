namespace BookingMiddleware.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        apiId = c.Int(nullable: false),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false),
                        ClientLastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "CityId", "dbo.Cities");
            DropIndex("dbo.Reservations", new[] { "CityId" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Cities");
        }
    }
}
