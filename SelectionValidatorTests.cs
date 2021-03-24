using NUnit.Framework;
using RockPaperScissors.Service.Interface;
using PaperScissorsRockApp.Service;

namespace RockPaperScissors.Tests
{
    public class SelectionValidatorTests
    {
        ISelectionValidator validation;

        [SetUp]
        public void Setup()
        {
            validation = new SelectionValidator();
        }

        [TestCase("1",true)]
        [TestCase("2", true)]
        [TestCase("3", true)]
        [TestCase("4", false)]
        [TestCase("0", false)]
        [TestCase("-1", false)]
        [TestCase("a", false)]
        [TestCase("ab", false)]
        [TestCase("@", false)]
        public void TestIsValid_AgainstTestCases_ReturnsExpectedResult(string choice, bool expetedResult)
        {
            var result = validation.IsValid(choice);

            //Assert
            Assert.AreEqual(result, expetedResult);
        }
    }
}