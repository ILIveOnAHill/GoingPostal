using GoingPostal.Entities;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;


public abstract class ColliderShape
{
    public abstract Vector2 Size { get;}

    public abstract bool Intersects(ColliderShape c2, Transform t1, Transform t2, out CollisionManifold m);
    public abstract bool IntersectsWithAABB(AABBShape other, Transform t1, Transform t2, out CollisionManifold m);
    public abstract bool IntersectsWithCircle(CircleShape other, Transform t1, Transform t2, out CollisionManifold m);
    public abstract bool IntersectsWithOBB(OBBShape other, Transform t1, Transform t2, out CollisionManifold m);
    
}