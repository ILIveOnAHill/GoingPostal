using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;


public abstract class ColliderShape
{
    public abstract Vector2 Size { get; protected set;}
}