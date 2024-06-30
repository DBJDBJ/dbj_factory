
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;


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
        private static readonly ConcurrentDictionary<Type, Type> ProductRegistry = new ConcurrentDictionary<Type, Type>();

        public static void RegisterProduct<TConcrete>()
            where TConcrete : IProduct, new()
        {
            ProductRegistry[typeof(IProduct)] = typeof(TConcrete);
        }

        public static async Task<TProduct> GetProductAsync<TProduct>()
            where TProduct : IProduct
        {
            if (ProductRegistry.TryGetValue(typeof(TProduct), out Type concreteType))
            {
                return await Task.Run(() => (TProduct)Activator.CreateInstance(concreteType));
            }
            throw new InvalidOperationException($"No concrete type registered for {typeof(TProduct).Name}");
        }
    }


