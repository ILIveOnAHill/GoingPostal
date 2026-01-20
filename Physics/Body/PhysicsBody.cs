
using GoingPostal.Physics.ColliderShapes;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.Body;
public abstract class PhysicsBody
{
    public Vector2 Velocity;

    public ColliderShape Collider { get; private set; }

    public void SetCollider(ColliderShape collider)
    {
        Collider = collider;
    }
}
