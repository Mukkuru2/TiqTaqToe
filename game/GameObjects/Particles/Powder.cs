using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects;

public abstract class Powder : Element
{
    private bool isFalling = true;
    private const int MAX_FALLING_TRIES = 30;
    private int fallingTries = 0;
    
    public Powder(int layer = 0, string id = "") : base(layer, id)
    {
        Velocity = new Vector2(0, 0);
    }
    
    // The integer part represents the amount of cells it can range in a single tick.
    // The decimal part represents the chance to have an extra cell of movement.
    public float Viscosity { get; set; } = 0.1f;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        // If the cell is not falling and the cell below is not air, don't do anything
        if (!isFalling && !(Parent.Get(X, Y + 1) is Air)) return;
        
        // Update velocity. Should stay smaller than MAX_FALL_SPEED
        Velocity = Velocity with { Y = Math.Min(Velocity.Y + GRAVITY, MAX_FALL_SPEED) };
        
        // Establish horizontal movement range
        int rangeX = (int)Viscosity;
        if (Game.Random.NextDouble() < Viscosity - rangeX) rangeX++;
        

        int rangeY = (int)Velocity.Y;
        if (Game.Random.NextDouble() < Velocity.Y - rangeY) rangeY++;
        
        for (int _y = rangeY; _y > 0 ; _y--)
        {
            for (int _x = 0; _x <= rangeX * 2; _x++)
            {
                // Search from center to sides, increasing range
                int randomSign = GameEnvironment.Random.NextDouble() > 0.5 ? 1 : -1;
                int sign = _x % 2 == 0 ? 1 : -1;
                int dx = randomSign * sign * _x / 2;

                Vector2 cellTest = new Vector2(X + dx, Y + _y);
                
                // Check if all relevant cells are air. It checks if the cell below is air and if all the cells in between are air
                if (!AreCellsAllAir(X + dx, _y)) continue;
                
                Parent.Move(this, (int)cellTest.X, (int)cellTest.Y);
                return;
            }
        }
        
        // Cannot move :( set isFalling to false if fallingTries is greater than MAX_FALLING_TRIES
        if (fallingTries > MAX_FALLING_TRIES)
        {
            isFalling = false;
            fallingTries = 0;
            Velocity = new Vector2(0, 0);
        }
        else
        {
            fallingTries++;
        }
    }

    private bool AreCellsAllAir(int x, int y)
    {
        // Check if all the cells in between are air
        bool allAir = true;
        for (int i = 1; i <= y; i++)
        {
            if (Parent.Get(x, Y + i) is not Air)
            {
                allAir = false;
                break;
            }
        }
        
        return allAir;
    }
}