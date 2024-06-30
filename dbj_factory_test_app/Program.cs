using System;
using System.Threading.Tasks;
using dbj_factory_test_app;
using DbjFactory;
using dbj_result;

namespace dbj_factory_test_app;

public class ProductA : BaseProduct
{
    // Additional properties and methods specific to ProductA
}


class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            DbjFactory.DbjFactory.RegisterProduct<ProductA>();
            //
            // IProduct? product;
            // Result<IProduct?> 
                var result = await DbjFactory.DbjFactory.GetProductAsync<ProductA>();
            Console.WriteLine(result?.GetName());
        }
        catch (InvalidOperationException iopx)
        {
            Console.WriteLine(iopx.Message);
        }
    }
}


