using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;


public class CircleShape(float r) : ColliderShape
{
    public override Vector2 Size { get => new(2*r,2*r); protected set => SetSize(2*r,2*r); }

    private static Vector2 SetSize(float w, float h)
    {
        return new(w, h);
    }

}