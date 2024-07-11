using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using CinemaSuite.Models;
using CinemaSuite.DataAccess.Data;
using CinemaSuite.DataAccess.Repository.IRepository;
using CinemaSuite.Models.ViewModels;
using CinemaSuite.DataAccess.Repository;
using CinemaSuite.Utility;
using Microsoft.AspNetCore.Authorization;

namespace CinemaSuite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MovieController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Movie> objMovieList = _unitOfWork.Movie.GetAll(includeProperties: "Category").ToList();
            return View(objMovieList);
        }

        public IActionResult Upsert(int? id)
        {
            MovieVM movieVM = new()
            {
                CategoryList =
                 _unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                Movie = new Movie()
            };
            if (id == null || id == 0)
            {
                return View(movieVM);
            }
            else
            {
                movieVM.Movie = _unitOfWork.Movie.Get(u => u.Id == id);
                return View(movieVM);

            }
        }

        [HttpPost]
        public IActionResult Upsert(MovieVM movieVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string moviePath = Path.Combine(wwwRootPath, @"images\movie");

                    if (!string.IsNullOrEmpty(movieVM.Movie.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, movieVM.Movie.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(moviePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    movieVM.Movie.ImageUrl = @"\images\movie\" + fileName;
                }

                if (movieVM.Movie.Id == 0)
                {
                    _unitOfWork.Movie.Add(movieVM.Movie);

                    _unitOfWork.Save();
                    TempData["success"] = "Movie Created successfully";

                }
                else
                {
                    _unitOfWork.Movie.Update(movieVM.Movie);

                    _unitOfWork.Save();
                    TempData["success"] = "Movie Updated successfully";
                }

                return RedirectToAction("Index");
            }
            else
            {
                movieVM.CategoryList = _unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });
                return View(movieVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Movie> objMovieList = _unitOfWork.Movie.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objMovieList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var movieToBeDeleted = _unitOfWork.Movie.Get(u => u.Id == id);
            if (movieToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            var oldImagePath =
                Path.Combine(_webHostEnvironment.WebRootPath,
                movieToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Movie.Remove(movieToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
