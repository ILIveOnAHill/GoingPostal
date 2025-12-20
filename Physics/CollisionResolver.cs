using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Numerics;
using GoingPostal.Entities;
using GoingPostal.Physics.ColliderShapes;

namespace GoingPostal.Physics;

public class CollisionResolver
{
    public static void Resolve(Entity e, CollisionManifold m)
    {
        // positional correction
        e.Transform.Position -= m.Depth;
        

        // resolve velocity on the collision axis
        if (m.Normal.X != 0)
            e.Body.Velocity.X = 0;

        if (m.Normal.Y != 0)
        {
            // resolve player collisions
            if (e is Player p && p.Body.Velocity.Y > 0) 
                p.Body.OnGround = true;
            e.Body.Velocity.Y = 0;
        }
    }

    public static bool Intersects(Entity e1, Entity e2, out CollisionManifold m)
    {
        m = default;

        if (e1.Body.shape is BoxShape && e2.Body.shape is BoxShape)
        {
             if(!IntersectsAABB(e1, e2, out m)) return false;
        }

        return true;
    }

    public static bool IntersectsAABB(Entity e1, Entity e2, out CollisionManifold m)
    {
        m = default;

        var aRect = new Rectangle(
            (int)e1.Transform.Position.X, 
            (int)e1.Transform.Position.Y, 
            (int)e1.Body.shape.Size.X, 
            (int)e1.Body.shape.Size.Y
        );
        var bRect = new Rectangle(
            (int)e2.Transform.Position.X, 
            (int)e2.Transform.Position.Y, 
            (int)e2.Body.shape.Size.X, 
            (int)e2.Body.shape.Size.Y
        );

        if(!aRect.IntersectsWith(bRect)) return false;

        Console.WriteLine("Collision Detected");

        float overlapX = Math.Min(aRect.Right, bRect.Right) - Math.Max(aRect.Left, bRect.Left);
        float overlapY = Math.Min(aRect.Bottom, bRect.Bottom) - Math.Max(aRect.Top, bRect.Top);

        if (overlapX < overlapY)
        {
            float normalX = Center(aRect).X < Center(bRect).X ? -1 : 1;
            m = new CollisionManifold(new Vector2(normalX, 0), new Vector2(overlapX, 0));
        }
        else
        {
            float normalY = Center(aRect).Y < Center(bRect).Y ? -1 : 1;
            m = new CollisionManifold(new Vector2(0, normalY), new Vector2(0, overlapY));
        }

        return true;
    }

    private static Point Center(Rectangle rect)
    {
        return new (rect.Left + rect.Width / 2, rect.Top + rect.Width/2);
    }
}
