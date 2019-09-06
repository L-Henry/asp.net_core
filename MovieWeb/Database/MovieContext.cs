using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Domain
{
    public class MovieContext : IdentityDbContext
    {

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            
        }
      
        public DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().ToTable("Movies");
        }

        public Movie GetMovie(int id)
        {
            Movie movieToReturn = new Movie();

            movieToReturn = Movies.FirstOrDefault(x => x.Id == id);

            return movieToReturn;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return Movies;
        }

        public Movie Insert(Movie movie)
        {
            Movies.Add(movie);
            this.SaveChanges();

          
            return movie;
        }

        public void Delete(int id)
        {
            var movie = Movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                Movies.Remove(movie);
                this.SaveChanges();
            }
        }

        public void Update(int id, Movie updatedMovie)
        {
            var movie = Movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Title = updatedMovie.Title;
                movie.Description = updatedMovie.Description;
                movie.ReleaseDate = updatedMovie.ReleaseDate;
                movie.Rating = updatedMovie.Rating;
                movie.Genre = updatedMovie.Genre;
                this.SaveChanges();
            }
        }
    }

}

