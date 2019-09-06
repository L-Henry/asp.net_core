using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Domain
{
    public class TagProject
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
