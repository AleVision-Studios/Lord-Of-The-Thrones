using Godot;
using System;

public partial class GambleManager : Node
{
    public static Random rnd = new Random();


    public static int DoubleOrNothing(int droppedCombatGold)
    {
        // Generate a random number (0 or 1) to represent win or lose
        int randomNumber = rnd.Next(2);

        // 50% chance of winning
        if (randomNumber == 0)
        {
            // You win, double the initial gold
            return droppedCombatGold * 2;
        }
        else
        {
            // You lose, return 0 gold
            return 0;
        }

    }
}

