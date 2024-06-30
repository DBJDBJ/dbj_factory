using System;
using System.Threading.Tasks;
using DbjFactory;
using MyProducts;

namespace MyProducts
{
    using DbjFactory;

    public class ProductA : BaseProduct
    {
        // Additional properties and methods specific to ProductA
    }
}

namespace FactoryApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DbjFactory.RegisterProduct<ProductA>();

            IProduct product = await DbjFactory.GetProductAsync<IProduct>();
            Console.WriteLine(product.GetName());
        }
    }
}

