//using System.IO;
//using System.Linq;
//using System.Runtime.Loader;
//
//namespace KsWare.Presentation.Composition {
//	public static class ContainerConfigurationExtension {
//
//		public static ContainerConfiguration WithAssembliesFromDirectory(this ContainerConfiguration configuration, string directory) {
//			// Find and load all the DLLs in the folder 
//			var assemblies = Directory.GetFiles(directory, "*.dll")
//				.Select(path => AssemblyLoadContext.Default.LoadFromAssemblyPath(path))
//				.Where(x => x != null);
//
//			// Add the loaded assemblies to the container 
//			configuration = configuration
//				.WithAssemblies(assemblies)
////				.WithAssembly(this.GetType().GetTypeInfo().Assembly)
//			;
////			var configuration = new ContainerConfiguration().WithAssembly(typeof(ContainerConfigurationExtension).Assembly);
//			return configuration;
//		}
//	}
//}
