using BeatsBy_J_Data;
using BeatsBy_J_Models.Album;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Services
{
    public class AlbumService
    {
        private readonly Guid _userId;
        public AlbumService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateAlbum(AlbumCreate model)
        {
            var entity = new Album()
            {
                ArtistId = model.ArtistId,
                AlbumName = model.AlbumName,
                //SongId = model.SongId,
                AlbumReleaseDate = model.AlbumReleaseDate,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Albums.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AlbumList> GetAlbums()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Albums.Select(e =>
                new AlbumList()
                {
                    AlbumName = e.AlbumName,
                    AlbumId = e.AlbumId
                });

                return query.ToArray();
            }
        }

        public AlbumDetail GetAlbumById(int albumId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Albums.Include(e => e.SongsInAlbum).Single(e => e.AlbumId == albumId);
                var test = ctx.Songs.Where(e => e.AlbumId == albumId).ToList();
                foreach (var item in test)
                {
                    entity.SongsInAlbum.Add(item);
                }


                var songsByAlbum = new List<string>();
                foreach (var song in entity.SongsInAlbum)
                {
                    songsByAlbum.Add(song.Title);
                }

                return new AlbumDetail()
                {
                    AlbumId = entity.AlbumId,
                    AlbumName = entity.AlbumName,
                    AlbumReleaseDate = entity.AlbumReleaseDate,
                    SongTitlesByAlbum = songsByAlbum
                };
            }
        }

        public AlbumDetail GetAlbumByName(string albumName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Albums.Include(e => e.SongsInAlbum).Single(e => e.AlbumName == albumName);

                var songsByAlbum = new List<string>();
                foreach (var song in entity.SongsInAlbum)
                {
                    songsByAlbum.Add(song.Title);
                }

                return new AlbumDetail()
                {
                    AlbumId = entity.AlbumId,
                    AlbumName = entity.AlbumName,
                    AlbumReleaseDate = entity.AlbumReleaseDate,
                    SongTitlesByAlbum = songsByAlbum
                };
            }
        }

        public bool UpdateAlbum(AlbumUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Albums.Single(e => e.AlbumId == model.AlbumId);

                entity.AlbumName = model.AlbumName;
                entity.AlbumReleaseDate = model.AlbumReleaseDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAlbum(int albumId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Albums.Single(e => e.AlbumId == albumId);
                ctx.Albums.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
