using GoingPostal.Entities;

namespace GoingPostal.Physics.ColliderShapes;

public class OBBShape(float width):ColliderShape
{
    public override bool Intersects(ColliderShape c2, Transform t1, Transform t2, out CollisionManifold m){
        
    }
    public override bool IntersectsWithAABB(AABBShape other, Transform t1, Transform t2, out CollisionManifold m){

    }
    public override bool IntersectsWithCircle(CircleShape other, Transform t1, Transform t2, out CollisionManifold m){

    }
    public override bool IntersectsWithOBB(OBBShape other, Transform t1, Transform t2, out CollisionManifold m){

    }
}