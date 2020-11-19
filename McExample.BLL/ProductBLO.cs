using McExample.BO;
using McExample.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.BLL
{
    public class ProductBLO
    {
        ProductDAO productRepo;         
        public ProductBLO()
        {
            productRepo = new ProductDAO();
        }
        public void CreateProduct(Product product)
        {
            productRepo.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            productRepo.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productRepo.Find();
        }
    }
}
