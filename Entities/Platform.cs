using System;
using GoingPostal.Physics.Body;
using GoingPostal.Physics.ColliderShapes;

namespace GoingPostal.Entities;

public class Platform(bool isActive = false) : Entity(isActive)
{
    public new PlatformBody Body;
    public override void Update(float dt)
    {
        base.Update(dt);
    }
    public override void SetCollider(bool isColliderTrigger)
    {
        Body ??= new();
        var s = new BoxShape(
                SpriteRenderer.Texture.Width, 
                SpriteRenderer.Texture.Height
                );
        Body.SetShape(s);
        Console.WriteLine(Body.shape.Size.X);
    }
}