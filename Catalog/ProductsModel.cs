using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog
{
    public class ProductsModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
