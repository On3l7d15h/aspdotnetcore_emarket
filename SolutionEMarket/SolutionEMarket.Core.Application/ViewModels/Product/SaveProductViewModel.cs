using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.ViewModels.Product
{
    public class SaveProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]

        public string Description { get; set; }

        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "This field is required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string Created { get; set; }

        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "This field is required")]
        public int CategoryId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile[] File { get; set; }
    }
}
