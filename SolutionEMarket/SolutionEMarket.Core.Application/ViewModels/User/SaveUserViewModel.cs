using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //added
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Compare(nameof(Password), ErrorMessage = "The Password must be the same")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
