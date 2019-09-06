using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Domain
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public byte[] Image { get; set; }
        public Status Status { get; set; }
        public ICollection<TagProject> TagProjects { get; set; }
    }

}
