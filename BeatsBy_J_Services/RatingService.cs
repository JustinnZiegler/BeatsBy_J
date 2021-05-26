using BeatsBy_J_Data;
using BeatsBy_J_Models.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsBy_J_Services
{
    public class RatingService
    {
        private readonly Guid _userId;
        public RatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRating(RatingCreate model)
        {
            var entity =
                new Rating()
                {
                    EnjoymentScore = model.EnjoymentScore,
                    SongLengthScore = model.SongLengthScore,
                    ArtistStyleScore = model.ArtistStyleScore,
                    Description = model.Description,
                    SongId = model.SongId,
                    UserId = _userId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ratings.Add(entity);

                var songEntity = ctx.Songs.Find(model.SongId);
                songEntity.RatingsForSong.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RatingList> GetRatings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Ratings.Select(e =>
                new RatingList()
                {
                    RatingId = e.RatingId,
                    EnjoymentScore = e.EnjoymentScore,
                    SongLengthScore = e.SongLengthScore,
                    ArtistStyleScore = e.ArtistStyleScore
                });

                return query.ToArray();
            }
        }

        public RatingDetail GetRatingById(int ratingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Find(ratingId);

                var listOfRatings = new List<double>();
                foreach (var rating in entity.RatingsForSong)
                {
                    listOfRatings.Add(rating.ScoreAverage);
                }

                return new RatingDetail()
                {
                    RatingId = entity.RatingId,
                    EnjoymentScore = entity.EnjoymentScore,
                    SongLengthScore = entity.SongLengthScore,
                    ArtistStyleScore = entity.ArtistStyleScore,
                    Description = entity.Description
                };
            }
        }

        public bool UpdateRating(RatingUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(e => e.RatingId == model.RatingId);

                entity.EnjoymentScore = model.EnjoymentScore;
                entity.SongLengthScore = model.SongLengthScore;
                entity.ArtistStyleScore = model.ArtistStyleScore;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int ratingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(e => e.RatingId == ratingId);

                ctx.Ratings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
