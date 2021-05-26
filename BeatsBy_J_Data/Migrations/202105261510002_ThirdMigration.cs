namespace BeatsBy_J_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Album", "SongId", "dbo.Song");
            DropIndex("dbo.Album", new[] { "SongId" });
            AlterColumn("dbo.Album", "SongId", c => c.Int());
            CreateIndex("dbo.Album", "SongId");
            AddForeignKey("dbo.Album", "SongId", "dbo.Song", "SongId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Album", "SongId", "dbo.Song");
            DropIndex("dbo.Album", new[] { "SongId" });
            AlterColumn("dbo.Album", "SongId", c => c.Int(nullable: false));
            CreateIndex("dbo.Album", "SongId");
            AddForeignKey("dbo.Album", "SongId", "dbo.Song", "SongId", cascadeDelete: true);
        }
    }
}
