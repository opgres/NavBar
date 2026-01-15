namespace NavBar.Controllers.ReviewController.Models
{
    public class ReviewRequestCreate
    {
        public int UserId { get; set; }
        public int CocktailId { get; set; }
        public string? Comment { get; set; }
        public int Score { get; set; }
    }
}
