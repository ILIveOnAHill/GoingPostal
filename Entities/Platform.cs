using System;
using GoingPostal.Physics.Body;
using GoingPostal.Physics.ColliderShapes;

namespace GoingPostal.Entities;

public class Platform(bool isActive = false) : Entity<PlatformBody>(isActive)
{

    public override void SetCollider()
    {
        Body ??= new();
        Body.SetCollider( 
            new AABBShape(
                SpriteRenderer.Texture.Width, 
                SpriteRenderer.Texture.Height,
                Transform
            )
        );

    }
}