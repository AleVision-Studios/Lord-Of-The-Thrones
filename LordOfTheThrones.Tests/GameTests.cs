using Xunit;
using Moq;
using Godot;
using System.Reflection.PortableExecutable;

namespace LordOfTheThrones.Tests
{
	//Due to Godot all of our methods need to be static for us to be able to do tests on them.
	public class GameTests
	{
		[Theory]
		[InlineData(10)] 
		[InlineData(20)]
		[InlineData(30)]
		public void RandomNumber_Should_Return_Random_Number_Within_Range(int maxNumber)
		{
			// Arrange
			// Act
			int result = Combat.RandomNumber(maxNumber);

			// Assert
			Assert.InRange(result, 1, maxNumber);
		}

		[Theory]
		[InlineData(0)]    
		[InlineData(500)]  
		[InlineData(1000)] 
		public async Task RunMethodWithDelay_ShouldRunMethodAfterDelay(int delayTime)
		{
			// Arrange
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
		[Theory]
		[InlineData(10)] 
		[InlineData(15)] 
		[InlineData(0)]  
		public void DoubleOrNothing_ShouldDoubleGoldOnWin(int droppedCombatGold)
		{
			// Arrange
			var mockRandom = new Mock<Random>();
			mockRandom.Setup(r => r.Next(2)).Returns(0);
			GambleManager.rnd = mockRandom.Object;

			// Act
			int result = GambleManager.DoubleOrNothing(droppedCombatGold);

			// Assert
			Assert.Equal(droppedCombatGold * 2, result);
		}

		//Checks so that when you lose you get no gold.
		[Theory]
		[InlineData(10)] 
		[InlineData(20)] 
		[InlineData(0)]  
		public void DoubleOrNothing_ShouldReturnZeroOnLose(int droppedCombatGold)
		{
			// Arrange
			var mockRandom = new Mock<Random>();
			mockRandom.Setup(r => r.Next(2)).Returns(1);
			GambleManager.rnd = mockRandom.Object;

			// Act
			int result = GambleManager.DoubleOrNothing(droppedCombatGold);

			// Assert
			Assert.Equal(0, result);
		}

		[Theory]
		//[InlineData("ShortName")]
		//[InlineData("ThisIsAReasonablyLongName")]
		[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDE")] 
		[InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEF")]
        [InlineData("ABCDEFGHIJKLMNOPQASDASDCDDWRSTUVWXYZABCDEF")]
        [InlineData("ABCDEFGHIJKLMNOPQASDASDCDDASKLDJIOÖCHÖDWRSTUVWXYZABCDEF")]
        public void NameChecker_InvalidNames(string name)
		{
			// Arrange

			// Act
			bool result = Helpers.NameChecker(name); 

			// Assert
			if (name.Length >= 15)
			{
				Assert.False(result); 
			}

		}

		[Theory]
        [InlineData("ShortName")]
        [InlineData("FiftheenNumbers")]
		[InlineData("Douglas")]
        [InlineData("Jonatan")]
        [InlineData("Bo")]
        public void NameChecker_ValidNames(string name)
        {
            // Arrange

            // Act
            bool result = Helpers.NameChecker(name);

            // Assert
            if (name.Length <= 15)
            {
                Assert.True(result);
            }

        }

    }
}