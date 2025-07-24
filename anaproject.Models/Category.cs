using System;
using static anaproject.Models.Cars;

namespace anaproject.Models
{
	public class Category
	{
        
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

            public ICollection<Car> Cars { get; set; }
        }
    }

