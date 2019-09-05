using MovieWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Database
{
    public static class MovieEFInitialize
    {

        public static void Initialize(MovieContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }

            var movie = new Movie[]
            {
            new Movie{Title = "Matrix", Description="Cool", ReleaseDate= DateTime.Parse("2002-09-01"), Genre="Sci-fi", Rating=10},
            new Movie{Title = "Intersterllar", Description="Cool", ReleaseDate= DateTime.Parse("2002-09-01"), Genre="Sci-fi", Rating=10},
            new Movie{Title = "GWH", Description="Cool", ReleaseDate= DateTime.Parse("2002-09-01"), Genre="Sci-fi", Rating=10}
            };
            foreach (var mov in movie)
            {
                context.Movies.Add(mov);
            }
            context.SaveChanges();

     
        }
    }
}



