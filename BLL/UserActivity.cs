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
        public string? Username { get; set; }
        public PerformedAction Action { get; set; }
        
        //Used to generate information which drinks are visited, added to favourites or reviewed the most
        public string? DrinkId { get; set; }
        
        //Used to gather information which Ingredient or phrase contained by drink name is searched the most
        public string? SearchedPhrase { get; set; }
        
        //Used to generate information which drinks are liked or disliked the most
        public int? Score { get; set; }
        public DateTime Created { get; set; }
    }
}
