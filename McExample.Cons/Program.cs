using McExample.BLL;
using McExample.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.Cons
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string choice = "y";
            do
            {
                Console.Clear();
                Console.WriteLine("**********Creer un produit**********");
                Console.Write("Entrer la référence du produit\t:");
                string reference = Console.ReadLine();
                Console.Write("Entrer le nom du produit\t:");
                string name = Console.ReadLine();
                Console.Write("Entrer le prix unitaire\t:");
                double price = double.Parse(Console.ReadLine());
                Console.Write("Entrer la TVA\t");
                float tax = float.Parse(Console.ReadLine());

                Product product = new Product(reference, name, price, tax);
                ProductBLO productBLO = new ProductBLO();
                productBLO.CreateProduct(product);

                IEnumerable<Product> products = productBLO.GetAllProducts();
                foreach (Product p in products)
                {
                    Console.WriteLine($"{p.Reference}\t{p.Name}");
                }

                Console.WriteLine("Voulez-vous créer un autre produit ?[y/n]");
                choice = Console.ReadLine();

            } 
            
            while (choice.ToLower() != "n");
            

            Console.ReadKey();

        }
    }
}
