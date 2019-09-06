using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public static class InitializeStatusAndTags
    {

        public static void Initialize(ProjectContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Status.Any() && context.Tags.Any())
            {
                return;   // DB has been seeded
            }

            var status = new Status[]
            {
            new Status{ Naam = "Afgewerkt"},
            new Status{ Naam = "In progress" },
            new Status{ Naam = "To do" }
            };
            foreach (var item in status)
            {
                context.Status.Add(item);
            }

            var tags = new Tag[] {
                new Tag{ Naam = "UWP"},
                new Tag{ Naam = "html"},
                new Tag{ Naam = "css"},
                new Tag{ Naam = "winforms"},
                new Tag{ Naam = "js"},
                new Tag{ Naam = "EF"},
                new Tag{ Naam = "asp.net"},
                new Tag{ Naam = "c#"}
            };
            foreach (var item in tags)
            {
                context.Tags.Add(item);
            }

            context.SaveChanges();


        }
    }
}
