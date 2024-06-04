using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects;

public class Water : Fluid
{
    public Water(int layer = 0, string id = "") : base(layer, id)
    {
        color = Color.Blue;
    }
}