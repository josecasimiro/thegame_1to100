using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheGame.Domain.NumberGenerator;

namespace TheGame.Domain.Test.NumberGenerator
{
  [TestClass]
  public class RandomNumberGeneratorTests
  {
    [TestMethod]
    public void Given_ValidStartEnd_When_GeneratedNumbersAreExhausted_Then_NoRepeatedNumbersAreReturned()
    {
      // Arrange
      const int start = 0;
      const int end = 1 * 1000 * 1000;

      // Act
      var numberGenerator = new RandomNumberGenerator(start, end);
      var generatedNumbers = numberGenerator.ToList();

      // Assert
      var distinctNumbers = generatedNumbers.Distinct();

      Assert.AreEqual(generatedNumbers.Count, distinctNumbers.Count(), "Duplicate Numbers Returned");
    }

    [TestMethod]
    public void Given_NegativeStartAndNegativeEnd_When_GeneratedNumbersAreExhausted_Then_NoRepeatedNumbersAreReturned()
    {
      // Arrange
      const int start = -1 * 1000 * 1000;
      const int end = 0;

      // Act
      var numberGenerator = new RandomNumberGenerator(start, end);
      var generatedNumbers = numberGenerator.ToList();

      // Assert
      var distinctNumbers = generatedNumbers.Distinct();

      Assert.AreEqual(generatedNumbers.Count, distinctNumbers.Count(), "Duplicate Numbers Returned");
    }

    [TestMethod]
    public void Given_NegativeStartPositiveEnd_When_GeneratedNumbersAreExhausted_Then_NoRepeatedNumbersAreReturned()
    {
      // Arrange
      const int start = -1 * 1000 * 1000;
      const int end = 1 * 1000 * 1000;

      // Act
      var numberGenerator = new RandomNumberGenerator(start, end);
      var generatedNumbers = numberGenerator.ToList();

      // Assert
      var distinctNumbers = generatedNumbers.Distinct();

      Assert.AreEqual(generatedNumbers.Count, distinctNumbers.Count(), "Duplicate Numbers Returned");
    }

    [TestMethod]
    public void Given_StartGreaterThanEnd_When_GenerationIsRequested_Then_NoNumbersReturned()
    {
      // Arrange
      const int start = 2;
      const int end = 1;

      // Act
      var numberGenerator = new RandomNumberGenerator(start, end);
      var generatedNumbers = numberGenerator.ToList();

      // Assert
      Assert.AreEqual(generatedNumbers.Count, 0, "Some numbers generated");
    }
  }
}
