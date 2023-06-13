using System.ComponentModel.DataAnnotations;

namespace KonfiguratorPojazdow.Data
{
    

    public class Engine
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Pojemność")]
        public int Capacity { get; set; }
        [Display(Name = "Rodzaj paliwa")]
        public FuelType Fuel { get; set; }
        [Display(Name = "Cena extra")]
        public decimal Price { get; set; }
        [Display(Name = "Hybryda")]
        public bool Hybrid { get; set; }

        public string Display { get => ToString(); }

        public ICollection<Configuration>? Configurations { get; set; }

        public Engine(string name, int capacity, FuelType fuel, decimal price, bool hybrid)
        {
            Name = name;
            Capacity = capacity;
            Fuel = fuel;
            Price = price;
            Hybrid = hybrid;
        }

        public Engine() { }

        public override string ToString()
        {
            return $"{Name} {Capacity}";
        }
    }
}
