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

        // GET: Genre
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GenreService(userId);
            var model = service.GetAllGenres();
            //List<Genre> genreList = _service.Genres.ToList();
            //List<Genre> orderedList = (List<Genre>)genreList.OrderBy(alpha => alpha.GenreName);

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

        //public ActionResult GetSongsByGenre(int id)
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
            // Initialization.  
            GenreDetail model = new GenreDetail();

            // Settings.  
            model.SelectedGenreId = 0;

            // Loading drop down lists.  
            //this.ViewBag.GenreList = this.GetGenreList();
            ViewBag.GenreList = new SelectList(_service.Genres, "GenreId", "GenreName");

            // Info.  
            return this.View(model);
        }

        #endregion

        #region Helpers  

        #region Load Data  

        private List<Genre> LoadData()
        {
            // Initialization.  
            List<Genre> lst = new List<Genre>();

            try
            {
                // Initialization.  
                string line = string.Empty;
                string srcFilePath = "content/files/genre_list.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var fullPath = Path.Combine(rootPath, srcFilePath);
                string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                // Read file.  
                while ((line = sr.ReadLine()) != null)
                {
                    // Initialization.  
                    Genre infoObj = new Genre();
                    string[] info = line.Split(',');

                    // Setting.  
                    infoObj.GenreId = Convert.ToInt32(info[0].ToString());
                    infoObj.GenreName = info[1].ToString();

                    // Adding.  
                    lst.Add(infoObj);
                }

                // Closing.  
                sr.Dispose();
                sr.Close();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
            }

            // info.  
            return lst;
        }

        #endregion

        #region Get roles method.  

        private IEnumerable<SelectListItem> GetGenreList()
        {
            // Initialization.  
            SelectList lstobj = null;

            try
            {
                // Loading.  
                var list = this.LoadData()
                                  .Select(p =>
                                            new SelectListItem
                                            {
                                                Value = p.GenreId.ToString(),
                                                Text = p.GenreName
                                            });

                // Setting.  
                lstobj = new SelectList(list, "Value", "Text");
            }
            catch (Exception ex)
            {
                // Info  
                throw ex;
            }

            // info.  
            return lstobj;
        }

        #endregion

        #endregion
    }
}