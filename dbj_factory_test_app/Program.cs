// global aliases but for this project only
global using FactoryResult = dbj_result.Result<DbjFactory.IProduct>;
using dbj_result;

//using System;
//using System.Threading.Tasks;
//using dbj_factory_test_app;
using DbjFactory;
//using dbj_result;


namespace dbj_factory_test_app;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            DbjFactory.DbjFactory.RegisterProduct<ProductA>();

            _ = await GetProduct<ProductA>();
            _ = await GetProduct<ProductB>();
        }
        catch (Exception x)
        {
            Console.WriteLine(x.Message);
        }
    }

    static async Task<FactoryResult> GetProduct<TProduct>( )
        where TProduct : DbjFactory.IProduct, new()
    {
        FactoryResult 
            result =
                await DbjFactory.DbjFactory.GetProductAsync<TProduct>();

                //if (false == result.IsSuccess)
                // this is error message
                 Console.WriteLine(result.Message);
        return result ;
    }
}


