using static anaproject.Models.Cars;

namespace anaproject.Models
{
    public class CarImage
    {
        public int ImageId { get; set; } // <-- PRIMARY KEY
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public int Order { get; set; }
    }
}