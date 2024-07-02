using DbjFactory;

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
        DbjFactory.DbjFactory.RegisterProduct<Milk>();
        // TODO
        // what if this fails
    }
}