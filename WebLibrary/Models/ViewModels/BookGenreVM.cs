using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class BookGenreVM
    {
        public BookGenre BookGenre { get; set; }
        public Book Book { get; set; }
        public IEnumerable<BookGenre> BookGenreList { get; set; }

        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
