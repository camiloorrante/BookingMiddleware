namespace BookingMiddleware.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientLastName = c.String(),
                        Email = c.String(),
                        Duration = c.Time(nullable: false, precision: 7),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .Index(t => t.CityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "CityID", "dbo.Cities");
            DropIndex("dbo.Reservations", new[] { "CityID" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Cities");
        }
    }
}
