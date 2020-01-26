using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using CommonServiceLocator;

namespace KsWare.Presentation.Composition {

	// INCLUDE nuget 

	/// <summary>
	/// Implements the <see cref="CommonServiceLocator.IServiceLocator" /> for System.ComponentModel.Composition 4.7
	/// </summary>
	/// <seealso cref="CommonServiceLocator.IServiceLocator" />
	/// <example>
	/// <code language="csharp">
	/// var container = new ContainerConfiguration()
	///                     .WithAssemblies(...)
	///                     .CreateContainer();
	/// ServiceLocator.SetLocatorProvider(() => new CompositionServiceLocator(container));
	/// </code>
	/// </example>
	public class CompositionServiceLocator : IServiceLocator {

		

		private readonly CompositionContainer _container;

		public CompositionServiceLocator(CompositionContainer container) {
			_container = container;
		}

		public object GetService(Type serviceType) => GetInstance(serviceType);

		public object GetInstance(Type serviceType) => _container.GetExportedValue(serviceType);

		public object GetInstance(Type serviceType, string contractName) => _container.GetExportedValue(serviceType, contractName);

		public IEnumerable<object> GetAllInstances(Type serviceType) => _container.GetExportedValues(serviceType);

		public TService GetInstance<TService>() => _container.GetExportedValue<TService>();

		public TService GetInstance<TService>(string key) => _container.GetExportedValue<TService>(key);

		public IEnumerable<TService> GetAllInstances<TService>() => _container.GetExportedValues<TService>();

	}

}
