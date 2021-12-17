using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.ViewModels
{
    public  class BookVM
    {
        public Book Book { get; set; }

        public IEnumerable<SelectListItem> PublisherList { get; set; }

        public IEnumerable<SelectListItem> CategoriesList { get; set; }

    }
}
