namespace BLL.Contracts.Requests
{
    public class ReviewCreateRequest
    {
        public string ReviewScore { get; set; }

        public string ReviewText { get; set; }

        public string AuthorName { get; set; }
    }
}