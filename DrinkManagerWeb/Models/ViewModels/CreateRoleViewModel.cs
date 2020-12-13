using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Use only letters (lowercase and/or uppercase).")]
        public string Name { get; set; }
    }
}
