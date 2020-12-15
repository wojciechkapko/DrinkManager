using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class UserEditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public List<SelectListItem> ApplicationRoles { get; set; }

        [Required]
        [RegularExpression("^(?!Please select).*", ErrorMessage = "The Role field is required.")]
        [Display(Name = "Role")]  
        public string ApplicationRoleId { get; set; }
    }
}
