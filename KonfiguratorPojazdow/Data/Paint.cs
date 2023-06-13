using System.ComponentModel.DataAnnotations;

namespace KonfiguratorPojazdow.Data
{
    

    public class Paint
    {
        public int Id { get; set; }
        [Display(Name = "Kolor")]
        public string Color { get; set; }
        [Display(Name = "Typ")]
        public PaintType Type { get; set; }
        [Display(Name = "Cena extra")]
        public decimal Price { get; set; }

        public string Display { get => ToString(); }

        public ICollection<Configuration>? Configurations { get; set; }

        public Paint(string color, PaintType type, decimal price)
        {
            Color = color;
            Type = type;
            Price = price;
        }

        public Paint() { }

        public override string ToString()
        {
            return $"{Color} {Type}";
        }
    }
}
