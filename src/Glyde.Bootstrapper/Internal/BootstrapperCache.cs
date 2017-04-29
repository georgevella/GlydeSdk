using System;
using System.Collections.Concurrent;

namespace Glyde.Bootstrapper.Internal
{
    internal class BootstrapperCache
    {
        private static ConcurrentDictionary<Type, IBootstrapper> Cache { get; } = new ConcurrentDictionary<Type, IBootstrapper>();

        public static IBootstrapper Get<TBootstrapper>()
            where TBootstrapper : IBootstrapper, new()
        {
            return Cache.GetOrAdd(typeof(TBootstrapper), type => new TBootstrapper());
        }

        public static IBootstrapper Get(Type bootstrapperType)
        {
            return Cache.GetOrAdd(bootstrapperType, type => (IBootstrapper)Activator.CreateInstance(type));
        }
    }
}