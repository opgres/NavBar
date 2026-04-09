using NavBar.Models;

namespace NavBar.Controllers.IngredientController.Models
{
    public class FilterAll
    {
        public Availability Available { get; set; }
        public List<int> TypeDrinkId { get; set; }
    }
}
