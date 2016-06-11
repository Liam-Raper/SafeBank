using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using System.Web.Mvc;
using Data.Standard.Classed;
using Data.Standard.Interfaces;
using Security.Classes.SecurityQuestions;
using Security.Interfaces.SecurityQuestions;

namespace Common
{
    public static class IoCManager
    {

        public class IoCDependencyResolver : IDependencyResolver
        {

            private readonly Container _container;

            public IoCDependencyResolver(Container container)
            {
                this._container = container;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return _container.TryGetInstance(serviceType);
                }
                return _container.GetInstance(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return _container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
            }

        }

        public static void SetUp()
        {
            var container = new Container(registry =>
                {
                    registry.For<IUnitOfWork>().Use<UnitOfWork>();
                    registry.For<ISecurityQuestions>().Use<SecurityQuestions>();
                }
            );
            DependencyResolver.SetResolver(new IoCDependencyResolver(container));
        }
    }
    
}
