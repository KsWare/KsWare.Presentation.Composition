
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using CommonServiceLocator;
using NUnit.Framework;

namespace KsWare.Presentation.Composition.Tests {

	[TestFixture]
	public class CompositionBuilderExtensionTests {

		[SetUp]
		public void Setup() {
			
		}

		[Test]
		public void Simple() {
			CompositionBuilder b;
			(b=new CompositionBuilder())
				.Add(typeof(SampleService))
				.Add(new DirectoryCatalog("."))
				.Add(Assembly.GetExecutingAssembly())
				.CreateContainer()
				.Add(new object());
			Assert.That(b.Catalog.Catalogs.Count,Is.EqualTo(3));
		}
	}
	
	public class SampleService { }

}


