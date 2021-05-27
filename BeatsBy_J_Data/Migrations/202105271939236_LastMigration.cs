namespace BeatsBy_J_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Titles = c.String(),
                        AlbumName = c.String(nullable: false, maxLength: 100),
                        AlbumReleaseDate = c.DateTime(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistId);
            
            CreateTable(
                "dbo.Song",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ArtistId = c.Int(),
                        ArtistName = c.String(),
                        GenreId = c.Int(nullable: false),
                        GenreName = c.String(),
                        AlbumId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Artist", t => t.ArtistId)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        EnjoymentScore = c.Double(nullable: false),
                        SongLengthScore = c.Double(nullable: false),
                        ArtistStyleScore = c.Double(nullable: false),
                        Description = c.String(maxLength: 300),
                        UserId = c.Guid(nullable: false),
                        SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Song", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Song", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropForeignKey("dbo.Rating", "SongId", "dbo.Song");
            DropForeignKey("dbo.Song", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Song", "ArtistId", "dbo.Artist");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Rating", new[] { "SongId" });
            DropIndex("dbo.Song", new[] { "AlbumId" });
            DropIndex("dbo.Song", new[] { "GenreId" });
            DropIndex("dbo.Song", new[] { "ArtistId" });
            DropIndex("dbo.Album", new[] { "ArtistId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Rating");
            DropTable("dbo.Genre");
            DropTable("dbo.Song");
            DropTable("dbo.Artist");
            DropTable("dbo.Album");
        }
    }
}
