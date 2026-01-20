using GoingPostal.Entities;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;


public class CircleShape(float radius, Vector2 Scale) : ColliderShape
{
    public float Radius { get; } = radius;

    public override Vector2 Size => new Vector2(Radius * 2, Radius * 2) * Scale;

    public override bool Intersects(ColliderShape c2, Transform t1, Transform t2, out CollisionManifold m)
    {
        return c2.Intersects(this, t2, t1, out m);
    }
    public override bool IntersectsWithAABB(AABBShape other, Transform t1, Transform t2, out CollisionManifold m)
    {
        return CollisionMath.CircleToAABB(this, other, t1, t2, out m);
        
    }
    public override bool IntersectsWithCircle(CircleShape other, Transform t1, Transform t2, out CollisionManifold m)
    {
        return CollisionMath.CircleToCircle(this, other, t1, t2, out m);
    }
    // public override bool IntersectsWithOBB(OBBShape other, Transform t1, Transform t2, out CollisionManifold m)
    // {
        
    // }
}
