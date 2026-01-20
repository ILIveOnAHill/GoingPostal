using System;
using GoingPostal.Entities;
using GoingPostal.Physics.ColliderShapes;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics;

public class CollisionMath
{
    public static bool AABBToAABB(AABBShape c1, AABBShape c2, Transform t1, Transform t2, out CollisionManifold m)
    {
        m = default;

        // 1. Calculate half-extents
        Vector2 hA = c1.Size / 2f;
        Vector2 hB = c2.Size / 2f;

        // 2. Calculate distance between centers
        Vector2 delta = t2.Position - t1.Position;

        // 3. Calculate overlap on both axes
        // Overlap = (combined half-widths) - distance between centers
        float overlapX = hA.X + hB.X - MathF.Abs(delta.X);
        if (overlapX <= 0) return false;

        float overlapY = hA.Y + hB.Y - MathF.Abs(delta.Y);
        if (overlapY <= 0) return false;

        // 4. Find the axis of least penetration (that's our collision normal)
        if (overlapX < overlapY)
        {
            // Normal points from A to B. If delta.X is negative, B is to the left, so normal is left.
            float normalX = delta.X > 0 ? 1f : -1f;
            m = new CollisionManifold(new Vector2(normalX, 0f), new(overlapX, 0f));
        }
        else
        {
            float normalY = delta.Y > 0 ? 1f : -1f;
            m = new CollisionManifold(new Vector2(0f, normalY), new(0f, overlapY));
        }

        return true;
    }

    public static bool AABBToCircle(AABBShape box, CircleShape circle, Transform tBox, Transform tCircle, out CollisionManifold m)
    {
        m = default;

        Vector2 boxHalfSize = box.Size / 2f;
        Vector2 delta = tCircle.Position - tBox.Position;

        Vector2 closest = new(
            Math.Clamp(delta.X, -boxHalfSize.X, boxHalfSize.X),
            Math.Clamp(delta.Y, -boxHalfSize.Y, boxHalfSize.Y)
        );

        // 3. Distance from closest point to circle center
        Vector2 difference = delta - closest;
        float distSq = difference.LengthSquared();
        float radius = circle.Radius;

        // Early out
        if (distSq > radius * radius) return false;

        Vector2 normal;
        float penetration;

        if (distSq > 1e-6f) // Case: Circle center is OUTSIDE the box
        {
            float dist = MathF.Sqrt(distSq);
            // Normal points from Box to Circle
            normal = difference / dist; 
            penetration = radius - dist;
        }
        else // Case: Circle center is INSIDE the box
        {
            // Find the closest edge to push the circle out
            float xDist = boxHalfSize.X - MathF.Abs(delta.X);
            float yDist = boxHalfSize.Y - MathF.Abs(delta.Y);

            if (xDist < yDist)
            {
                normal = new Vector2(delta.X > 0 ? 1 : -1, 0);
                penetration = radius + xDist;
            }
            else
            {
                normal = new Vector2(0, delta.Y > 0 ? 1 : -1);
                penetration = radius + yDist;
            }
        }

        // Assign to manifold (Normal points A -> B, penetration is positive scalar)
        m = new CollisionManifold(normal, -normal * penetration);
        return true;
    }

    public static bool CircleToCircle(CircleShape c1, CircleShape c2, Transform t1, Transform t2, out CollisionManifold m)
    {
        m = default;

        // Normal pointing from 1 to 2
        Vector2 delta = t2.Position - t1.Position; 
        float distSq = delta.LengthSquared();
        float radiusSum = c1.Radius + c2.Radius;

        if (distSq >= radiusSum * radiusSum) return false;

        float dist = MathF.Sqrt(distSq);
        
        // If distance is zero, we pick an arbitrary up-vector
        Vector2 normal = dist > 1e-6f ? delta / dist : new Vector2(0f, 1f);
        float penetration = radiusSum - dist;

        // Consistency check: Does your engine want the vector or the components?
        // If your manifold takes (Vector2 normal, Vector2 separationVector):
        m = new CollisionManifold(normal, normal * penetration); 
        
        return true;
    }

    public static bool CircleToAABB(CircleShape c1, AABBShape c2, Transform t1, Transform t2, out CollisionManifold m)
    {
        bool hit = AABBToCircle(c2, c1, t2, t1, out m);
        if(hit) m.Normal = -m.Normal;
        return hit;
    }


}