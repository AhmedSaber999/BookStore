using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Auther
    {
        public int id { get ; set ; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string name { get ; set ; }
    }
}
