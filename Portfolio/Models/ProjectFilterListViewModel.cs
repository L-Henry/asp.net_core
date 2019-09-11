using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProjectFilterListViewModel
    {
        public IEnumerable<ProjectListViewModel> Projecten { get; set; }
        public int StatusId { get; set; }
        public string Tags { get; set; }
    }
}
