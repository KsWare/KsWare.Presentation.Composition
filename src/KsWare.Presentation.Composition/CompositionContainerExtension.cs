using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace KsWare.Presentation.Composition {

	public static class CompositionContainerExtension {

		static readonly Dictionary<string, MethodInfo> MethodCache = new Dictionary<string, MethodInfo>();

		public static object GetExportedValue(this CompositionContainer container, Type serviceType) {
			var key = $"GetExportedValue<{serviceType.AssemblyQualifiedName}>()";
			if (!MethodCache.TryGetValue(key, out var methodInfo)) {
				methodInfo = container.GetType().GetMethods()
					.First(d => d.Name == "GetExportedValue" && d.GetParameters().Length == 0);
				Type[] genericTypeArray = { serviceType };
				methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);
				MethodCache.Add(key, methodInfo);
			}
			return methodInfo.Invoke(container, null);
		}

		public static object GetExportedValue(this CompositionContainer container, Type serviceType, string contractName) {
			//_container.GetExportedValue<serviceType>(contractName)
			var key = $"GetExportedValue<{serviceType.AssemblyQualifiedName}>(string)";
			if (!MethodCache.TryGetValue(key, out var methodInfo)) {
				methodInfo = container.GetType().GetMethods()
					.First(d => d.Name == "GetExportedValue" && d.GetParameters().Length == 1);
				Type[] genericTypeArray = { serviceType };
				methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);
				MethodCache.Add(key, methodInfo);
			}

			return methodInfo.Invoke(container, new object[] {contractName});
		}

		public static IEnumerable<object> GetExportedValues(this CompositionContainer container, Type serviceType) {
			// _container.GetExportedValues<serviceType>();
			var key = $"GetExportedValues<{serviceType.AssemblyQualifiedName}>()";
			if (!MethodCache.TryGetValue(key, out var methodInfo)) {
				methodInfo = container.GetType().GetMethods()
					.First(d => d.Name == "GetExportedValues" && d.GetParameters().Length == 0);
				Type[] genericTypeArray = { serviceType };
				methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);
				MethodCache.Add(key, methodInfo);
			}
			var enumerable = (IEnumerable)methodInfo.Invoke(container, null);
			return enumerable?.Cast<object>();
		}
	}

}
