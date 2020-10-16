using System;

namespace SUS.MvcFramework
{
    public interface IServiceCollection
    {
        void Add<TSourse, TDestination>();

        object CreateInstance(Type type);
    }
}
