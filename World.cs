using System;
using System.Collections.Generic;

using GoingPostal.Entities;
using Microsoft.Xna.Framework;

namespace GoingPostal;

public class World(int w, int h)
{
    public Vector2 Size {get;} = new(w, h);
    public List<EntityBase> Entities = [];
    public Dictionary<int, Level> Levels {get;set;} = [];
    public Dictionary<(int, int), List<EntityBase>> SpatialHash {get;set;} // Spatial hash for Broad phase collision
 


    public void BuildCollisionGrid(int cellSize = 64)
    {
        SpatialHash = [];
        foreach(EntityBase e in Entities)
        {            
            e.GridPositions = [];

            var entitySize = e.BodyBase.Collider.Size / 2f;

            int cellEntityStartX = (int) MathF.Floor((e.Transform.Position.X - entitySize.X) / cellSize);
            int cellEntityEndX = (int) MathF.Floor((e.Transform.Position.X + entitySize.X) / cellSize);
            int cellEntityStartY = (int) MathF.Floor((e.Transform.Position.Y - entitySize.Y) / cellSize);
            int cellEntityEndY = (int) MathF.Floor((e.Transform.Position.Y + entitySize.Y) / cellSize);

            for(int i = cellEntityStartX; i <= cellEntityEndX; i++)
            {
                for(int j = cellEntityStartY; j <= cellEntityEndY; j++)
                {
                    if (!SpatialHash.TryGetValue((i, j), out List<EntityBase> l))
                    {
                        l = [];
                        SpatialHash.Add((i, j), l);  
                    } 
                    l.Add(e);
                    e.GridPositions.Add((i, j), false);
                }
            }

        }
    }

    public Level GetLevel(int lid)
    {
        if(!Levels.TryGetValue(lid, out Level v))
        {
            v = new(lid);
            Levels.Add(lid, v);
        }

        return v;
    }

    public List<Platform> GetPlatforms(int lid)
    {
        var level = GetLevel(lid);

        List<Platform> platforms = []; 

        foreach (EntityBase e in level.Entities)
        {
            if (e is Platform p) platforms.Add(p);
        }

        return platforms;
    }

    public void Init()
    {
        var t = new Transform(new(700, 150), 0f, new(0.5f, 0.5f));
        Entities.Add(EntityFactory.CreatePlayer(t));
        t = new Transform(new(Size.X / 2, Size.Y - 50), 0f, new(10f, 1f));
        Entities.Add(EntityFactory.CreatePlatform(t));
        t = new Transform(new(700, 300));
        Entities.Add(EntityFactory.CreatePlatform(t));
        t = new Transform(new(1100, 500));
        Entities.Add(EntityFactory.CreatePlatform(t));
        t = new Transform(new(500, 1200));
        Entities.Add(EntityFactory.CreatePlatform(t));
        t = new Transform(new(700, 1500));
        Entities.Add(EntityFactory.CreatePlatform(t));
        t = new Transform(new(900, 1800));
        Entities.Add(EntityFactory.CreatePlatform(t));
    }
}