using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }
    }
}