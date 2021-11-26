﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
   public class BookDetail
    {
        [Key]
        public int BookDetailId { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }

        public int NumberOfPages { get; set; }

        public Decimal Weight { get; set; }

        public Book Book { get; set; }

    }
}
