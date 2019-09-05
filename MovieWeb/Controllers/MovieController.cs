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
    public class MovieController : Controller
    {
        private readonly IMovieDatabase _movieDatabase;
        public MovieController(IMovieDatabase movieDatabase)
        {
            _movieDatabase = movieDatabase;
        }


        public IActionResult Details(int id) {
            Movie movieFromDb = _movieDatabase.GetMovie(id);
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

        public IActionResult Index() {
            List<MovieListViewModel> movies = new List<MovieListViewModel>();
            IEnumerable<Movie> moviesFromDatabase = _movieDatabase.GetMovies();

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

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieCreateViewModel model) {
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

            movie = _movieDatabase.Insert(movie);

            return RedirectToAction("Details", new { id = movie.Id });
        }

        public IActionResult Delete(int id) {
            Movie movieFromDb = _movieDatabase.GetMovie(id);
            MovieDeleteViewModel model = new MovieDeleteViewModel()
            {
                Title = movieFromDb.Title,
                Id = movieFromDb.Id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id) {
            _movieDatabase.Delete(id);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id) {
            Movie movieFromDb = _movieDatabase.GetMovie(id);
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

        [HttpPost]
        public IActionResult Edit(int id, MovieEditViewModel model) {
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

            _movieDatabase.Update(id, movieToUpdate);

            return RedirectToAction("Details", new { Id = id });
        }
    }
}
