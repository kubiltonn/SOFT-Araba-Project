using System;
using static anaproject.Models.Cars;

namespace anaproject.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public decimal? PackagePrice { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}

