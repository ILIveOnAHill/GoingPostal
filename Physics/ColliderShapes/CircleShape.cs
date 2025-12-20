using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;


public class CircleShape(float radius) : ColliderShape
{
    public float Radius { get; } = radius;

    public override Vector2 Size => new(Radius * 2, Radius * 2);
}
