using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Publisher
    {

        [Key]
        public int PublisherId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Location { get; set; }

        public List<Book> Books { get; set; }
    }
}
