using Xunit;
using Moq;
using Godot;

namespace LordOfTheThrones.Tests
{
	public class GameLogicTests
	{
		[Fact]
		public void SetHealth_Should_SetProgressBarValuesCorrectly()
		{
			// Arrange
			var combat = new Combat();
			var progressBarMock = new Mock<ProgressBar>();
			var labelMock = new Mock<Label>();

			progressBarMock.Setup(p => p.GetNode<Label>("Label")).Returns(labelMock.Object);

			// Act
			combat.SetHealth(progressBarMock.Object, 80, 100);

			// Assert
			progressBarMock.VerifySet(p => p.Value = 80, Times.Once);
			progressBarMock.VerifySet(p => p.MaxValue = 100, Times.Once);
			labelMock.VerifySet(l => l.Text = "HP: 80/100", Times.Once);
		}
	}
}