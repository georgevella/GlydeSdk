using System;
using System.Collections.Generic;

namespace Glyde.Di
{
    public interface IContainer : IServiceProvider
    {
        T GetService<T>() where T : class;

        IEnumerable<T> GetServices<T>() where T : class;
    }
}