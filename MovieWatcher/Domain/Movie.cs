using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Domain
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        public ICollection<MovieGenres> Genres { get; set; }

        public byte[] Foto { get; set; }
        public int Speeltijd { get; set; }

        public ICollection<RatingReview> RatingReviews { get; set; }
        public ICollection<UserMovieGezienStatus> UserMovieGezienStatussen { get; set; }

    }
}
