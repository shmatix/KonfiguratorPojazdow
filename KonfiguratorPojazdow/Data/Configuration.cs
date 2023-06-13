using System.ComponentModel.DataAnnotations;

namespace KonfiguratorPojazdow.Data
{

    public class Configuration
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Display(Name = "Pojazd")]
        public int CarId { get; set; }
        [Display(Name = "Silnik")]
        public int EngineId { get; set; }
        [Display(Name = "Lakier")]
        public int PaintId { get; set; }
        [Display(Name = "Pakiet wnętrze")]
        public InteriorType Interior { get; set; }
        [Display(Name = "Felgi")]
        public RimsType Rims { get; set; }


        [Display(Name = "Pojazd")]
        public Car? Car { get; set; }
        [Display(Name = "Silnik")]
        public Engine? Engine { get; set; }
        [Display(Name = "Lakier")]
        public Paint? Paint { get; set; }

        [Display(Name = "Cena razem")]
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                if (Car != null)
                {
                    totalPrice += Car.Price;
                }

                if (Engine != null)
                {
                    totalPrice += Engine.Price;
                }

                if (Paint != null)
                {
                    totalPrice += Paint.Price;
                }

                // Dodatkowe koszty w zależności od wybranych opcji Interior i Rims
                switch (Interior)
                {
                    case InteriorType.Premium:
                        totalPrice += 6000;
                        break;
                    case InteriorType.Deluxe:
                        totalPrice += 12000;
                        break;
                    default:
                        // Dla opcji Standard nie dodajemy żadnych dodatkowych kosztów
                        break;
                }

                switch (Rims)
                {
                    case RimsType.Aluminium:
                        totalPrice += 4500;
                        break;
                    case RimsType.Chrome:
                        totalPrice += 15000;
                        break;
                    default:
                        // Dla opcji Steel nie dodajemy żadnych dodatkowych kosztów
                        break;
                }

                return totalPrice;
            }
        }

        public Configuration(string userId, int carId, int engineId, int paintId, InteriorType interior, RimsType rims, Car car, Engine engine, Paint paint)
        {
            UserId = userId;
            CarId = carId;
            EngineId = engineId;
            PaintId = paintId;
            Interior = interior;
            Rims = rims;
            Car = car;
            Engine = engine;
            Paint = paint;
        }

        public Configuration() { }

        
    }
}
