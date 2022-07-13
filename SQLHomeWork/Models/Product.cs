using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHomeWork.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            Id = id;
            Name = name;
            Price = price;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }

            Name = name;
        }

        public void UpdatePrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }
            Price = price;
        }
    }
}
