
using System;
using System.ComponentModel.Composition;
using CommonServiceLocator;
using NUnit.Framework;

namespace KsWare.Presentation.Composition.Tests {

	[TestFixture]
	public class CompositionBuilderTests {

		public void Setup() {
			//workaround for local exception: Microsoft.VisualStudio.TestPlatform.ObjectModel not found;
		}

		[Test]
		public void Simple() {
			var b=new CompositionBuilder();
			var c=b.CreateContainer();
			Assert.That(b.Catalog.Catalogs.Count,Is.GreaterThanOrEqualTo(1));
		}
	}

	

}


