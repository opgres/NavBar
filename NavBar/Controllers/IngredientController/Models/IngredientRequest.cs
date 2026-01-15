namespace NavBar.Controllers.IngredientController.Models
{
    public class IngredientRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float V { get; set; }
        public float AvgBuyPrice { get; set; }
        public string? Color { get; set; }
        public string? Ro { get; set; }
        public string? Strength { get; set; }
        public int TypeDrinkId { get; set; }

    }
}
