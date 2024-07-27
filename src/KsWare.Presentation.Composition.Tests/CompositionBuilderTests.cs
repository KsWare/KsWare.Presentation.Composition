
using System;
using System.ComponentModel.Composition;
using System.IO;
using CommonServiceLocator;
using NUnit.Framework;

namespace KsWare.Presentation.Composition.Tests {

	[TestFixture]
	public class CompositionBuilderTests {

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
			var b=new CompositionBuilder();
			var c=b.CreateContainer();
			Assert.That(b.Catalog.Catalogs.Count,Is.GreaterThanOrEqualTo(1));
		}
	}

	

}


