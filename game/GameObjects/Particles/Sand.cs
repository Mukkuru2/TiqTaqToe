using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects;

public class Sand : Powder
{
    public Sand(int layer = 0, string id = "") : base(layer, id)
    {
        color = Color.Yellow;
    }
    
}