using System;
using System.Collections.Generic;
using Glyde.ApplicationSupport.ApplicationStartup;

namespace Glyde.ApplicationSupport.ApplicationConfiguration
{
    public interface IApplicationConfigurationBuilder
    {
        void RegisterStartupService<T>()
            where T : IRunOnStartup;
    }

    internal class ApplicationConfigurationBuilder : IApplicationConfigurationBuilder
    {
        private readonly IList<Type> _startupServices;

        public ApplicationConfigurationBuilder(IList<Type> startupServices)
        {
            _startupServices = startupServices;
        }
        public void RegisterStartupService<T>() where T : IRunOnStartup
        {
            _startupServices.Add(typeof(T));
        }
    }
}