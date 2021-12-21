using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.StoreProcedureModels
{
    public class BookFull
    {

        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; }
        [Required]
        public Decimal Price { get; set; }

        public String NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
    }
}
