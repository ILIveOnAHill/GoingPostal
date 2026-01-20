
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics;
public struct CollisionManifold(Vector2 normal, Vector2 depth)
{
    public Vector2 Normal = normal;   // direction of the collision
    public Vector2 Depth = depth;    // penetration depth
}
