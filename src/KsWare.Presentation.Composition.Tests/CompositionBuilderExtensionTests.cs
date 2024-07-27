
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using CommonServiceLocator;
using NUnit.Framework;

namespace KsWare.Presentation.Composition.Tests {

	[TestFixture]
	public class CompositionBuilderExtensionTests {

		[OneTimeSetUp]
		public void OneTimeSetUp() {
			//workaround for local exception: Microsoft.VisualStudio.TestPlatform.ObjectModel not found;
			File.WriteAllText("import.cfg", @"
				!Microsoft.TestPlatform.CoreUtilities**
				!Microsoft.VisualStudio.TestPlatform**
				!nunit**
			");
		}

		[Test]
		public void Simple() {
			CompositionBuilder b;
			(b=new CompositionBuilder())
				.Add(typeof(SampleService))
				.Add(new FilteredDirectoryCatalog("."))
				.Add(Assembly.GetExecutingAssembly())
				.CreateContainer()
				.Add(new object());
			Assert.That(b.Catalog.Catalogs.Count,Is.EqualTo(3));
		}
	}
	
	public class SampleService { }

}


