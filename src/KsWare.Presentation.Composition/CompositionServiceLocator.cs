// ***********************************************************************
// Assembly         : KsWare.Presentation.Composition
// Author           : SchreinerK
// Created          : 01-26-2020
//
// Last Modified By : SchreinerK
// Last Modified On : 01-26-2020
// ***********************************************************************
// <copyright file="CompositionServiceLocator.cs" company="KsWare">
//     Copyright © by KsWare. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using CommonServiceLocator;

namespace KsWare.Presentation.Composition {

	/// <summary>
	/// Implements the <see cref="CommonServiceLocator.IServiceLocator" /> for System.ComponentModel.Composition 4.7
	/// </summary>
	/// <seealso cref="CommonServiceLocator.IServiceLocator" />
	/// <example>
	///   <code language="csharp">
	/// var container = new ContainerConfiguration()
	/// .WithAssemblies(...)
	/// .CreateContainer();
	/// ServiceLocator.SetLocatorProvider(() =&gt; new CompositionServiceLocator(container));
	/// </code>
	/// </example>
	public class CompositionServiceLocator : IServiceLocator {

		private readonly CompositionContainer _container;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionServiceLocator"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public CompositionServiceLocator(CompositionContainer container) {
			_container = container;
		}

		/// <summary>
		/// Gets the service object of the specified type.
		/// </summary>
		/// <param name="serviceType">An object that specifies the type of service object to get.</param>
		/// <returns>A service object of type <paramref name="serviceType" />.
		/// -or-
		/// <see langword="null" /> if there is no service object of type <paramref name="serviceType" />.</returns>
		public object GetService(Type serviceType) => GetInstance(serviceType);

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns>System.Object.</returns>
		public object GetInstance(Type serviceType) => _container.GetExportedValue(serviceType);

		/// <summary>
		/// Gets the instance with the specified type and contract name.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="contractName">Name of the contract.</param>
		/// <returns>System.Object.</returns>
		public object GetInstance(Type serviceType, string contractName) => _container.GetExportedValue(serviceType, contractName);

		/// <summary>
		/// Gets all instances of specified type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns>IEnumerable&lt;System.Object&gt;.</returns>
		public IEnumerable<object> GetAllInstances(Type serviceType) => _container.GetExportedValues(serviceType);

		/// <summary>
		/// Gets the instance with the specified type.
		/// </summary>
		/// <typeparam name="TService">The type of service.</typeparam>
		/// <returns>TService.</returns>
		public TService GetInstance<TService>() => _container.GetExportedValue<TService>();

		/// <summary>
		/// Gets the instance with the specified type and key.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <param name="key">The key.</param>
		/// <returns>TService.</returns>
		public TService GetInstance<TService>(string key) => _container.GetExportedValue<TService>(key);

		/// <summary>
		/// Gets all instances of the specified type.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <returns>IEnumerable&lt;TService&gt;.</returns>
		public IEnumerable<TService> GetAllInstances<TService>() => _container.GetExportedValues<TService>();

	}

}
