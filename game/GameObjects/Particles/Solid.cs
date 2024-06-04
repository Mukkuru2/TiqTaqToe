using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects;

public abstract class Solid : Element
{
    
    public Solid(int layer = 0, string id = "") : base(layer, id)
    {
    }
    
    public override void Update(GameTime gameTime)
    {
    }
    
}