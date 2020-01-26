// ***********************************************************************
// Assembly         : KsWare.Presentation.Composition
// Author           : SchreinerK
// Created          : 01-26-2020
//
// Last Modified By : SchreinerK
// Last Modified On : 01-26-2020
// ***********************************************************************
// <copyright file="CompositionBuilder.cs" company="KsWare">
//     Copyright © by KsWare. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using CommonServiceLocator;

namespace KsWare.Presentation.Composition {

	/// <summary>
	/// Provides a composition builder.
	/// </summary>
	/// <remarks><para>
	/// The <see cref="CompositionBuilder"/> was developed to provide a <see cref="CompositionContainer"/> with little effort.
	/// <code language="CSarp">new CompositionBuilder().CreateContainer();</code>
	/// </para>
	/// <para>The <see cref="CompositionBuilder"/> is customizable using the extension methods. <br/>
	/// - add assembly, <br/>
	/// - add types, <br/>
	/// - add catalogs and <br/>
	/// - add parts <br/>
	/// using <see cref="CompositionBuilderExtension"/></para>
	/// </remarks>
	/// <example>
	/// Creates the container with a default DirectoryCatalog and registers it to ServiceLocator.
	/// <code language="CSarp">
	/// new CompositionBuilder().CreateContainer();
	/// ServiceLocator.Current.GetService&lt;SampleService&gt;();
	/// </code>
	/// </example>
	/// <example>
	/// Creates customized container and registers it to ServiceLocator.
	/// <code>
	/// new CompositionBuilder())
	///    .Add(new DirectoryCatalog("."))
	///    .Add(typeof(SampleService))
	///    .Add(Assembly.LoadFile("Plugins/Loader.dll"))
	///    .CreateContainer()
	///    .Add(new MyExtraPart());
	/// </code>
	/// </example>
	public class CompositionBuilder {

		/// <summary>
		/// Gets or sets the container.
		/// </summary>
		/// <value>The container.</value>
		public CompositionContainer Container {get;set;}

		/// <summary>
		/// Gets or sets the catalog.
		/// </summary>
		/// <value>The catalog.</value>
		public AggregateCatalog Catalog { get; set; } = new AggregateCatalog();

		/// <summary>
		/// Creates the container.
		/// </summary>
		/// <returns>CompositionContainer.</returns>
		/// <remarks>
		/// <para>If <see cref="Catalog"/> is empty a <see cref="DirectoryCatalog"/> with default directory is added.</para> 
		/// <para>The container is registered to <see cref="ServiceLocator"/></para></remarks>
		public CompositionContainer CreateContainer() {
			if(Catalog==null)
				Catalog = new AggregateCatalog();
			if (!Catalog.Catalogs.Any())
				Catalog.Catalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(typeof(CompositionBuilder).Assembly.Location)));
			Container = new ExtendedCompositionContainer(Catalog);
			ServiceLocator.SetLocatorProvider(() => new CompositionServiceLocator(Container));
			return Container;
		}
	}

}