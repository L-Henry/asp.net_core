
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWeb.Database;
using MovieWeb.Domain;
using MovieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Controllers
{
    public class MovieEFController : Controller
    {
        private readonly MovieContext _context;
        public MovieEFController(MovieContext context)
        {
            _context = context;
        }


        public IActionResult Details(int id)
        {
            Movie movieFromDb = _context.GetMovie(id);
            MovieDetailsViewModel model = new MovieDetailsViewModel()
            {
                Title = movieFromDb.Title,
                Description = movieFromDb.Description,
                ReleaseDate = movieFromDb.ReleaseDate,
                Genre = movieFromDb.Genre,
                Rating = movieFromDb.Rating
            };
            return View(model);
        }

        public IActionResult Index()
        {
            List<MovieListViewModel> movies = new List<MovieListViewModel>();
            IEnumerable<Movie> moviesFromDatabase = _context.GetMovies();

            foreach (var movie in moviesFromDatabase)
            {
                movies.Add(new MovieListViewModel()
                {
                    Id = movie.Id,
                    Title = movie.Title
                });
            }

            return View(movies);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieCreateViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }

            var movie = new Movie()
            {
                Title = model.Title,
                Description = model.Description,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate,
                Rating = model.Rating
            };

            movie = _context.Insert(movie);

            return RedirectToAction("Details", new { id = movie.Id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            Movie movieFromDb = _context.GetMovie(id);
            MovieDeleteViewModel model = new MovieDeleteViewModel()
            {
                Title = movieFromDb.Title,
                Id = movieFromDb.Id
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _context.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            Movie movieFromDb = _context.GetMovie(id);
            MovieEditViewModel vm = new MovieEditViewModel()
            {
                Title = movieFromDb.Title,
                Description = movieFromDb.Description,
                Rating = movieFromDb.Rating,
                Genre = movieFromDb.Genre,
                ReleaseDate = movieFromDb.ReleaseDate
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MovieEditViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }

            Movie movieToUpdate = new Movie()
            {
                Id = id,
                Title = model.Title,
                Description = model.Description,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate,
                Rating = model.Rating
            };

            _context.Update(id, movieToUpdate);

            return RedirectToAction("Details", new { Id = id });
        }
    }
}
