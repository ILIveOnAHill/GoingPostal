
using System.Net.WebSockets;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;

public class BoxShape(float width, float height) : ColliderShape
{
    public float Width { get; } = width;
    public float Height { get; } = height;

    public override Vector2 Size => new(Width, Height);
}
