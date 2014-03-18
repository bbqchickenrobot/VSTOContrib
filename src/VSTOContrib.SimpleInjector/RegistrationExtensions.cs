using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleInjector;
using VSTOContrib.Core.RibbonFactory.Interfaces;

namespace VSTOContrib.SimpleInjector
{
    public static class RegistrationExtensions
    {
        public static void RegisterRibbonViewModels(this Container container, Assembly assemblyToScan)
        {
            if (assemblyToScan == null) return;
            var registrations = from type in assemblyToScan.GetExportedTypes()
                from service in type.GetInterfaces()
                where type.IsAssignableFrom(typeof(IRibbonViewModel)) && type.IsClass
                select new { Service = service, Implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.Service, reg.Implementation);
            }
        }
    }
}
