using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SUS.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {

        private Dictionary<Type, Type> dependencyContainer = new Dictionary<Type, Type>();
        public void Add<TSourse, TDestination>()
        {
            this.dependencyContainer[typeof(TSourse)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (this.dependencyContainer.ContainsKey(type))
            {
                type = this.dependencyContainer[type];
            }

            var constructor = type.GetConstructors()
                                .OrderBy(x=>x.GetParameters().Count())
                                .FirstOrDefault();

            var parameters = constructor.GetParameters();

            var parametersValues = new List<object>();

            foreach (var parameter in parameters)
            {
                var parameterValue = CreateInstance(parameter.ParameterType);

                parametersValues.Add(parameterValue);
            }

            var obj = constructor.Invoke(parametersValues.ToArray());

            return obj;
        }
    }
}
