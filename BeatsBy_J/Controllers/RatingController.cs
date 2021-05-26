using BeatsBy_J_Data;
using BeatsBy_J_Models.Rating;
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
    public class RatingController : Controller
    {
        private ApplicationDbContext _service = new ApplicationDbContext();

        // GET: Rating
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RatingService(userId);
            var model = service.GetRatings();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RatingCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var _service = CreateRatingService();

            if (_service.CreateRating(model))
            {
                TempData["SaveResult"] = "Your Rating was submitted.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your Rating could not be submitted.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRatingService();
            var model = svc.GetRatingById(id);

            return View(model);
        }

        [ActionName("Update")]
        public ActionResult Update(int id)
        {
            var _service = CreateRatingService();
            var detail = _service.GetRatingById(id);
            var model =
                new RatingUpdate
                {
                    RatingId = detail.RatingId,
                    EnjoymentScore = detail.EnjoymentScore,
                    SongLengthScore = detail.SongLengthScore,
                    ArtistStyleScore = detail.ArtistStyleScore,
                    Description = detail.Description
                };
            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, RatingUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RatingId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var _service = CreateRatingService();

            if (_service.UpdateRating(model))
            {
                TempData["SaveResult"] = "Your Rating was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Rating could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRatingService();
            var model = svc.GetRatingById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var _service = CreateRatingService();

            _service.DeleteRating(id);

            TempData["SaveResult"] = "Your Rating was deleted.";

            return RedirectToAction("Index");
        }

        private RatingService CreateRatingService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var _service = new RatingService(userId);
            return _service;
        }
    }
}