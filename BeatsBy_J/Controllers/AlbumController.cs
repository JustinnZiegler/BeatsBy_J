using BeatsBy_J_Data;
using BeatsBy_J_Models.Album;
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
    public class AlbumController : Controller
    {
        private ApplicationDbContext _service = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);
            var model = service.GetAlbums();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var _service = CreateAlbumService();

            if (_service.CreateAlbum(model))
            {
                TempData["SaveResult"] = "Your Album was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your Album could not be created.");

            return View(model);
        }

        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            var svc = CreateAlbumService();
            var model = svc.GetAlbumById(id);

            return View(model);
        }

        public ActionResult GetByName(string name)
        {
            var svc = CreateAlbumService();
            var model = svc.GetAlbumByName(name);

            return View(model);
        }

        [ActionName("Update")]
        public ActionResult Update(int id)
        {
            var _service = CreateAlbumService();
            var detail = _service.GetAlbumById(id);
            var model =
                new AlbumUpdate
                {
                    AlbumId = detail.AlbumId,
                    AlbumName = detail.AlbumName,
                    AlbumReleaseDate = detail.AlbumReleaseDate,
                };
            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, AlbumUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AlbumId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var _service = CreateAlbumService();

            if (_service.UpdateAlbum(model))
            {
                TempData["SaveResult"] = "Your Album was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Album could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAlbumService();
            var model = svc.GetAlbumById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var _service = CreateAlbumService();

            _service.DeleteAlbum(id);

            TempData["SaveResult"] = "Your Album was deleted.";

            return RedirectToAction("Index");
        }

        private AlbumService CreateAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _service = new AlbumService(userId);
            return _service;
        }
    }
}