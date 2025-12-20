using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GoingPostal.Entities;
using GoingPostal.Physics.Body;

namespace GoingPostal;

public class World(float w, float h, int cSize = 64)
{
    public readonly Dictionary<(int, int), List<EntityBase>> cGrid; // Spatial hash for storing objects
    private readonly int cellSize = cSize;
    private Vector2 worldSize = new(w, h);

    public void BuildCollisionGrid(List<EntityBase> entities)
    {
        foreach(Entity<PhysicsBody> e in entities.Cast<Entity<PhysicsBody>>())
        {            
            e.GridPositions = [];
            Console.WriteLine(e.Body.Collider.Size.X);
            int cellEntityStartX = (int) Math.Floor(e.Transform.Position.X / this.cellSize);
            int cellEntityEndX = (int) Math.Floor((e.Transform.Position.X + e.Body.Collider.Size.X) / this.cellSize);
            int cellEntityStartY = (int) Math.Floor(e.Transform.Position.Y / this.cellSize);
            int cellEntityEndY = (int) Math.Floor((e.Transform.Position.Y + e.Body.Collider.Size.Y) / this.cellSize);

            for(int i = cellEntityStartX; i <= cellEntityEndX; i++)
            {
                for(int j = cellEntityStartY; j <= cellEntityEndY; j++)
                {
                    cGrid[(i, j)].Add(e);
                    e.GridPositions[(i, j)] = false;
                }
            }

        }
    }
}