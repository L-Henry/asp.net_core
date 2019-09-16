using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class RatingReviewViewModel
    {
        public int Id { get; set; }

        public int Rating { get; set; }
        public List<SelectListItem> RatingSelectList { get; set; }

        [DataType(DataType.MultilineText)]
        public string Review { get; set; }
        public string UserName { get; set; }
    }
}
