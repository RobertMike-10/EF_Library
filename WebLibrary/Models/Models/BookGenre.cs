using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class BookGenre
    {
     
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Book Book { get; set; }
        public Genre Genre { get; set; }   

    }
}
