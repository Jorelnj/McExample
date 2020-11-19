using McExample.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.DAL
{
    public class ProductDAO
    {
        private static List<Product> products;
        private const string FILE_NAME = @"data/products.json";
        private FileInfo file;
        public ProductDAO()
        {
            file = new FileInfo(FILE_NAME);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            if (!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }
            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    products = JsonConvert.DeserializeObject<List<Product>>(json);
                }
                if(products == null)
                {
                    products = new List<Product>();
                }
                    
            }
        }
        public void Add(Product product)
        {
            products.Add(product);
            Save();
        }

        private void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(products);
                sw.WriteLine(json);
            }
        }

        public void Remove(Product product)
        {
            products.Remove(product); //base sur Product.Equals redefini
            Save();
        }

        public IEnumerable<Product> Find()
        {
            return new List<Product>(products);
        }
    }
}
