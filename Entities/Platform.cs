using System;
using GoingPostal.Physics.Body;
using GoingPostal.Physics.ColliderShapes;

namespace GoingPostal.Entities;

public class Platform(bool isActive = false) : Entity<PlatformBody>(isActive)
{
    public new PlatformBody Body;
    public override void Update(float dt)
    {
        base.Update(dt);
    }
    public override void SetCollider()
    {
        Body ??= new();
        var s = new BoxShape(
                SpriteRenderer.Texture.Width, 
                SpriteRenderer.Texture.Height
                );
        Console.WriteLine(Body.Collider.Size.X);
    }
}