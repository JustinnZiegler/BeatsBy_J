using BeatsBy_J_Data;
using BeatsBy_J_Models.Song;
using BeatsBy_J_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeatsBy_J.Controllers
{
    [Authorize]
    public class SongController : Controller
    {
        private ApplicationDbContext _service = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);
            var model = service.GetSongs();
            List<Song> songList = _service.Songs.ToList();
            List<Song> orderedList = songList.OrderBy(alpha => alpha.Title).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            var service = CreateSongService();

            ViewData["Genres"] = service.GetGenres();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var _service = CreateSongService();

            if (_service.CreateSong(model))
            {
                TempData["SaveResult"] = "Your song was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your song could not be created.");

            return View(model);
        }

        public ActionResult GetByDate(DateTime startDate, DateTime endDate)
        {
            if (startDate < endDate)
            {
                var _service = CreateSongService();
                var songs = _service.GetSongsByDate(startDate, endDate);
                var songsOrdered = songs.OrderBy(date => date.Date);
                return View(songsOrdered);
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }

        public ActionResult GetByName(string name)
        {
            var svc = CreateSongService();
            var model = svc.GetSongByName(name);

            return View(model);
        }

        [ActionName("Update")]
        public ActionResult Update(int id)
        {
            var _service = CreateSongService();
            var detail = _service.GetSongById(id);
            ViewData["Genres"] = _service.GetGenres();
            var model =
                new SongUpdate
                {
                    SongId = detail.SongId,
                    Title = detail.Title,
                    ArtistId = detail.ArtistId,
                    GenreId = detail.GenreId,
                    AlbumId = detail.AlbumId,
                    Date = detail.Date
                };
            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, SongUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SongId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var _service = CreateSongService();
            if (_service.UpdateSong(model))
            {
                TempData["SaveResult"] = "Your song was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your song could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var _service = CreateSongService();

            _service.DeleteSong(id);

            TempData["SaveResult"] = "Your song was deleted.";

            return RedirectToAction("Index");
        }

        private SongService CreateSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _service = new SongService(userId);
            return _service;
        }
    }
}