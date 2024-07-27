using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace KsWare.Presentation.Composition {

	public class FilteredDirectoryCatalog : ComposablePartCatalog {

		private readonly AggregateCatalog _catalog;
		private readonly List<ComposablePartDefinition> _parts = new List<ComposablePartDefinition>();

		public FilteredDirectoryCatalog(string path) {
			_catalog = new AggregateCatalog();

			var files = GetFilteredFiles(Path.GetFullPath(path??"."));
			foreach (var assemblyName in files) {
				var filePath = Path.Combine(path, assemblyName);
				if (File.Exists(filePath)) {
					try {
						var asmCat = new AssemblyCatalog(filePath);
						_catalog.Catalogs.Add(asmCat);
						Debug.WriteLine($"Loaded assembly: {filePath}");
					} catch (ReflectionTypeLoadException ex) {
						Debug.WriteLine($"Could not load assembly: {filePath}");
						foreach (var loaderException in ex.LoaderExceptions) {
							Debug.WriteLine(loaderException.Message);
						}
					} catch (Exception ex) {
						Debug.WriteLine($"Error loading assembly: {filePath}, Exception: {ex.Message}");
					}
				} else {
					Debug.WriteLine($"Assembly file not found: {filePath}");
				}
			}

			foreach (var part in _catalog.Parts) {
				_parts.Add(part);
			}
		}

		private static List<string> GetFilteredFiles(string folder) {
			var includeFilePath = Path.Combine(folder, "import.cfg");
			var allFiles = Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly).ToList();
			if (!File.Exists(includeFilePath)) return allFiles;
			var includePatterns = File.ReadAllLines(includeFilePath);
			if (includePatterns.Length == 0) return allFiles;
			var filteredFiles = new List<string>();
			var includesRegex = new List<Regex>();
			var excludesRegex = new List<Regex>();
			foreach (var pattern in includePatterns) {
				var p = PreRegex(pattern, out var isComment, out var isNegation);
				if(p==null || isComment) continue;
				var regex = BuildRegex(p);
				if (isNegation) excludesRegex.Add(regex);
				else includesRegex.Add(regex);
			}
			if (includesRegex.Count == 0) {
				filteredFiles.AddRange(allFiles);
			}
			else {
				foreach (var incl in includesRegex) {
					filteredFiles.AddRange(allFiles.Where(file => incl.IsMatch(file.Substring(folder.Length + 1))));
				}
			}
			foreach (var regex in excludesRegex) {
				filteredFiles = filteredFiles.Where(file => !regex.IsMatch(file.Substring(folder.Length + 1))).ToList();
			}
			return filteredFiles.Distinct().ToList();
		}

		private static string PreRegex(string pattern, out bool isComment, out bool isNegation) {
			if (Regex.IsMatch(pattern, @"\s*#")) {
				isComment = true; 
				isNegation = false; 
				return pattern;
			}
			if (Regex.IsMatch(pattern, @"\s*!")) {
				isNegation = true;
				isComment = false;
				return pattern.Trim().Substring(1);
			}
			if (Regex.IsMatch(pattern, @"\s*\\#")) {
				isNegation = false;
				isComment = false;
				return "#" + pattern.Trim().Substring(2);
			}
			if (Regex.IsMatch(pattern, @"\s*\!")) {
				isNegation = false;
				isComment = false;
				return "!" + pattern.Trim().Substring(2);
			}
			{
				isComment = true;
				isNegation = false;
				return string.IsNullOrWhiteSpace(pattern) ? null : pattern.Trim();
			}
		}

		private static Regex BuildRegex(string pattern) {
			var regexPattern = "^" + Regex.Escape(pattern)
				.Replace(@"\*\*", ".*")
				.Replace(@"\*", @"[^\\]*")
				.Replace(@"\?", ".") + "$";
			var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
			return regex;
		}

		public override IQueryable<ComposablePartDefinition> Parts => _parts.AsQueryable();
	}

}
