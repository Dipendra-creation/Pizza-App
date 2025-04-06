using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Pizza_App.Models;
using Xamarin.Forms;

namespace Pizza_App.ViewModels
{
    public class PizzaCustomizationViewModel : BaseViewModel
    {
        private const int MaxToppings = 5;
        private decimal basePrice;

        public PizzaCustomizationViewModel(Pizza selectedPizza)
        {
            SelectedPizza = selectedPizza;
            SelectedSize = "medium";
            SelectedCrust = "thin";
            Toppings = new ObservableCollection<Topping>(Topping.GetDefaultToppings());
            SelectedToppings = new ObservableCollection<Topping>();

            basePrice = selectedPizza.Price;
            UpdatePrice();
        }

        public Pizza SelectedPizza { get; }

        private string selectedSize;
        public string SelectedSize
        {
            get => selectedSize;
            set
            {
                if (selectedSize != value)
                {
                    selectedSize = value;
                    OnPropertyChanged();
                    UpdatePrice();
                }
            }
        }

        private string selectedCrust;
        public string SelectedCrust
        {
            get => selectedCrust;
            set
            {
                if (selectedCrust != value)
                {
                    selectedCrust = value;
                    OnPropertyChanged();
                    UpdatePrice();
                }
            }
        }

        public ObservableCollection<Topping> Toppings { get; set; }
        public ObservableCollection<Topping> SelectedToppings { get; set; }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        public string ToppingCounter => $"{SelectedToppings.Count}/{MaxToppings} selected";

        public ICommand SelectSizeCommand => new Command<string>((size) => SelectedSize = size);
        public ICommand SelectCrustCommand => new Command<string>((crust) => SelectedCrust = crust);

        public ICommand ToggleToppingCommand => new Command<Topping>((topping) =>
        {
            if (SelectedToppings.Contains(topping))
            {
                SelectedToppings.Remove(topping);
            }
            else if (SelectedToppings.Count < MaxToppings)
            {
                SelectedToppings.Add(topping);
            }

            OnPropertyChanged(nameof(ToppingCounter));
            UpdatePrice();
        });

        private void UpdatePrice()
        {
            decimal v = selectedSize switch
            {
                "small" => 1.0m,
                "medium" => 1.2m,
                "large" => 1.4m,
                _ => 1.0m
            };
            decimal sizeMultiplier = v;

            decimal crustCost = selectedCrust == "thick" ? 2.0m : 0.0m;
            decimal toppingCost = SelectedToppings.Count * 1.5m;

            TotalPrice = Math.Round((basePrice * sizeMultiplier) + crustCost + toppingCost, 2);
        }
    }
}
