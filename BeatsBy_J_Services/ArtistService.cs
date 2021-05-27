﻿using BeatsBy_J_Data;
using BeatsBy_J_Models.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace BeatsBy_J_Services
{
    public class ArtistService
    {
        private readonly Guid _userId;
        public ArtistService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateArtist(ArtistCreate model)
        {
            var entity =
                new Artist()
                {
                    ArtistName = model.ArtistName,
                    Birthdate = model.Birthdate
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Artists.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ArtistList> GetAllArtists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Artists.Select(e => new ArtistList
                {
                    ArtistId = e.ArtistId,
                    ArtistName = e.ArtistName
                });

                return query.ToArray();
            }
        }

        public ArtistDetail GetArtistById(int artistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Artists.Include(e => e.Albums).Single(e => artistId == e.ArtistId);

                var namesOfSongs = new List<string>();

                return new ArtistDetail()
                {
                    ArtistId = entity.ArtistId,
                    ArtistName = entity.ArtistName,
                    Birthdate = entity.Birthdate,
                    NamesOfSongsByArtist = namesOfSongs
                };
            }
        }

        public ArtistDetail GetArtistByName(string artistName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Artists.Include(e => e.Albums).Single(e => artistName == e.ArtistName);

                var namesOfSongs = new List<string>();

                foreach (var song in entity.Albums)
                {
                    namesOfSongs.Add(song.Titles);
                }

                return new ArtistDetail()
                {
                    ArtistId = entity.ArtistId,
                    ArtistName = entity.ArtistName,
                    Birthdate = entity.Birthdate,
                    NamesOfSongsByArtist = namesOfSongs
                };
            }
        }

        public bool UpdateArtist(ArtistUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Artists.Single(e => e.ArtistId == model.ArtistId);

                entity.ArtistName = model.ArtistName;
                entity.Birthdate = model.Birthdate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteArtist(int artistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Artists.Single(e => e.ArtistId == artistId);
                    int initialCount = ctx.Artists.Count();
                    var testing = ctx.Artists.Find(artistId);
                    ctx.Artists.Remove(testing);
                    ctx.SaveChanges();
                    return true;
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    return false;
                }

            }
        }
    }
}