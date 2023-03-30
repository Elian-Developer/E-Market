using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverstisements
{
    public class AdsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string AdsUserName { get; set; }
        public DateTime? AdsCreated { get; set; }
        public string? AdsUserEmail { get; set; }
        public string? AdsUserPhone { get; set; }
        public DateTime? AdsLastModified { get; set; }


        //Foreign Key
        public int CategoryId { get; set; }
        public int UserId { get; set; }

    }
}
