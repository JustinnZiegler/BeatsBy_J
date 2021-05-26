namespace BeatsBy_J_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rating", "Rating_RatingId", "dbo.Rating");
            DropIndex("dbo.Rating", new[] { "Rating_RatingId" });
            DropColumn("dbo.Rating", "Rating_RatingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rating", "Rating_RatingId", c => c.Int());
            CreateIndex("dbo.Rating", "Rating_RatingId");
            AddForeignKey("dbo.Rating", "Rating_RatingId", "dbo.Rating", "RatingId");
        }
    }
}
