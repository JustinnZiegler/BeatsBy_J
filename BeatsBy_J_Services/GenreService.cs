using BeatsBy_J_Data;
using BeatsBy_J_Models.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BeatsBy_J_Services
{
    public class GenreService
    {
        private readonly Guid _userId;
        public GenreService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateGenre(GenreCreate model)
        {
            var entity = new Genre()
            {
                GenreName = model.GenreName
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Genres.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GenreList> GetAllGenres()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Genres.Select(e => new GenreList
                {
                    GenreId = e.GenreId,
                    GenreName = e.GenreName
                });
                return query.ToArray();
            }
        }

        public GenreDetail GetAllSongsInGenre(int genreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Include(e => e.SongsInGenre)
                    .Single(e => e.GenreId == genreId);

                var titlesOfSongsInGenre = new List<string>();
                foreach (var song in entity.SongsInGenre)
                {
                    titlesOfSongsInGenre.Add(song.Title);
                }

                return new GenreDetail()
                {
                    GenreId = entity.GenreId,
                    GenreName = entity.GenreName,
                    SongTitlesInGenre = titlesOfSongsInGenre
                };
            }
        }

        public GenreDetail GetGenreById(int genreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Include(e => e.SongsInGenre).Single(e => genreId == e.GenreId);

                var namesOfSongs = new List<string>();

                foreach (var song in entity.SongsInGenre)
                {
                    namesOfSongs.Add(song.Title);
                }

                return new GenreDetail()
                {
                    GenreId = entity.GenreId,
                    GenreName = entity.GenreName,
                    SongTitlesInGenre = namesOfSongs
                };
            }
        }

        public GenreDetail GetGenreByName(string genreName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Include(e => e.SongsInGenre).Single(e => genreName == e.GenreName);

                var titlesOfSongsInGenre = new List<string>();

                foreach (var song in entity.SongsInGenre)
                {
                    titlesOfSongsInGenre.Add(song.Title);
                }

                return new GenreDetail()
                {
                    GenreId = entity.GenreId,
                    GenreName = entity.GenreName,
                    SongTitlesInGenre = titlesOfSongsInGenre
                };
            }
        }

        public bool UpdateGenre(GenreUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Single(e => e.GenreId == model.GenreId);
                entity.GenreName = model.GenreName;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteGenre(int genreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Genres.Single(e => e.GenreId == genreId);
                ctx.Genres.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}