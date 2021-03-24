using Moq;
using NUnit.Framework;
using PaperScissorsRockApp.Domain.Enum;
using RockPaperScissors.Domain;
using RockPaperScissors.Interface;
using RockPaperScissors.Service;
using RockPaperScissors.Service.Interface;

namespace RockPaperScissors.Tests
{
    public class GameTests
    {

        Game game;
        Mock<ISelectionService> mockSelectionService;
        Mock<ISelectionValidator> mockSelectionValidator;
        Mock<IConsoleWrapperService> mockConsoleWrapperService;
        int defaultNoOfGamesToWin = 2;

        [SetUp]
        public void SetUp()
        {
            mockSelectionService = new Mock<ISelectionService>();
            mockSelectionValidator = new Mock<ISelectionValidator>();
            mockConsoleWrapperService = new Mock<IConsoleWrapperService>();

            game = new Game(mockSelectionValidator.Object, mockSelectionService.Object, mockConsoleWrapperService.Object);
        }

        [Test]
        public void TestPlay_OptionsWrittenToConsole_ExpectAllOptionsWritten()
        {
            // Arrange
            mockConsoleWrapperService.Setup(c => c.ReadLine()).Returns("x");

            // Act
            game.Play(defaultNoOfGamesToWin);

            // Assert
            mockConsoleWrapperService.Verify(c => c.WriteLine("1 Paper"), Times.Once);
            mockConsoleWrapperService.Verify(c => c.WriteLine("2 Scissors"), Times.Once);
            mockConsoleWrapperService.Verify(c => c.WriteLine("3 Rock"), Times.Once);
        }


        [Test]
        public void TestPlay_PromptsWrittenToConsole_ExpectPromptsWritten()
        {
            // Arrange
            mockConsoleWrapperService.SetupSequence(c => c.ReadLine()).Returns("1").Returns("x");

            // Act
            game.Play(defaultNoOfGamesToWin);

            // Assert
            mockConsoleWrapperService.Verify(c => c.WriteLine("Select the number next the option you want:"), Times.Once);
            mockConsoleWrapperService.Verify(c => c.WriteLine("Enter your next selection."), Times.AtLeastOnce);
        }


        [Test]
        public void TestPlay_WhenAnInvalidKeyIsEnteredErrorMessageWrittenToConsole_ExpectCorrectMessageWritten()
        {
            // Arrange
            mockConsoleWrapperService.SetupSequence(c => c.ReadLine()).Returns("w").Returns("x");

            // Acti
            game.Play(defaultNoOfGamesToWin);

            // Asert
            mockConsoleWrapperService.Verify(c => c.WriteLine("You have entered an invalid selection, please try again."), Times.Once);
        }

        [Test]
        public void TestPlay_WhenUserWinsTwoRounds_ExpectCorrectMessagesWritten()
        {
            // Arrage
            mockConsoleWrapperService.SetupSequence(c => c.ReadLine()).Returns("1").Returns("1").Returns("x");

            // Act
            mockSelectionService.Setup(c => c.GetRandomSelection()).Returns(Choice.Rock);
            mockSelectionValidator.Setup(c => c.IsValid(It.IsAny<string>())).Returns(true);
            mockSelectionService.Setup(c => c.CompareSelection(It.IsAny<Choice>(), It.IsAny<Choice>())).Returns(new ScoreBoard(1, 0));

            // Assert
            game.Play(defaultNoOfGamesToWin);


            mockConsoleWrapperService.Verify(c => c.WriteLine("You won that round."), Times.AtLeastOnce);
            mockConsoleWrapperService.Verify(c => c.WriteLine("You are the overall winner!"), Times.Once);
        }

        [Test]
        public void TestPlay_WhenComputerWinsTwoRounds_DisplayCorrectMessagesWritten()
        {
            // Arrange
            mockConsoleWrapperService.SetupSequence(c => c.ReadLine()).Returns("1").Returns("1").Returns("x");
            mockSelectionService.Setup(c => c.GetRandomSelection()).Returns(Choice.Scissors);
            mockSelectionValidator.Setup(c => c.IsValid(It.IsAny<string>())).Returns(true);
            mockSelectionService.Setup(c => c.CompareSelection(It.IsAny<Choice>(), It.IsAny<Choice>())).Returns(new ScoreBoard(0, 1));

            // Act
            game.Play(defaultNoOfGamesToWin);

            // Assert
            mockConsoleWrapperService.Verify(c => c.WriteLine("The Computer won that round."), Times.AtLeastOnce);
            mockConsoleWrapperService.Verify(c => c.WriteLine("The Computer is the overall winner!"), Times.Once);
        }

        [Test]
        public void TestPlay_WhenThereIsADrawnRound_DisplayCorrectMessageWritten()
        {
            // Arrange
            mockConsoleWrapperService.SetupSequence(c => c.ReadLine()).Returns("1").Returns("x");
            mockSelectionService.Setup(c => c.GetRandomSelection()).Returns(Choice.Scissors);
            mockSelectionValidator.Setup(c => c.IsValid(It.IsAny<string>())).Returns(true);
            mockSelectionService.Setup(c => c.CompareSelection(It.IsAny<Choice>(), It.IsAny<Choice>())).Returns(new ScoreBoard(0, 0));

            // Act
            game.Play(defaultNoOfGamesToWin);

            // Assert
            mockConsoleWrapperService.Verify(c => c.WriteLine("That was a draw."), Times.AtLeastOnce);
        }
    }
}
