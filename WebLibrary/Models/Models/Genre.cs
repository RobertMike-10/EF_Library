using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Genre
    {

        [Key]
        public int GenreId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
