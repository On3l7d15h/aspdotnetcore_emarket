using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.ViewModels.Product
{
    public class SaveUrlsProductViewModel
    {
        [DataType(DataType.Upload)]
        public IFormFile File1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File4 { get; set; }
    }
}
