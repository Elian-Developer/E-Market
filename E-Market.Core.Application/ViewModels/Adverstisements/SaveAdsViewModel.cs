using E_Market.Core.Application.ViewModels.Category;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverstisements
{
    public class SaveAdsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You should set the Ads name.")]
        //[DataType(DataType.Text)]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "You should set the Ads Image.")]
        //[DataType(DataType.Upload)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "You should set the Ads Description.")]
        //[DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "You should set the Ads Price.")]
        //[DataType(DataType.Currency)]
        public int Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You should set the Ads category.")]
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You should set the Ads User.")]
        public int UserId { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
        public List<UserViewModel>? Users { get; set; }

        //[Required(ErrorMessage = "You should set the Ads images.")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public string? AdsUserName { get; set; }
        public string? AdsUserEmail { get; set; }
        public string? AdsUserPhone { get; set; }
        public DateTime? AdsCreated { get; set; }
    }
}
