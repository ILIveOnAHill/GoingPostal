using System;
using GoingPostal.Entities;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;

public class AABBShape(float width, float height, Transform transform) : ColliderShape
{
    public float SpriteWidth { get; } = width;
    public float SpriteHeight { get; } = height;

    public override Vector2 Size => CalculateSize(transform);


    public override bool Intersects(ColliderShape c2, Transform t1, Transform t2, out CollisionManifold m)
    {
       return c2.IntersectsWithAABB(this, t2, t1, out m);
    }

    public override bool IntersectsWithAABB(AABBShape other, Transform t1, Transform t2, out CollisionManifold m)
    {
        return CollisionMath.AABBToAABB(this, other, t1, t2, out m);
    }

    public override bool IntersectsWithCircle(CircleShape other, Transform t1, Transform t2, out CollisionManifold m){
        return CollisionMath.AABBToCircle(this, other, t1, t2, out m);
    }
    public override bool IntersectsWithOBB(OBBShape other, Transform t1, Transform t2, out CollisionManifold m){
        
    }

    public Vector2[] GetCorners(Transform transform)
    {
        // 1. Calculate half-extents (from center)
        float halfW = SpriteWidth * transform.Scale.X / 2f;
        float halfH = SpriteHeight * transform.Scale.Y / 2f;

        // Convert degrees to radians
        float conversion = MathF.PI / 180f * transform.Rotation;

        // 2. Define the 4 corners in local space (relative to 0,0)
        Vector2[] corners =
        [
            new(-halfW, -halfH), // Top Left
            new(halfW, -halfH),  // Top Right
            new(halfW, halfH),   // Bottom Right
            new(-halfW, halfH)   // Bottom Left
        ];

        float cos = MathF.Cos(conversion);
        float sin = MathF.Sin(conversion);

        // 3. Rotate and Translate each corner
        for (int i = 0; i < corners.Length; i++)
        {
            float x = corners[i].X;
            float y = corners[i].Y;

            // Apply rotation formula
            float rotatedX = x * cos - y * sin;
            float rotatedY = x * sin + y * cos;

            // Add the world position (translation)
            corners[i] = new Vector2(rotatedX, rotatedY) + transform.Position;
        }

        return corners;
    }

    public Vector2 CalculateSize(Transform transform)
    {
        if (transform.Rotation == 0f) 
            return new Vector2(SpriteWidth, SpriteHeight) * transform.Scale;

        // Reuse your corner calculation logic
        Vector2[] corners = GetCorners(transform);

        float minX = float.MaxValue, minY = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue;

        foreach (var p in corners)
        {
            if (p.X < minX) minX = p.X;
            if (p.X > maxX) maxX = p.X;
            if (p.Y < minY) minY = p.Y;
            if (p.Y > maxY) maxY = p.Y;
        }

        return new(maxX-minX, maxY-minY);
    }
}
