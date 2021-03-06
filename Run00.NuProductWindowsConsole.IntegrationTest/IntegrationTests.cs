﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Run00.MsTest;
using Run00.NuProduct;
using System.IO;

namespace Run00.NuProductWindowsConsole.IntegrationTest
{
	[DeploymentItem(@"..\..\Artifacts")]
	[TestClass, CategorizeByConventionClass(typeof(IntegrationTest))]
	public class IntegrationTest
	{
		[TestInitialize]
		public void Initialize()
		{
			if (Directory.Exists(Arguments.GetProductDir()) == false)
				return;

			//This is necessary because the Package Manager does not overwrite the installation folder
			//and the version of the test packages never changes.
			Directory.Delete(Arguments.GetProductDir(), true);
		}

		[TestMethod, CategorizeByConvention]
		public void WhenOnlyCodeBlockIsChanged_ShouldBeRefactor()
		{
			//Arrange;
			var args = GetArgs("RefactoringMethod");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Refactor, result.Justification.ChangeType);
			Assert.AreEqual("0.0.1.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenOnlyCommentsAreChanged_ShouldBeCosmetic()
		{
			//Arrange
			var args = GetArgs("ChangingComments");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Cosmetic, result.Justification.ChangeType);
			Assert.AreEqual("0.0.1.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenClassIsDeleted_ShouldBeBreaking()
		{
			//Arrange
			var args = GetArgs("DeletingClass");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Breaking, result.Justification.ChangeType);
			Assert.AreEqual("1.0.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenClassIsAdded_ShouldBeEnhancement()
		{
			//Arrange
			var args = GetArgs("AddingClass");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Enhancement, result.Justification.ChangeType);
			Assert.AreEqual("0.1.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenMethodIsAdded_ShouldBeEnhancement()
		{
			//Arrange
			var args = GetArgs("AddingMethod");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Enhancement, result.Justification.ChangeType);
			Assert.AreEqual("0.1.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenMethodIsModified_ShouldBeBreaking()
		{
			//Arrange
			var args = GetArgs("ChangingMethodSig");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Breaking, result.Justification.ChangeType);
			Assert.AreEqual("1.0.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenNamespaceIsChanged_ShouldBeBreaking()
		{
			//Arrange
			var args = GetArgs("ChangingNamespace");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Breaking, result.Justification.ChangeType);
			Assert.AreEqual("1.0.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenGenericIsChanged_ShouldBeBreaking()
		{
			//Arrange
			var args = GetArgs("ChangingGenericType");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Breaking, result.Justification.ChangeType);
			Assert.AreEqual("1.0.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenPrivateMethodIsAdded_ShouldBeRefactor()
		{
			//Arrange
			var args = GetArgs("AddingPrivateMethod");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Refactor, result.Justification.ChangeType);
			Assert.AreEqual("0.0.1.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenPropertyIsAdded_ShouldBeEnhancement()
		{
			//Arrange
			var args = GetArgs("AddingProperty");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Refactor, result.Justification.ChangeType);
			Assert.AreEqual("0.1.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenEnumIsAdded_ShouldBeEnhancement()
		{
			//Arrange
			var args = GetArgs("AddingEnum");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Refactor, result.Justification.ChangeType);
			Assert.AreEqual("0.1.0.0", result.Change.ToString());
		}

		[TestMethod, CategorizeByConvention]
		public void WhenNuProdHasCopyVersion_ShouldCopyVersionFromGivenPackage()
		{
			//Arrange
			var args = GetArgs("CopyVersion");

			//Act
			var result = Program.Execute(args);

			//Assert
			//Assert.AreEqual(ContractChangeType.Refactor, result.Justification.ChangeType);
			Assert.AreEqual("3.0.0", result.Change.ToString());
		}

		private string[] GetArgs(string targetVersion)
		{
			return new[] 
			{ 
				"--target", Path.Combine(Directory.GetCurrentDirectory(), "Test.Sample.0.0.0-" + targetVersion + ".nupkg"),
				"--host", Directory.GetCurrentDirectory(),
				"--out", Path.Combine(Arguments.GetProductDir(), "Output")
			};
		}

	}
}
