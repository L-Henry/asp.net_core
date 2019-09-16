using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWatcher.Data;
using MovieWatcher.Domain;
using MovieWatcher.Models;

namespace MovieWatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieWatcherContext _context;

        public HomeController(MovieWatcherContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<MoviesListViewModel> movies = new List<MoviesListViewModel>();
            IEnumerable<Movie> moviesFromDatabase = _context.GetMovies();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var movie in moviesFromDatabase)
            {
                int ratingAvg = movie.RatingReviews.Where(rat => rat.MovieId == movie.Id && rat.Rating > -1).Count() != 0 ?
                    movie.RatingReviews.Where(rat => rat.MovieId == movie.Id && rat.Rating > -1).Select(r => r.Rating).Sum() / movie.RatingReviews.Where(rat => rat.MovieId == movie.Id && rat.Rating > -1).Count() : -1;

                int aantalGezien = movie.UserMovieGezienStatussen.Any(s=>s.GezienStatus != null) ?
                    movie.UserMovieGezienStatussen.Where(s => s.MovieId == movie.Id && s.GezienStatus != null && s.GezienStatus.Id == 2).Count() : 0;

                int heeftGezien = userId != null && movie.UserMovieGezienStatussen.Any(s => s.UserId == userId && s.GezienStatus != null) ?
                    movie.UserMovieGezienStatussen.SingleOrDefault(s => s.MovieId == movie.Id && s.UserId == userId).GezienStatus.Id : 1;

                List<SelectListItem> heeftGezienSelectList = new List<SelectListItem>();
                foreach (var gezienStatus in _context.GezienStatus)
                {
                    heeftGezienSelectList.Add(new SelectListItem { Value = gezienStatus.Id.ToString(), Text = gezienStatus.Naam });
                }

                movies.Add(new MoviesListViewModel()
                {
                    Id = movie.Id,
                    Titel = movie.Titel,
                    Genres = movie.Genres.Where(g => g.MovieId == movie.Id).Select(g => g.Genre.Naam).ToList(),
                    Rating = ratingAvg,
                    AantalGezien = aantalGezien,
                    HeeftGezienSelectList = heeftGezienSelectList,
                    HeeftGezien = heeftGezien,
                });
            }

            MoviesFilterListViewModel vm = new MoviesFilterListViewModel
            {
                Movies = movies
            };

            return View(vm);
        }

        //[HttpGet]
        //public IActionResult Index(int genre, string titel, int rating, int gezienStatus)
        //{
        //    List<MoviesListViewModel> movies = new List<MoviesListViewModel>();
        //    IEnumerable<Movie> moviesFromDatabase = _context.GetMovies();

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    foreach (var movie in moviesFromDatabase)
        //    {
        //        int ratingAvg = movie.RatingReviews.Where(rat => rat.MovieId == movie.Id).Select(r => r.Rating) != null ?
        //            movie.RatingReviews.Where(rat => rat.MovieId == movie.Id).Select(r => r.Rating).Sum() / movie.RatingReviews.Where(rat => rat.MovieId == movie.Id).Count() :
        //            0;
        //        int aantalGezien = movie.UserMovieGezienStatussen.Where(s => s.MovieId == movie.Id).Select(s => s.GezienStatus.Id == 2).Count();
        //        string heeftGezien = movie.UserMovieGezienStatussen.SingleOrDefault(s => s.MovieId == movie.Id && s.UserId == userId).GezienStatus.Naam;


        //        movies.Add(new MoviesListViewModel()
        //        {
        //            Id = movie.Id,
        //            Titel = movie.Titel,
        //            Genres = movie.Genre,
        //            Rating = ratingAvg,
        //            AantalGezien = aantalGezien,
        //            HeeftGezien = heeftGezien
        //        });
        //    }

        //    MoviesFilterListViewModel vm = new MoviesFilterListViewModel
        //    {
        //        Movies = movies
        //    };

        //    return View(vm);
        //}

        public IActionResult Create()
        {
            List<GenreListViewModel> list = new List<GenreListViewModel>();
            foreach (var item in _context.Genres)
            {
                list.Add(new GenreListViewModel() { Naam = item.Naam });
            };
            MovieCreateViewModel model = new MovieCreateViewModel()
            {
                GenreList = list
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View();
            }

            Movie movie = new Movie()
            {
                Titel = model.Titel,
                Speeltijd = model.Speeltijd
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.Foto.CopyToAsync(memoryStream);
                movie.Foto = memoryStream.ToArray();
            }

            List<string> genreListItems = new List<string>();
            foreach (var item in model.GenreList)
            {
                if (item.Checked)
                {
                    genreListItems.Add(item.Naam);
                }
            }

            _context.Insert(movie);
            _context.AssignGenres(genreListItems, movie.Id);

            return RedirectToAction("MovieDetail", new { id = movie.Id });
        }

        public IActionResult MovieDetail(int id)
        {
            Movie movieFromDb = _context.GetMovie(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int ratingAvg = movieFromDb.RatingReviews.Where(rat => rat.MovieId == movieFromDb.Id && rat.Rating > -1).Count() != 0 ?
                    movieFromDb.RatingReviews.Where(rat => rat.MovieId == movieFromDb.Id && rat.Rating > -1).Select(r => r.Rating).Sum() 
                    / movieFromDb.RatingReviews.Where(rat => rat.MovieId == movieFromDb.Id && rat.Rating > -1).Count() : -1;

            int aantalGezien = movieFromDb.UserMovieGezienStatussen.Any(s => s.GezienStatus != null) ?
                    movieFromDb.UserMovieGezienStatussen.Where(s => s.MovieId == movieFromDb.Id && s.GezienStatus != null && s.GezienStatus.Id == 2).Count() : 0;

            int aantalWilZien = movieFromDb.UserMovieGezienStatussen.Any(s => s.GezienStatus != null) ?
                    movieFromDb.UserMovieGezienStatussen.Where(s => s.MovieId == movieFromDb.Id && s.GezienStatus != null && s.GezienStatus.Id == 3).Count() : 0;


            int heeftGezien = userId != null && movieFromDb.UserMovieGezienStatussen.Any(s => s.UserId == userId && s.GezienStatus != null) ?
                    movieFromDb.UserMovieGezienStatussen.SingleOrDefault(s => s.MovieId == movieFromDb.Id && s.UserId == userId).GezienStatus.Id : 1;

            int eigenRating = userId != null && movieFromDb.RatingReviews.Any(rr=>rr.UserId == userId) /*movieFromDb.RatingReviews.Count != 0*/ ?
                movieFromDb.RatingReviews.SingleOrDefault(r => r.MovieId == movieFromDb.Id && r.UserId == userId).Rating : -1;

            List<SelectListItem> heeftGezienSelectList = new List<SelectListItem>();
            foreach (var gezienStatus in _context.GezienStatus)
            {
                heeftGezienSelectList.Add(new SelectListItem { Value = gezienStatus.Id.ToString(), Text = gezienStatus.Naam });
            }
            List<SelectListItem> eigenRatingSelectList = new List<SelectListItem>();
            eigenRatingSelectList.Add(new SelectListItem { Value = "-1", Text = "Geef rating" });
            for (var i=0; i <= 10; i++ )
            {
                eigenRatingSelectList.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            List<RatingReviewViewModel> ratingReviewList = new List<RatingReviewViewModel>();
            foreach (var rr in movieFromDb.RatingReviews.Where(r => r.MovieId == id).ToList())
            {
                ratingReviewList.Add(new RatingReviewViewModel {
                    Id = rr.MovieId,
                    Rating = rr.Rating,
                    Review = rr.Review,
                    UserName = rr.User.UserName
                });
            }
            


            MovieDetailViewModel movieVM = new MovieDetailViewModel()
            {
                Id = movieFromDb.Id,
                Titel = movieFromDb.Titel,
                //FotoFile = movieFromDb.Foto,
                Genres = movieFromDb.Genres.Where(x => x.MovieId == movieFromDb.Id).Select(x => x.Genre.Naam).ToList(),
                Rating = ratingAvg,
                EigenRating = eigenRating,
                EigenRatingSelectList = eigenRatingSelectList,
                AantalGezien = aantalGezien,
                AantalWilZien = aantalWilZien,
                HeeftGezienSelectList = heeftGezienSelectList,
                HeeftGezien = heeftGezien,
                RatingReview = ratingReviewList
            };
            return View(movieVM);
        }

        [HttpPost]
        public IActionResult MovieDetail(MovieDetailViewModel model) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.AssignUserMovieGezienStatus(model.Id, userId, model.HeeftGezien);
            _context.AssignRatingReview(model.Id, userId, model.EigenRating);
            return RedirectToAction("MovieDetail");
        }

        public IActionResult RatingReview(int movieId) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Movie movieFromDb = _context.GetMovie(movieId);

            int eigenRating = userId != null && movieFromDb.RatingReviews.Count != 0 ?
                movieFromDb.RatingReviews.SingleOrDefault(r => r.MovieId == movieFromDb.Id && r.UserId == userId).Rating : -1;
            string eigenReview = userId != null && movieFromDb.RatingReviews.Count != 0 ?
                movieFromDb.RatingReviews.SingleOrDefault(r => r.MovieId == movieFromDb.Id && r.UserId == userId).Review : null;

            List<SelectListItem> ratingSelectList = new List<SelectListItem>();
            ratingSelectList.Add(new SelectListItem { Value = "-1", Text = "Geef rating" });
            for (var i = 0; i <= 10; i++)
            {
                ratingSelectList.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            RatingReviewViewModel vm = new RatingReviewViewModel
            {
                Id = movieFromDb.Id,
                Rating = eigenRating,
                Review = eigenReview,
                RatingSelectList = ratingSelectList,
                UserName = movieFromDb.RatingReviews.SingleOrDefault(u=>u.UserId == userId).User.UserName
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult RatingReview(RatingReviewViewModel model, int id) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.AssignRatingReview(id, userId, model.Rating, model.Review);
            return RedirectToAction("MovieDetail", new { id = model.Id });
        }



        public IActionResult Beheer()
        {
            return View();
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}