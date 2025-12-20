using System;
using System.Collections.Generic;
using System.Numerics;
using GoingPostal.Entities;

namespace GoingPostal;

public class World(float w, float h, int cSize = 64)
{
    public readonly Dictionary<(int, int), List<Entity>> cGrid; // Spatial hash for storing objects
    private readonly int cellSize = cSize;
    private Vector2 worldSize = new(w, h);

    public void BuildCollisionGrid(List<Entity> entities)
    {
        foreach(Entity e in entities)
        {            
            e.cGridPositions = [];
            Console.WriteLine(e.Body.shape.Size.X);
            int cellEntityStartX = (int) Math.Floor(e.Transform.Position.X / this.cellSize);
            int cellEntityEndX = (int) Math.Floor((e.Transform.Position.X + e.Body.shape.Size.X) / this.cellSize);
            int cellEntityStartY = (int) Math.Floor(e.Transform.Position.Y / this.cellSize);
            int cellEntityEndY = (int) Math.Floor((e.Transform.Position.Y + e.Body.shape.Size.Y) / this.cellSize);

            for(int i = cellEntityStartX; i <= cellEntityEndX; i++)
            {
                for(int j = cellEntityStartY; j <= cellEntityEndY; j++)
                {
                    cGrid[(i, j)].Add(e);
                    e.cGridPositions[(i, j)] = false;
                }
            }

        }
    }
}