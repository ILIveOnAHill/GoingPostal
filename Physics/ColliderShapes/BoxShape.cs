
using System.Net.WebSockets;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics.ColliderShapes;

public class BoxShape(float w, float h) : ColliderShape
{
    public override Vector2 Size { get => new(w,h); protected set => SetSize(w, h); }

    private static Vector2 SetSize(float w, float h)
    {
        return new(w, h);
    }

}