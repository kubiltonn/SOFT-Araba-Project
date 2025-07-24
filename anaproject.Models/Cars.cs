using System;

namespace anaproject.Models
{
    public class Cars
    {
        public class Car
        {
            public int ID { get; set; }
            public string CarAdi { get; set; }
            public string CarTur { get; set; }
            public string CarModel { get; set; }
            public int CarYil { get; set; }
            public decimal CarFiyat { get; set; }

            // Renk ilişkisi (FK)
            public int ColorId { get; set; }

            public Color Color { get; set; }

            // Donanım paketi ilişkisi (FK)
            public int PackageId { get; set; }

            public Package Package { get; set; }

            public string CarImage { get; set; }
            public int CarBeygir { get; set; }
            public int CarMenzil { get; set; }
            public int CarKW { get; set; }
            public decimal CarSifirYuz { get; set; }

            // Her araba için özel açıklama
            public string CarDescription { get; set; }

            // Her araba için motor sesi dosya yolu
            public string EngineSoundUrl { get; set; }

            // Kategori ilişkisi (FK)
            public int CategoryId { get; set; }

            public Category Category { get; set; }

            // Galeri ilişkisi (bir araca birden fazla resim)
            public ICollection<CarImage> CarImages { get; set; }

            // Yorumlar ilişkisi
            public ICollection<Review> Reviews { get; set; }

            // Test sürüşü başvuruları ilişkisi
            public ICollection<TestDrive> TestDrives { get; set; }
        }
    }
}