﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Domain
{
    public class GezienStatus
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
    }
}
