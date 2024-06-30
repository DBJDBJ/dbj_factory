
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dbj_result;


namespace DbjFactory;

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

public static class DbjFactory
{
    //private static readonly ConcurrentDictionary<Type, Type> ProductRegistry = new ConcurrentDictionary<Type, Type>();
    private static readonly
    System.Collections.Concurrent.BlockingCollection<Type>
        ProductRegistry = new();

    public static void RegisterProduct<TConcrete>()
            where TConcrete : IProduct, new()
    {
        //ProductRegistry[typeof(IProduct)] = typeof(TConcrete);
        ProductRegistry.TryAdd(typeof(TConcrete));
    }

    public static async Task<TProduct?> GetProductAsync<TProduct>()
        where TProduct : IProduct, new()
    {

        if (ProductRegistry.Contains(typeof(TProduct)))
        {
            return await Task.Run(() => (TProduct?)Activator.CreateInstance(typeof(TProduct)));
        }
        throw new InvalidOperationException($"No concrete type registered for {typeof(TProduct).Name}");
    }

    public static async Task<Result<TProduct?>?> GetProductAsyncNoThrow<TProduct>()
        where TProduct : IProduct, new()
    {

        if (ProductRegistry.Contains(typeof(TProduct)))
        {
            return await Task.Run(() =>
            Result<TProduct?>.Success(
                (TProduct?)Activator.CreateInstance(typeof(TProduct))
            ));
        }

        return Result<TProduct?>.Failure(
            $"No concrete type registered for {typeof(TProduct).Name}");

    }
}



