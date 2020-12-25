using System.ComponentModel.DataAnnotations;

namespace DrinkManager.API.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Use only letters (lowercase and/or uppercase).")]
        public string Name { get; set; }
    }
}
