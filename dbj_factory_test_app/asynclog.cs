using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dbj_factory_test_app;

    internal class Log
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        static public void Async(string message)
        {
            _semaphore.Wait();
            try
            {
                Console.Out.WriteLineAsync(message);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

