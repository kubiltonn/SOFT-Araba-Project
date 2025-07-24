﻿using System;
using static anaproject.Models.Cars;

namespace anaproject.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

