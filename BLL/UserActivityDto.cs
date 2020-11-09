using BLL.Enums;

namespace BLL
{
    public class UserActivityDto
    {
        public string Username { get; set; }
        public PerformedAction Action { get; set; }
        public string? DrinkId { get; set; }
        public string? SearchedPhrase { get; set; }
        public int? Score { get; set; }
    }
}
