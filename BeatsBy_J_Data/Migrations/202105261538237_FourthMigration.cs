namespace BeatsBy_J_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Album", name: "SongId", newName: "Song_SongId");
            RenameIndex(table: "dbo.Album", name: "IX_SongId", newName: "IX_Song_SongId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Album", name: "IX_Song_SongId", newName: "IX_SongId");
            RenameColumn(table: "dbo.Album", name: "Song_SongId", newName: "SongId");
        }
    }
}
