﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string title { get; set; }
        [Required]
        [MinLength(3)]
        public string description { get; set; }
        public string image_path { get; set; }
        [Required]
        public Auther auther { get; set; }
    }
}
