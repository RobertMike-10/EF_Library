using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(250)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(250)]
        public string LastName { get; set; }

    }
}
