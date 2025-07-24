using System;
using static anaproject.Models.Cars;

namespace anaproject.Models
{
    public class TestDrive
    {
        public int TestDriveId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime PreferredDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

