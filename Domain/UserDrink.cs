namespace Domain
{
    public class UserDrink
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}