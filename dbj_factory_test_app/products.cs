using dbj_factory_test_app;
using DbjProduction;

namespace products;

public class Milk : BaseProduct
{
    // Additional properties and methods specific to Milk
    bool Ok4Chesse { get;} = true ;
}

public class Bread : BaseProduct
{
    // Additional properties and methods specific to Bread
    bool OvenBaked { get;} = true;
}


public static class ProductRegistrar
{
    static ProductRegistrar()
    {
        DbjProduction.Factory.RegisterProduct<Milk>();
        // TODO
        // what if this fails
        Log.Async("Products registered");
    }

    // this will provoke calling the static ctor first
    // one needs to call metod like bellow 
    // if there is no Main method
    public static void EnsureStaticCtorCalled()
    {
        Log.Async("ProductRegistrar static ctor was called");
    }
}
