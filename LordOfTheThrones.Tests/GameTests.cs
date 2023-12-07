using Xunit;
using Moq;
using Godot;
using System.Reflection.PortableExecutable;

namespace LordOfTheThrones.Tests
{
	//Due to Godot all of our methods need to be static for us to be able to do tests on them.
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

		//Checks so that when you win the gold gets Doubled.
        [Fact]
        public void DoubleOrNothing_ShouldDoubleGoldOnWin()
        {
            // Arrange
            int droppedCombatGold = 10;

            // Act
            int result = GambleManager.DoubleOrNothing(droppedCombatGold);

            // Assert
            Assert.Equal(droppedCombatGold * 2, result);
        }
		//Checks so that when you lose you get no gold.
        [Fact]
        public void DoubleOrNothing_ShouldReturnZeroOnLose()
        {
            // Arrange           
            int droppedCombatGold = 10;

            // Assume that the gamble function will always lose in this test
            // Mock the random behavior or use a deterministic approach for testing
            // Act
            int result = GambleManager.DoubleOrNothing(droppedCombatGold);

            // Assert
            Assert.Equal(0, result);
        }


    }
}