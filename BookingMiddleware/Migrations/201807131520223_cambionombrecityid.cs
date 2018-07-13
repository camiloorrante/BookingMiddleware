namespace BookingMiddleware.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambionombrecityid : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservations", new[] { "CityID" });
            CreateIndex("dbo.Reservations", "CityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reservations", new[] { "CityId" });
            CreateIndex("dbo.Reservations", "CityID");
        }
    }
}
