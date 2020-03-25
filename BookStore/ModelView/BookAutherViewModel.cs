using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ModelView
{
    public class BookAutherViewModel
    {
        public int book_id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string book_name { get; set; }
        [Required]
        [MinLength(3)]
        public string book_description { get; set; }
        [Required]
        public int author_id { get; set; }
        public List<Auther> authors { get; set; }
        public IFormFile image { get; set; }
        public string image_path { get; set; }
    }
}
