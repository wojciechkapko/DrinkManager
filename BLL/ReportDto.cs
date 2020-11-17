using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ReportDto
    {
        public string SuccessfulLoginsAmount { get; set; }
        public string MostFavorableDrink { get; set; }
        public string HighestScoreDrink { get; set; }
        public string LowestScoreDrink { get; set; }
        public string MostVisitedDrink { get; set; }
        public string MostActiveUser { get; set; }
        public string MostReviewedDrink { get; set; }
    }
}
