﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog
{
    public class CategoriesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductsModel> Products { get; set; }
    }
}
