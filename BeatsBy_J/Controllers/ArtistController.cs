using BeatsBy_J_Data;
using BeatsBy_J_Models.Artist;
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
    public class ArtistController : Controller
    {
        private ApplicationDbContext _service = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ArtistService(userId);
            var model = service.GetAllArtists();
            List<Artist> artistList = _service.Artists.ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var _service = CreateArtistService();

            if (_service.CreateArtist(model))
            {
                TempData["SaveResult"] = "Your Artist was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your Artist could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            return View(model);
        }

        public ActionResult GetByName(string name)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistByName(name);

            return View(model);
        }

        [ActionName("Update")]
        public ActionResult Update(int id)
        {
            var _service = CreateArtistService();
            var detail = _service.GetArtistById(id);
            var model =
                new ArtistUpdate
                {
                    ArtistId = detail.ArtistId,
                    ArtistName = detail.ArtistName,
                    Birthdate = detail.Birthdate,
                };
            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, ArtistUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ArtistId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var _service = CreateArtistService();

            if (_service.UpdateArtist(model))
            {
                TempData["SaveResult"] = "Your Artist was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Artist could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var _service = CreateArtistService();

            bool deleting = _service.DeleteArtist(id);
            if (deleting)
            {
                TempData["SaveResult"] = "Your Artist was deleted.";
                return RedirectToAction(nameof(Index));
            }
            TempData["NotDeleted"] = "Please Delete Songs/Albums for this Artist First.";
            return RedirectToAction(nameof(Index));

        }

        private ArtistService CreateArtistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _service = new ArtistService(userId);
            return _service;
        }
    }
}