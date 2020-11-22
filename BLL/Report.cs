using BLL.ReportDataModels;
using System;

namespace BLL
{
    public class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created => DateTime.Now;
        public LoginData SuccessfulLoginsAmount { get; set; }
        public TheMostData MostFavouriteDrink { get; set; }
        public ScoreData HighestScoreDrink { get; set; }
        public ScoreData LowestScoreDrink { get; set; }
        public TheMostData MostVisitedDrink { get; set; }
        public TheMostData MostActiveUser { get; set; }
        public TheMostData MostReviewedDrink { get; set; }
    }
}
