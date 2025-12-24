namespace NavBar.Controllers.CocktailRequestontroller.Models
{
    public class CocktailRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int CookingMethodId { get; set; }

    }
}
