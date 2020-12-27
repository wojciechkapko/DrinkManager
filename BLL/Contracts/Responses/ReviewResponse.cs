using System;

namespace BLL.Contracts.Responses
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public int ReviewScore { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }

        public string AuthorName { get; set; }
    }
}