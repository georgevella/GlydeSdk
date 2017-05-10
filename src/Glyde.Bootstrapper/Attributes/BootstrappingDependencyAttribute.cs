using System;

namespace Glyde.Bootstrapper.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BootstrappingDependencyAttribute : Attribute
    {
        public Type DependantBootstrapperStageType { get; }

        public BootstrappingDependencyAttribute(Type dependantBootstrapperStageType)
        {
            DependantBootstrapperStageType = dependantBootstrapperStageType;
        }
    }
}