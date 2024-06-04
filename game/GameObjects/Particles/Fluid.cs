using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects;

public abstract class Fluid : Element
{
    public Fluid(int layer = 0, string id = "") : base(layer, id)
    {
        Velocity = new Vector2(0, 0);
    }

    // The integer part represents the amount of cells it can range in a single tick.
    // The decimal part represents the chance to have an extra cell of movement.
    public float Viscosity { get; set; } = 3f;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // Update velocity
        Velocity = Velocity with { Y = Velocity.Y + GRAVITY };

        // Establish horizontal movement range
        int rangeX = (int)Viscosity;
        if (Game.Random.NextDouble() < Viscosity - rangeX) rangeX++;


        int rangeY = (int)Velocity.Y;
        if (Game.Random.NextDouble() < Velocity.Y - rangeY) rangeY++;

        MoveFluidDownItertive(rangeX, rangeY);
    }

    private void MoveFluidDownItertive(int rangeX, int rangeY)
    {
        // Check if the cell can go down directly. Then, check if it can go down further if it goes one cell left or right. Keep checking this for the range of movement
        int lowest = 0;

        for (int deltaY = rangeY; deltaY >= 0; deltaY--)
        {
            if (!AreCellsAllAir(X, Y, Y + deltaY)) continue;
            
            lowest = deltaY;
            
            Parent.Move(this, X, Y + deltaY);
            return;
            
        }
    }

    /// <summary>
    /// Check if all cells in a column are air.
    /// </summary>
    private bool AreCellsAllAir(int x, int y1, int y2)
    {
        // Check if all the cells in between are air
        bool allAir = true;
        for (int i = y1; i <= y2; i++)
        {
            if (Parent.Get(x, y1 + i) is not Air)
            {
                allAir = false;
                break;
            }
        }

        return allAir;
    }
}