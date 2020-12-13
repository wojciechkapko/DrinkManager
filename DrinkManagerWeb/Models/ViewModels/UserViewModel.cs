using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long and must contain at least one letter and one digit.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<SelectListItem> ApplicationRoles { get; set; }  

        [Display(Name = "Role")]  
        public string ApplicationRoleId { get; set; } 
    }
}
