using System.Collections.Generic;

namespace Pizza_App.Models
{
    public class Topping
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public static List<Topping> GetDefaultToppings()
        {
            return new List<Topping>
            {
                new Topping { Name = "Pepperoni", Image = "topping_pepperoni.jpg" },
                new Topping { Name = "Mushroom", Image = "topping_mushroom.jpg" },
                new Topping { Name = "Onion", Image = "topping_onion.jpg" },
                new Topping { Name = "Bell Pepper", Image = "topping_bellpepper.jpg" },
                new Topping { Name = "Olive", Image = "topping_olive.jpg" },
                new Topping { Name = "Bacon", Image = "topping_bacon.jpg" },
                new Topping { Name = "Sausage", Image = "topping_sausage.jpg" },
                new Topping { Name = "Pineapple", Image = "topping_pineapple.jpg" }
            };
        }
    }
}
