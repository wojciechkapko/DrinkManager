using BLL.Enums;

namespace BLL
{
    public class UserActivityDto
    {
        public string Username { get; set; }
        public PerformedAction Action { get; set; }
    }
}
