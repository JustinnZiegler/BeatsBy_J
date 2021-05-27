using BeatsBy_J_Data;
using BeatsBy_J_Models.Genre;
using BeatsBy_J_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BeatsBy_J.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private ApplicationDbContext _service = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GenreService(userId);
            var model = service.GetAllGenres();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenreCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var _service = CreateGenreService();

            if (_service.CreateGenre(model))
            {
                TempData["SaveResult"] = "Your Genre was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Your Genre could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateGenreService();
            var model = svc.GetAllSongsInGenre(id);

            return View(model);
        }

        public ActionResult GetByName(string name)
        {
            var svc = CreateGenreService();
            var model = svc.GetGenreByName(name);

            return View(model);
        }

        [ActionName("Update")]
        public ActionResult Update(int id)
        {
            var _service = CreateGenreService();
            var detail = _service.GetGenreById(id);
            var model =
                new GenreUpdate
                {
                    GenreId = detail.GenreId,
                    GenreName = detail.GenreName,
                };
            return View(model);
        }

        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, GenreUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.GenreId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var _service = CreateGenreService();

            if (_service.UpdateGenre(model))
            {
                TempData["SaveResult"] = "Your Genre was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Genre could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGenreService();
            var model = svc.GetGenreById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var _service = CreateGenreService();

            _service.DeleteGenre(id);

            TempData["SaveResult"] = "Your Genre was deleted.";

            return RedirectToAction("Index");
        }

        private GenreService CreateGenreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _service = new GenreService(userId);
            return _service;
        }

        #region Index method  

        public ActionResult DropDownIndex()
        {
            GenreDetail model = new GenreDetail();

            model.SelectedGenreId = 0;

            ViewBag.GenreList = new SelectList(_service.Genres, "GenreId", "GenreName");

            return this.View(model);
        }

        #endregion
        #region Helpers  
        #region Load Data  

        private List<Genre> LoadData()
        {
            List<Genre> lst = new List<Genre>();

            try
            {
                string line = string.Empty;
                string srcFilePath = "content/files/genre_list.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var fullPath = Path.Combine(rootPath, srcFilePath);
                string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                while ((line = sr.ReadLine()) != null)
                {
                    Genre infoObj = new Genre();
                    string[] info = line.Split(',');

                    infoObj.GenreId = Convert.ToInt32(info[0].ToString());
                    infoObj.GenreName = info[1].ToString();

                    lst.Add(infoObj);
                }

                sr.Dispose();
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return lst;
        }

        #endregion
        #region Get roles method.  

        private IEnumerable<SelectListItem> GetGenreList()
        {
            SelectList lstobj = null;

            try
            {
                var list = this.LoadData()
                                  .Select(p =>
                                            new SelectListItem
                                            {
                                                Value = p.GenreId.ToString(),
                                                Text = p.GenreName
                                            });
                lstobj = new SelectList(list, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstobj;
        }
        #endregion
        #endregion
    }
}