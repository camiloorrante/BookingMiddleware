namespace BookingMiddleware.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificacioncityId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "CityID", "dbo.Cities");
            DropPrimaryKey("dbo.Cities");
            DropColumn("dbo.Cities", "Id");
            AddColumn("dbo.Cities", "CityId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cities", "CityId");
            AddForeignKey("dbo.Reservations", "CityID", "dbo.Cities", "CityId", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cities", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Reservations", "CityID", "dbo.Cities");
            DropPrimaryKey("dbo.Cities");
            DropColumn("dbo.Cities", "CityId");
            AddPrimaryKey("dbo.Cities", "Id");
            AddForeignKey("dbo.Reservations", "CityID", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
