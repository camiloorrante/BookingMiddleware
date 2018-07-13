namespace BookingMiddleware.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombresenreservation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "ClientName", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "ClientLastName", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Email", c => c.String());
            AlterColumn("dbo.Reservations", "ClientLastName", c => c.String());
            AlterColumn("dbo.Reservations", "ClientName", c => c.String());
        }
    }
}
