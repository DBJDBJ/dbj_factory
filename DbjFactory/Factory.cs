// global aliase used here
//global using FactoryResult = dbj_result.Result<DbjFactory.IProduct>;

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dbj_result;


// pay attention this is a global type alias
// inhere C# compiler needs name spaces
// still this alias is not visible outside of this file
// using FactoryResult = dbj_result.Result<DbjFactory.IProduct>;
// ditto we use the alisases.cs 
// in here and wnerever required 

namespace DbjProduction;

// pay attention this is inside the name space where IProduct is defined
// using FactoryResult = Result<IProduct>;


public interface IProduct
{
    string GetName();
}

public abstract class BaseProduct : IProduct
{
    public string GetName()
    {
        return this.GetType().Name;
    }
}

public static class Factory
{
    // ConcurrentDictionary, is a overkill here
    private static readonly
    System.Collections.Concurrent.ConcurrentBag<Type>
        ProductRegistry = new();

    public static void RegisterProduct<TConcrete>()
            where TConcrete : IProduct, new()
    {
        ProductRegistry.Add(typeof(TConcrete));
    }

    //public static async Task<TProduct?> GetProductAsync<TProduct>()
    //    where TProduct : IProduct, new()
    //{

    //    if (ProductRegistry.Contains(typeof(TProduct)))
    //    {
    //        return await Task.Run(() => (TProduct?)Activator.CreateInstance(typeof(TProduct)));
    //    }
    //    throw new InvalidOperationException($"No concrete type registered for {typeof(TProduct).Name}");
    //}

    public static async Task<FactoryResult>
        GetProductAsync<TProduct>()
        where TProduct : IProduct, new()
    {
        try
        {
            if (ProductRegistry.Contains(typeof(TProduct)))
            {
                return await Task.Run(() =>
                {
                    // 2024 c# compiler and legacy casting dont go well together , example
                    // (IProduct)Activator.CreateInstance(typeof(TProduct))
                    // if not using "as" as bellow
                    // nullability complains will arrise
                    IProduct? product = Activator.CreateInstance(typeof(TProduct)) as IProduct ;

                    if (product is not null)
                        return FactoryResult.Success(
                           $"Concrete type {typeof(TProduct).Name}, instance created"
                           , product
                         );

                    return FactoryResult.Failure(
        $"Concrete type {typeof(TProduct).Name}, but could not create its instance");
                });
            }

            return FactoryResult.Failure(
                $"No concrete type registered for {typeof(TProduct).Name}");
        }
        catch (Exception ex)
        {
            return FactoryResult.Failure("General Exception from GetProductAsyncNoThrow", ex);
        }
    }
}



