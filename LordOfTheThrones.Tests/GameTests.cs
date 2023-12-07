using Xunit;
using Moq;
using Godot;

namespace LordOfTheThrones.Tests
{
	public class GameTests
	{
		[Fact]
		public void RandomNumber_Should_Return_Random_Number_Within_Range()
		{
			// Arrange
			const int maxNumber = 100;

			// Act
			int result = Combat.RandomNumber(maxNumber);

			// Assert
			Assert.InRange(result, 1, maxNumber);
		}

		[Fact]
		public async Task RunMethodWithDelay_ShouldRunMethodAfterDelay()
		{
			// Arrange
			int delayTime = 1000; // 1 second delay
			bool methodExecuted = false;

			Action testMethod = () =>
			{
				methodExecuted = true;
			};

			// Act
			await Combat.RunMethodWithDelay(delayTime, testMethod);

			// Assert
			Assert.True(methodExecuted);
		}


	}
}