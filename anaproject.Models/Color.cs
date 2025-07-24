using System;
using static anaproject.Models.Cars;

namespace anaproject.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public decimal? ColorPrice { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}

