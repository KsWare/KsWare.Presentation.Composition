// ***********************************************************************
// Assembly         : KsWare.Presentation.Composition
// Author           : SchreinerK
// Created          : 01-26-2020
//
// Last Modified By : SchreinerK
// Last Modified On : 01-26-2020
// ***********************************************************************
// <copyright file="CompositionBuilderExtension.cs" company="KsWare">
//     Copyright © by KsWare. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace KsWare.Presentation.Composition {

	/// <summary>
	/// Extension for <see cref="CompositionBuilder"/>
	/// </summary>
	public static class CompositionBuilderExtension {

		/// <summary>
		/// Adds the specified assembly.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="assembly">The assembly to add.</param>
		/// <returns>CompositionBuilder.</returns>
		public static CompositionBuilder Add(this CompositionBuilder builder, Assembly assembly) {
			var assemblyCatalog = new AssemblyCatalog(assembly);
			builder.Catalog.Catalogs.Add(assemblyCatalog);
			return builder;
		}

		/// <summary>
		/// Adds the specified types.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="types">The types to add.</param>
		/// <returns>CompositionBuilder.</returns>
		public static CompositionBuilder Add(this CompositionBuilder builder, params Type[] types) {
			var typeCatalog = new TypeCatalog(types);
			builder.Catalog.Catalogs.Add(typeCatalog);
			return builder;
		}

		/// <summary>
		/// Adds the specified catalog.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="catalog">The catalog to add.</param>
		/// <returns>CompositionBuilder.</returns>
		public static CompositionBuilder Add(this CompositionBuilder builder, ComposablePartCatalog catalog) {
			builder.Catalog.Catalogs.Add(catalog);
			return builder;
		}


		/// <summary>
		/// Adds the specified part.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="part">The part to add.</param>
		/// <returns>CompositionContainer.</returns>
		public static CompositionContainer Add(this CompositionContainer container, object part) {
			var batch = new CompositionBatch();
			batch.AddPart(part);
			container.Compose(batch);
			return container;
		}

	}

}
