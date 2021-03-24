using NUnit.Framework;
using PaperScissorsRockApp.Domain.Enum;
using RockPaperScissors.Domain;
using RockPaperScissors.Service;

namespace RockPaperScissors.Tests
{
    public class SelectionServiceTests
    {
        ISelectionService choiceService;

        [SetUp]
        public void SetUp()
        {
            choiceService = new SelectionService();
        }

        [TestCase(Choice.Paper, Choice.Paper, 0, 0)]
        [TestCase(Choice.Scissors, Choice.Scissors, 0, 0)]
        [TestCase(Choice.Rock, Choice.Rock, 0, 0)]
        [TestCase(Choice.Paper, Choice.Rock, 1, 0)]
        [TestCase(Choice.Paper, Choice.Scissors, 0, 1)]
        [TestCase(Choice.Scissors, Choice.Rock, 0, 1)]
        [TestCase(Choice.Scissors, Choice.Paper, 1, 0)]
        [TestCase(Choice.Rock, Choice.Scissors, 1, 0)]
        [TestCase(Choice.Rock, Choice.Paper, 0, 1)]
        public void TestCompareSelection_AgainstTestCases_ReturnsExpectedResults(Choice human, Choice computer, int humanResult, int computerResult)
        {
            // Act
            var expectedResult = new ScoreBoard(humanResult, computerResult);
            var actualResult = choiceService.CompareSelection(human, computer);

            // Assert
            Assert.AreEqual(actualResult.Player1, expectedResult.Player1);
            Assert.AreEqual(actualResult.Player2, expectedResult.Player2); ;
        }

        [Test]
        public void TestGetRandomSelection_ReturnsValidChoiceEnumValue()
        {
            // Act
            var result = choiceService.GetRandomSelection();

            // Assert
            Assert.That(result == Choice.Paper || result == Choice.Rock || result == Choice.Scissors);
        }
    }
}
