using System;
using GoingPostal.Entities;
using GoingPostal.Physics.ColliderShapes;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics;

public class CollisionResolver
{
    public static void Resolve(EntityBase e, CollisionManifold m)
    {
        // positional correction
        e.Transform.Position -= m.Depth;
        // resolve velocity on the collision axis

        if (m.Normal.X != 0)
            e.BodyBase.Velocity.X = 0;

        if (m.Normal.Y != 0)
        {
            // resolve player collisions
            if (e is Player p && m.Normal.Y < 0) 
                p.Body.OnGround = true;
            e.BodyBase.Velocity.Y = 0;
        }
    }

    private static bool IntersectsAABB(EntityBase e1, EntityBase e2, out CollisionManifold m)
    {
        m = default;

        var aPos  = e1.Transform.Position;
        var aSize = e1.BodyBase.Collider.Size / 2f;
        var bPos  = e2.Transform.Position;
        var bSize = e2.BodyBase.Collider.Size / 2f;

        float aLeft   = aPos.X - aSize.X;
        float aRight  = aPos.X + aSize.X;
        float aTop    = aPos.Y - aSize.Y;
        float aBottom = aPos.Y + aSize.Y;
    
        float bLeft   = bPos.X - bSize.X;
        float bRight  = bPos.X + bSize.X;
        float bTop    = bPos.Y - bSize.Y;
        float bBottom = bPos.Y + bSize.Y;

        // early-out: no overlap
        if (aRight < bLeft || aLeft > bRight || aBottom < bTop || aTop > bBottom) 
            return false;

        float overlapX = MathF.Min(aRight, bRight) - MathF.Max(aLeft, bLeft);
        float overlapY = MathF.Min(aBottom, bBottom) - MathF.Max(aTop, bTop);       

        var aCenter = aPos;
        var bCenter = bPos;

        if (overlapX < overlapY)
        {
            float normalX = aCenter.X < bCenter.X ? -1f : 1f;
            m = new CollisionManifold(new Vector2(normalX, 0f), new Vector2(-normalX * overlapX, 0f));
        }
        else
        {
            float normalY = aCenter.Y < bCenter.Y ? -1f : 1f;
            m = new CollisionManifold(new Vector2(0f, normalY), new Vector2(0f, -normalY * overlapY));
        }

        return true;
    }

    private static bool IntersectsCircle(EntityBase e1, EntityBase e2, out CollisionManifold m)
    {
        m = default;

        var aCircle = (CircleShape)e1.BodyBase.Collider;
        var bCircle = (CircleShape)e2.BodyBase.Collider;
        var aCenter = e1.Transform.Position;
        var bCenter = e2.Transform.Position;

        Vector2 delta = aCenter - bCenter;
        float distSq = delta.LengthSquared();
        float radiusSum = aCircle.Radius + bCircle.Radius;
        if (distSq >= radiusSum * radiusSum) return false;

        float dist = MathF.Sqrt(distSq);
        Vector2 normal = dist > 1e-6f ? delta / dist : new Vector2(1f, 0f);
        float penetration = radiusSum - dist;
        Vector2 depth = -normal * penetration; // matches Resolve (which subtracts depth)

        m = new CollisionManifold(normal, depth);
        return true;
    }

    private static bool IntersectsAABBCircle(EntityBase e1, EntityBase e2, out CollisionManifold m)
    {
        m = default;

        // Determine which entity is the box and which is the circle so the method works for both orders.
        EntityBase boxEntity, circleEntity;

        if (e1.BodyBase.Collider is AABBShape)
        {
            boxEntity = e1;
            circleEntity = e2;
        }
        else
        {
            boxEntity = e2;
            circleEntity = e1;
        }

        var boxPos = boxEntity.Transform.Position;
        var boxSize = boxEntity.BodyBase.Collider.Size / 2f;
        float boxLeft = boxPos.X - boxSize.X;
        float boxRight = boxPos.X + boxSize.X;
        float boxTop = boxPos.Y - boxSize.Y;
        float boxBottom = boxPos.Y + boxSize.Y;

        var circle = (CircleShape)circleEntity.BodyBase.Collider;
        var circleCenter = circleEntity.Transform.Position;
        float radius = circle.Radius;

        // Closest point on the box to the circle center (clamp)
        float closestX = MathF.Max(boxLeft, MathF.Min(circleCenter.X, boxRight));
        float closestY = MathF.Max(boxTop, MathF.Min(circleCenter.Y, boxBottom));
        var closestPoint = new Vector2(closestX, closestY);

        Vector2 delta = closestPoint - circleCenter; // from circle center TO box (closest point)
        float distSq = delta.LengthSquared();

        // no overlap if distance from circle center to box > radius
        if (distSq > radius * radius)
            return false;

        Vector2 rawNormal;
        float penetration;

        if (distSq > 1e-6f)
        {
            float dist = MathF.Sqrt(distSq);
            rawNormal = delta / dist; // points from circle to box
            penetration = radius - dist;
        }
        else
        {
            // circle center is inside the box; push out along nearest box side
            float leftDist = circleCenter.X - boxLeft;
            float rightDist = boxRight - circleCenter.X;
            float topDist = circleCenter.Y - boxTop;
            float bottomDist = boxBottom - circleCenter.Y;

            float minDist = MathF.Min(MathF.Min(leftDist, rightDist), MathF.Min(topDist, bottomDist));

            if (minDist == leftDist) rawNormal = new Vector2(-1f, 0f);
            else if (minDist == rightDist) rawNormal = new Vector2(1f, 0f);
            else if (minDist == topDist) rawNormal = new Vector2(0f, -1f);
            else rawNormal = new Vector2(0f, 1f);

            
            // To separate the shapes when the circle is inside the box we need to move by radius + distance-to-edge
            penetration = radius + minDist;
        }

        // rawNormal currently points from circle -> box. We want m.Normal to point from e2 -> e1 (consistent with AABB/AABB and Circle/ Circle).
        Vector2 normal = (e1.BodyBase.Collider is AABBShape) ? rawNormal : -rawNormal;

        Vector2 depth = -normal * penetration;
        m = new CollisionManifold(normal, depth);
        return true;
    }

}
