using BLL.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL
{
    public class UserActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public PerformedAction Action { get; set; }
        public string? DrinkId { get; set; }
        public string? SearchedPhrase { get; set; }
        public int? Score { get; set; }
        public DateTime Created { get; set; }
    }
}
