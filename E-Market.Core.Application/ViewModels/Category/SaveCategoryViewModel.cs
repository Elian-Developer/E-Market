using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Category
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You should set the category name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should the category description.")]
        public string Description { get; set; }
        //public string UserQuantity { get; set; }
    }
}
