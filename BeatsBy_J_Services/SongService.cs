using BeatsBy_J_Data;
using BeatsBy_J_Models;
using BeatsBy_J_Models.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace BeatsBy_J_Services
{
    public class SongService
    {
        private readonly Guid _userId;
        public SongService(Guid userId)
        {
            _userId = userId;
        }
        //private GenreService _genreService = new GenreService(_userId);
        public bool CreateSong(SongCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var _genreService = new GenreService(_userId);
                //var ctx = new ApplicationDbContext();
                //var Genre = _genreService.GetGenreByName(model.GenreName);
                //var GenreId = Genre.GenreId;
                string genreName = "";
                foreach (var item in ctx.Genres.ToList())
                {
                    if (item.GenreId == model.GenreId)
                        genreName = item.GenreName;

                }
            var entity = new Song()
            {
                OwnerId = _userId,
                Title = model.Title,
                ArtistId = model.ArtistId,
                ArtistName = model.ArtistName,
                GenreId = model.GenreId,
                GenreName = genreName,
                AlbumId = model.AlbumId,
                Date = model.Date,
            };
            //using (ctx)
                ctx.Songs.Add(entity);
                var artistEntity = ctx.Artists.Find(model.ArtistId);
                artistEntity.SongsByArtist.Add(entity);

                var test = ctx.Genres.ToList();
                foreach (var item in test)
                {
                    if (item.GenreName == model.GenreName)
                    {

                        var genreEntity = ctx.Genres.Find(item.GenreId);
                        genreEntity.SongsInGenre.Add(entity);
                    }
                }
                //var albumEntity = ctx.Albums.Find(model.AlbumId);

                //albumEntity.SongsInAlbum.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<SelectListItem> GetGenres()
        {
            using (var ctx= new ApplicationDbContext())
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var item in ctx.Genres.ToList())
                {
                    items.Add(new SelectListItem { Text = item.GenreName, Value = item.GenreId.ToString() });
                }
                return items;
            }
        }
        public IEnumerable<SongList> GetSongs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Include(e => e.Artist);

                var listOfSongs = new List<SongList>();
                foreach (var song in query)
                {
                    var dateAsString = song.Date.ToShortDateString();
                    listOfSongs.Add(new SongList()
                    {
                        Title = song.Title,
                        ArtistName = song.Artist.ArtistName,
                        GenreName = song.GenreName,
                        Date = dateAsString,
                        SongId = song.SongId
                    });
                }

                return listOfSongs.ToArray();
            }
        }

        public IEnumerable<SongList> GetSongsByDate(DateTime startDate, DateTime endDate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Include(e => e.Artist).Where(e => e.Date >= startDate && e.Date <= endDate);

                var listOfSongs = new List<SongList>();
                foreach (var song in query)
                {
                    var dateAsString = song.Date.ToShortDateString();
                    listOfSongs.Add(new SongList()
                    {
                        Title = song.Title,
                        ArtistName = song.Artist.ArtistName,
                        Date = dateAsString,
                        SongId = song.SongId
                    });
                }

                return listOfSongs.ToArray();
            }
        }

        public SongDetail GetSongById(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Find(songId);

                // Aaron's Edit
                var ratingDb = ctx.Ratings.Find(songId);

                //var entity = ctx.Songs.Include(e => e.Artist).Include(e => e.Album).Include(e => e.Genre).Include(e => e.RatingsForSong)
                // .Single(e => e.SongId == songId);


                var listOfRatings = new List<RatingForListInSongDetail>();
                foreach (var rating in entity.RatingsForSong)
                {
                    listOfRatings.Add(new RatingForListInSongDetail()
                    {
                        ScoreAverage = rating.ScoreAverage,
                        Description = rating.Description,
                        UserId = rating.UserId
                    });
                }
                return new SongDetail()
                {
                    SongId = entity.SongId,
                    Title = entity.Title,
                    ArtistId= entity.ArtistId,
                    ArtistName = entity.ArtistName,
                    GenreId = entity.GenreId,
                    GenreName = ctx.Songs.FirstOrDefault(e => e.GenreId==entity.GenreId).GenreName,
                    AlbumId = entity.AlbumId,
                    //Artist = entity.Artist,
                    //Genre = entity.Genre,
                    //Album = entity.Album,
                    Date = entity.Date,
                    AverageRating = entity.AverageRating,
                    IsRecommended = entity.IsRecommended,
                    RatingsForSong = listOfRatings
                };
            }
        }

        public SongDetail GetSongByName(string songName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Find(songName);
                //var entity = ctx.Songs.Include(e => e.Artist).Include(e => e.Album).Include(e => e.Genre).Include(e => e.RatingsForSong)
                //    .Single(e => e.Title == songName);

                var listOfRatings = new List<RatingForListInSongDetail>();
                foreach (var rating in entity.RatingsForSong)
                {
                    listOfRatings.Add(new RatingForListInSongDetail()
                    {
                        ScoreAverage = rating.ScoreAverage,
                        Description = rating.Description,
                        UserId = rating.UserId
                    });
                }
                return new SongDetail()
                {
                    SongId = entity.SongId,
                    Title = entity.Title,
                    ArtistName = entity.Artist.ArtistName,
                    ArtistId = (int)entity.ArtistId,
                    GenreId = entity.GenreId,
                    //Album = entity.Album,
                    Date = entity.Date,
                    AverageRating = entity.AverageRating,
                    IsRecommended = entity.IsRecommended,
                    RatingsForSong = listOfRatings
                };
            }
        }

        public bool UpdateSong(SongUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Find(model.SongId);
                string genreName = "";
                foreach (var item in ctx.Genres.ToList())
                {
                    if (item.GenreId == model.GenreId)
                        genreName = item.GenreName;
                }

                int? artistId = entity.ArtistId;
                int genreId = entity.GenreId;
                //int albumId = entity.AlbumId;

                entity.Title = model.Title;
                entity.Date = model.Date;
                entity.ArtistId = model.ArtistId;
                entity.GenreId = model.GenreId;
                entity.AlbumId = model.AlbumId;
                entity.GenreName = genreName;

                if (artistId != entity.ArtistId)
                {
                    var artistEntity = ctx.Artists.Find(artistId);
                    artistEntity.SongsByArtist.Remove(entity);

                    var newArtistEntity = ctx.Artists.Find(entity.ArtistId);
                    newArtistEntity.SongsByArtist.Add(entity);
                }
                if (genreId != entity.GenreId)
                {
                    var genreEntity = ctx.Genres.Find(genreId);
                    genreEntity.SongsInGenre.Remove(entity);

                    var newGenreEntity = ctx.Genres.Find(entity.GenreId);
                    newGenreEntity.SongsInGenre.Add(entity);
                }
                //if (albumId != entity.AlbumId)
                //{
                //    var albumEntity = ctx.Albums.Find(albumId);
                //    albumEntity.SongsInAlbum.Remove(entity);

                //    var newAlbumEntity = ctx.Albums.Find(entity.AlbumId);
                //    newAlbumEntity.SongsInAlbum.Add(entity);
                //}

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSong(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Single(e => e.SongId == songId);

                ctx.Songs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
