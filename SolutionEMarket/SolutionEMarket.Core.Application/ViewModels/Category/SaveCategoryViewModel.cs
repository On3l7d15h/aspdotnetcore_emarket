using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.ViewModels.Category
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Description { get; set; }
    }
}
