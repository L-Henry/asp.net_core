using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWatcher.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Data
{
    public class MovieWatcherContext : IdentityDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<RatingReview> RatingReview { get; set; }
        public DbSet<GezienStatus> GezienStatus { get; set; }
        public DbSet<UserMovieGezienStatus> UserMovieGezienStatus { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }


        public MovieWatcherContext(DbContextOptions<MovieWatcherContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatingReview>().HasKey(rr => new { rr.MovieId, rr.UserId });
            modelBuilder.Entity<UserMovieGezienStatus>().HasKey(umgs => new { umgs.MovieId, umgs.UserId });
            modelBuilder.Entity<MovieGenres>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<Movie> GetMovies() {
            return Movies
                .Include(x=>x.Genres).ThenInclude(x=>x.Genre)
                .Include(x => x.RatingReviews)
                .Include(x => x.UserMovieGezienStatussen).ThenInclude(x => x.GezienStatus);
        }

        public Movie GetMovie(int id) {
            return Movies
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Include(m=>m.RatingReviews).ThenInclude(u=>u.User)
                .Include(m=>m.UserMovieGezienStatussen).ThenInclude(m=>m.GezienStatus)
                .SingleOrDefault(m => m.Id == id);
        }

        public Movie Insert(Movie movie) {
            Movies.Add(movie);
            this.SaveChanges();
            return movie;
        }

        public void AssignGenres(List<string> genres, int id) {
            MovieGenres.RemoveRange(MovieGenres.Where(m => m.MovieId == id));
            foreach (var genre in genres)
            {
                    MovieGenres.Add(new MovieGenres { GenreId = Genres.SingleOrDefault(g => g.Naam == genre).Id, MovieId = Movies.SingleOrDefault(m => m.Id == id).Id });
            }
            this.SaveChanges();
        }

        public void AssignUserMovieGezienStatus(int movieId, string userId, int gezienStatus)
        {
            if (UserMovieGezienStatus.Count() != 0 && UserMovieGezienStatus.Any(x => x.MovieId == movieId && x.UserId == userId))
            {
                UserMovieGezienStatus.SingleOrDefault(x => x.MovieId == movieId && x.UserId == userId).GezienStatus =
                GezienStatus.SingleOrDefault(s => s.Id == gezienStatus);
            }
            else
            {
                UserMovieGezienStatus.Add(new UserMovieGezienStatus { MovieId = movieId, UserId = userId, GezienStatus = GezienStatus.SingleOrDefault(s => s.Id == gezienStatus) });
            }
            this.SaveChanges();
        }

        public void AssignRatingReview(int movieId, string userId, int eigenRating, string eigenReview = null) {
            if (RatingReview.Any(x=>x.MovieId == movieId && x.UserId == userId))
            {
                RatingReview.SingleOrDefault(x => x.MovieId == movieId && x.UserId == userId).Rating = eigenRating;
                RatingReview.SingleOrDefault(x => x.MovieId == movieId && x.UserId == userId).Review = eigenReview;
            }
            else
            {
                RatingReview.Add(new RatingReview { MovieId = movieId, UserId = userId, Rating = eigenRating, Review = eigenReview });
            }
            this.SaveChanges();
        }
    }
}

 
