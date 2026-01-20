using System;
using System.Collections.Generic;
using GoingPostal.Physics.Body;
using GoingPostal.Physics.ColliderShapes;

namespace GoingPostal.Entities;
public class Bird() : Entity<BirdBody>(true)
{

    public int Direction {get;set;} = 1;

    public override void Update(float dt, World world)
    {
        if (Transform.Position.X < world.Size.X / 2)
        {
            Direction = 1;
        } else
        {
            Direction = -1;
        }
    }

    public override void SetCollider()
    {
        if (SpriteRenderer == null)
            throw new InvalidOperationException("SpriteRenderer is null");

        if (SpriteRenderer.Texture == null)
            throw new InvalidOperationException("Texture is null (call after LoadContent)");

        Body ??= new BirdBody();

        Body.SetCollider(
            new CircleShape(
                SpriteRenderer.Texture.Width * 0.5f, Transform.Scale
            )
        );
    }
}

public class Hawk() : Entity<BirdBody>(true)
{
    public int Direction {get;set;} = 1;

    public int Moving {get;set;} = 0;

    public Platform TargetPlatform {get;private set;}

    public int LevelID {get;set;}

    public override void Update(float dt, World world)
    {
        if (Moving == 0) ChooseRandomPlatform(world);

    }

    public override void SetCollider()
    {
        if (SpriteRenderer == null)
            throw new InvalidOperationException("SpriteRenderer is null");

        if (SpriteRenderer.Texture == null)
            throw new InvalidOperationException("Texture is null (call after LoadContent)");

        Body ??= new BirdBody();

        Body.SetCollider(
            new CircleShape(
                SpriteRenderer.Texture.Width * 0.5f, Transform.Scale
            )
        );
    }

    private void ChooseRandomPlatform(World world)
    {
        List<Platform> l = world.GetPlatforms(LevelID);
        TargetPlatform = l[Random.Shared.Next(l.Count)];
        Moving = 1;
    }

}