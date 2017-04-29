﻿using System;
using System.Collections.Generic;
using System.Linq;
using Glyde.Di.Builder;
using SimpleInjector;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorContainerConfiguration : IContainerConfiguration
    {
        private readonly Container _container;

        private Lifestyle MapLifecycle(Lifecycle lifecycle)
        {
            switch (lifecycle)
            {
                case Lifecycle.Scoped:
                    return Lifestyle.Scoped;
                case Lifecycle.Singleton:
                    return Lifestyle.Singleton;
                case Lifecycle.Transient:
                    return Lifestyle.Transient;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifecycle), lifecycle, null);
            }
        }

        public SimpleInjectorContainerConfiguration(Container container)
        {
            _container = container;
        }

        public void AddRegistration<TContract>(Lifecycle lifecycle, IContractToImplementationRegistration<TContract> registration) where TContract : class
        {
            var reg = CreateSimpleInjectorRegistration(registration);
            _container.AddRegistration(typeof(TContract), reg);
        }

        public void AddCollectionRegistration<TContract>(IEnumerable<IContractToImplementationRegistration<TContract>> registrations) 
            where TContract : class
        {
            var simpleInjectorRegistrations = registrations
                .Select(CreateSimpleInjectorRegistration)
                .ToList();

            _container.RegisterCollection<TContract>(simpleInjectorRegistrations);
        }

        private Registration CreateSimpleInjectorRegistration<TContract>(IContractToImplementationRegistration<TContract> registration)
            where TContract : class
        {
            var lifestyle = MapLifecycle(registration.Lifecycle);
            
            if (registration.ImplementationType != null)
            {
                return lifestyle.CreateRegistration(typeof(TContract), registration.ImplementationType, _container);
            }

            if (registration.FactoryType != null)
            {
                _container.Register(typeof(IServiceFactory<TContract>), registration.FactoryType, lifestyle);

                return lifestyle.CreateRegistration<TContract>(
                    () => _container.GetInstance<IServiceFactory<TContract>>().Build(), 
                    _container);
            }

            if (registration.FactoryMethod != null)
            {
                return lifestyle.CreateRegistration<TContract>(registration.FactoryMethod,
                    _container);
            }

            throw new InvalidOperationException();
        }
    }
}