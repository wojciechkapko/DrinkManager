using System;

namespace BLL
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public DateTime Created { get; set; }
    }
}
