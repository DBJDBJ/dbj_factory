// global aliases but for this project only
global using FactoryResult = dbj_result.Result<DbjProduction.IProduct>;
using dbj_factory_test_app;
using products;

// if you think this is not elegant
// it replaces a lot of code required to achieve the same
// using DI, IHostedService and all that jazz 
ProductRegistrar.EnsureStaticCtorCalled();

try
{
    // cowie we can use, Milk was registered
    Milk ? cowie = await ObtainProduct<Milk>() as Milk ;

    // baget is null, Bread was not registered
    Bread ? baget = await ObtainProduct<Bread>() as Bread;
}
catch (Exception x)
{
    Log.Async(x.Message);
}

// no this is not to be inside factory, because it would
// create dependancy on logging mechanism
static async Task<DbjProduction.IProduct> ObtainProduct<TProduct>()
    where TProduct : DbjProduction.IProduct, new()
{
    FactoryResult
        result =
            await DbjProduction.Factory.GetProductAsync<TProduct>();

    //if (false == result.IsSuccess)
    // this is error message
    Log.Async(result.Message);
    return result.Data; 
}



