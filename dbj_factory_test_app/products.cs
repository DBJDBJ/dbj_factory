using DbjProduction;

public class Milk : BaseProduct
{
    // Additional properties and methods specific to ProductA
}

public class Bread : BaseProduct
{

    // Additional properties and methods specific to ProductA
}

public static class ProductRegistrar
{
    static ProductRegistrar()
    {
        DbjProduction.Factory.RegisterProduct<Milk>();
        // TODO
        // what if this fails
    }
}