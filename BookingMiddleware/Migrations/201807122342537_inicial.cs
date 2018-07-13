namespace BookingMiddleware.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "CityID", "dbo.Cities");
            DropPrimaryKey("dbo.Cities");
            AlterColumn("dbo.Cities", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Cities", "Id");
            AddForeignKey("dbo.Reservations", "CityID", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "CityID", "dbo.Cities");
            DropPrimaryKey("dbo.Cities");
            AlterColumn("dbo.Cities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cities", "Id");
            AddForeignKey("dbo.Reservations", "CityID", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
