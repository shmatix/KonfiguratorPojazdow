using System.ComponentModel.DataAnnotations;

namespace KonfiguratorPojazdow.Data
{
   

    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        [Display(Name = "Ilość drzwi")]
        public int Doors { get; set; }
        [Display(Name = "Typ")]
        public CarType Type { get; set; }
        [Display(Name = "Cena bazowa")]
        public decimal Price { get; set; }

        public string Display {  get => ToString(); }

        public ICollection<Configuration>? Configurations { get; set; }

        public Car(string model, int doors, CarType type, decimal price)
        {
            Model = model;
            Doors = doors;
            Type = type;
            Price = price;
        }

        public Car() { }

        public override string ToString()
        {
            return $"{Model} {Type}";
        }
    }
}
