using System;
using System.Diagnostics;
using Blok3Game.Engine.GameObjects;
using Blok3Game.Engine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects;

public abstract class Element : GameObject
{
    public override Chunk Parent
    {
        get => parent as Chunk;
    }

    public int X => (int)Position.X / World.CELL_SIZE;
    public int Y => (int)Position.Y / World.CELL_SIZE;

    public const float GRAVITY = 0.05f;
    public const float MAX_FALL_SPEED = 3f;
    
    public Element(int layer = 1, string id = "") : base(layer, id)
    {
    }
    
    // Color of cell
    public Color color = Color.Black;
    
    // What cell does the cell leave behind when it moves or expires
    public Func<Element> leaveBehind = () => new Air();

    private static Color RandomColor() {
        int r = Game.Random.Next(0, 255);
        int g = Game.Random.Next(0, 255);
        int b = Game.Random.Next(0, 255);
        return new Color(r, g, b);
    }

    public override void Update(GameTime gameTime)
    {
        Parent.SetActive();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Rectangle rectangle = new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, World.CELL_SIZE, World.CELL_SIZE);
        DrawingHelper.DrawRectangle(rectangle, spriteBatch, color);
    }
}