using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Catalog
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));           

            var products = GetProducts();
            var categoryAndProducts = GetCategories(products);
            var json = JsonConvert.SerializeObject(categoryAndProducts, Formatting.Indented);

            XmlSerializer inst = new XmlSerializer(typeof(List<CategoriesModel>));
            TextWriter writer = new StreamWriter(path + "Catalog.xml");
            
            inst.Serialize(writer, categoryAndProducts);
            writer.Close();

            File.WriteAllText(path + "Catalog.json", json, Encoding.UTF8);
        }
    
        private static List<ProductsModel> GetProducts()
        {
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.GetEncoding("ISO-8859-1") };
                using (var reader = new StreamReader(path + "Products.csv", Encoding.GetEncoding("ISO-8859-1")))
                using (var csv = new CsvReader(reader, config))
                {
                                         

                    var records = csv.GetRecords<ProductsModel>();

                    List<ProductsModel> productos = new List<ProductsModel>();
                    foreach (var r in records)
                    {
                        productos.Add(new ProductsModel
                        {
                            Id = r.Id,
                            CategoryId = r.CategoryId,
                            Name = r.Name,
                            Price = r.Price
                        });
                    }
                    return productos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<CategoriesModel> GetCategories(List<ProductsModel> products)
        {
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.GetEncoding("ISO-8859-1") };

                using (var reader = new StreamReader(path + "Categories.csv", Encoding.GetEncoding("ISO-8859-1")))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<CategoriesModel>();
                    List<CategoriesModel> categorias = new List<CategoriesModel>();
                    foreach (var r in records)
                    {
                        var prods = products.Where(p => p.CategoryId == r.Id).ToList();
                        categorias.Add(new CategoriesModel
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            Products = prods
                        });
                    }
                    return categorias;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }       
}
