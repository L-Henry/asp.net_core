using MovieWatcher.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Data
{
    public class DbInitializer
    {

        public static void Initialize(MovieWatcherContext context)
        {
            context.Database.EnsureCreated();

            if (context.GezienStatus.Any())
            {
                ;
            }
            else
            {
                var status = new GezienStatus[]
                {
                    new GezienStatus{ Naam = "Ongekend"},
                    new GezienStatus{ Naam = "Gezien"},
                    new GezienStatus{ Naam = "Wil zien" }
                };
                foreach (var item in status)
                {
                    context.GezienStatus.Add(item);
                }
            }

            context.SaveChanges();

            if (context.Movies.Any())
            {
                ;
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    context.Movies.Add(new Movie { Titel = "Titel" + i, Speeltijd = i * 10, Foto = null });
                }
            }

            context.SaveChanges();

            if (context.Genres.Any())
            {
                return;
            }
            else
            {
                var genres = new Genres[] {
                    new Genres{ Naam = "Sci-fi"},
                    new Genres{ Naam = "Horror"},
                    new Genres{ Naam = "Drama"},
                    new Genres{ Naam = "Romantic"},
                    new Genres{ Naam = "Fantasy"},
                    new Genres{ Naam = "Action"},
                };
                foreach (var item in genres)
                {
                    context.Genres.Add(item);
                }
            }

            context.SaveChanges();

        }
    }
}
