using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using SimpleInjector.Packaging;
using VSTOContrib.Core;
using VSTOContrib.Core.RibbonFactory.Interfaces;

namespace VSTOContrib.SimpleInjector
{
    /// <summary>
    /// Simple Injector View Model Factory
    /// </summary>
    public class SimpleInjectorViewModelFactory : IViewModelFactory, IDisposable
    {
        readonly Container _container;
        readonly Dictionary<IRibbonViewModel, LifetimeScope> _lifetimeScopeLookups = 
                new Dictionary<IRibbonViewModel, LifetimeScope>(); 
     
        public SimpleInjectorViewModelFactory(IEnumerable<Assembly> assemblies, Container container = null)
        {
            _container = container ?? new Container();
            _container.RegisterPackages(); // todo - assess redundancy
            _container.RegisterPackages(assemblies);
        }

        public SimpleInjectorViewModelFactory(Container container)
        {
            this._container = container;
        }


        public IRibbonViewModel Resolve(Type viewModelType)
        {
            var lifetime = _container.BeginLifetimeScope();
            var viewModel = (IRibbonViewModel) _container.GetInstance(viewModelType);
            _lifetimeScopeLookups.Add(viewModel, lifetime);
            return viewModel;
        }

        public void Release(IRibbonViewModel viewModelInstance)
        {
            var lifetimeScope = _lifetimeScopeLookups[viewModelInstance];
            _lifetimeScopeLookups.Remove(viewModelInstance);
            lifetimeScope.Dispose();
        }

        public void Dispose()
        {
            
        }

    }
}
