using Blok3Game.Engine.Helpers;
using Blok3Game.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects;

public class Air : Element
{
    public Air(int layer = 0, string id = "") : base(layer, id)
    {
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (GameState.DEBUG_LEVEL >= GameState.DebugLevel.DEBUG)
        {
            DrawingHelper.DrawRectangle(
                new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, World.CELL_SIZE, World.CELL_SIZE),
                spriteBatch, Color.White);
        }
    }
}