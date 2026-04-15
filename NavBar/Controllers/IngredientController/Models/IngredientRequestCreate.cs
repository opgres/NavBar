namespace NavBar.Controllers.IngredientController.Models
{
    public class IngredientRequestCreate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float V { get; set; }
        public float BuyPrice { get; set; }
        public string Strength { get; set; }
        public int TypeDrinkId { get; set; }
    }
}
