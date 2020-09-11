namespace BLL
{
    public class DrinkIngredient
    {
        public string DrinkId { get; set; }
        public Drink Drink { get; set; }
        public string IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}