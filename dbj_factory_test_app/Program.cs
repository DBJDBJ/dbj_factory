// global aliases but for this project only
global using FactoryResult = dbj_result.Result<DbjProduction.IProduct>;
using dbj_result;

//using System;
//using System.Threading.Tasks;
//using dbj_factory_test_app;
using DbjProduction;
//using dbj_result;


namespace dbj_factory_test_app;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            _ = await GetProduct<Milk>();
            _ = await GetProduct<Bread>();
        }
        catch (Exception x)
        {
            Console.WriteLine(x.Message);
        }
    }

    static async Task<FactoryResult> GetProduct<TProduct>( )
        where TProduct : DbjProduction.IProduct, new()
    {
        FactoryResult 
            result =
                await DbjProduction.Factory.GetProductAsync<TProduct>();

                //if (false == result.IsSuccess)
                // this is error message
                 Console.WriteLine(result.Message);
        return result ;
    }
}


