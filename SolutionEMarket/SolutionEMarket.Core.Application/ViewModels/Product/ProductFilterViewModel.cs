using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.ViewModels.Product
{
    public class ProductFilterViewModel
    {
        public string? ProductName { get; set; }
        public int[]? CategoryId { get; set; }
    }
}
