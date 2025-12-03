namespace NavBar.Models
{
    public class Review
    {
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Comment { get; set; }
        public int Score { get; set; }
    }
}
