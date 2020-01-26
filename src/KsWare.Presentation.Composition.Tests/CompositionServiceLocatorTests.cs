using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using NUnit.Framework;

namespace KsWare.Presentation.Composition.Tests {

	[TestFixture]
	public class CompositionServiceLocatorTests {

		public CompositionContainer Container { get; set; }

		[SetUp]
		public void SetUp() {
			var assemblyCatalog = new AssemblyCatalog(typeof(CompositionServiceLocatorTests).Assembly);
			Container = new CompositionContainer(assemblyCatalog);
		}


		[TearDown]
		public void TearDown() {
			Container?.Dispose();
		}

		private CompositionServiceLocator CreateCompositionServiceLocator() {
			return new CompositionServiceLocator(Container);
		}

		[Test]
		public void GetService_Call_Success() {
			// Arrange
			var sut = new CompositionServiceLocator(Container);
			Type serviceType = typeof(IDummy);

			// Act
			var result = sut.GetService(serviceType);

			// Assert
			Assert.That(result, Is.InstanceOf<Dummy>());
		}

		[Test]
		public void GetInstance_Call_Success() {
			// Arrange
			var sut = new CompositionServiceLocator(Container);
			Type serviceType = typeof(IDummy);

			// Act
			var result = sut.GetInstance(serviceType);

			// Assert
			Assert.That(result, Is.InstanceOf<Dummy>());
		}

		[Test]
		public void GetInstanceWithKey_Call_Success() {
			// Arrange
			var sut = new CompositionServiceLocator(Container);
			Type serviceType = typeof(Dummy);
			string contractName = "dummy";

			// Act
			var result = sut.GetInstance(serviceType,contractName);

			// Assert
			Assert.That(result, Is.InstanceOf<Dummy>());
		}

		[Test]
		public void GetAllInstances_Call_Success() {
			// Arrange
			var sut = new CompositionServiceLocator(Container);
			Type serviceType = typeof(IDummy2);

			// Act
			var result = sut.GetAllInstances(serviceType);

			// Assert
			Assert.That(result.First(), Is.InstanceOf<Dummy2A>());
			Assert.That(result.Skip(1).First(), Is.InstanceOf<Dummy2B>());
		}

		[Test]
		public void GetInstanceGeneric_Call_Success() {
			// Arrange
			var sut = CreateCompositionServiceLocator();

			// Act
			var result = sut.GetInstance<IDummy>();

			// Assert
			Assert.That(result, Is.InstanceOf<Dummy>());
		}

		[Test]
		public void GetInstanceGenericWithKey_Call_Success() {
			// Arrange
			var sut = CreateCompositionServiceLocator();
			string key = "dummy";

			// Act
			var result = sut.GetInstance<Dummy>(key);

			// Assert
			Assert.That(result, Is.InstanceOf<Dummy>());
		}

		[Test]
		public void GetAllInstancesGeneric_Call_Success() {
			// Arrange
			var sut = CreateCompositionServiceLocator();

			// Act
			var result = sut.GetAllInstances<IDummy2>();

			// Assert
			Assert.That(result.First(), Is.InstanceOf<Dummy2A>());
			Assert.That(result.Skip(1).First(), Is.InstanceOf<Dummy2B>());
		}
	}

	public interface IDummy { }

	[Export(typeof(IDummy))]
	[Export("dummy")]
	public class Dummy : IDummy { }

	public interface IDummy2 { }


	[Export(typeof(IDummy2))]
	public class Dummy2A : IDummy2 { }

	[Export(typeof(IDummy2))]
	public class Dummy2B : IDummy2 { }

}
