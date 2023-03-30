using E_Market.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverstisements
{
    public class FilterAdsViewModel
    {
        public string? AdsName { get; set; }
        public int? CategoryId { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
    }
}
