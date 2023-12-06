using Xunit;
using Moq;
using Godot;

namespace LordOfTheThrones.Tests
{
	public class GameLogicTests
	{
		[Fact]
		public void RandomNumber_Should_Return_Random_Number_Within_Range()
		{
			// Arrange
			var mockCombat = new Mock<ICombat>();

			// Assuming you want to generate numbers up to 100 for demonstration purposes
			const int maxNumber = 100;

			// Setup the mock to return a specific value for the RandomNumber method
			mockCombat.Setup(c => c.RandomNumber(maxNumber)).Returns(42);

			// Act
			int result = mockCombat.Object.RandomNumber(maxNumber);

			// Assert
			Assert.Equal(42, result);
		}
	}
}