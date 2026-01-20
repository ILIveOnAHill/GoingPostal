using System;
using Microsoft.Xna.Framework;

namespace GoingPostal;
public class Camera()
{
    public Vector2 Position { get; private set; }
    public int CurrentRoomY { get; private set; }

    public void FollowPlayer(Vector2 playerPos, float roomHeight)
    {
        int newRoomY = (int)Math.Floor(playerPos.Y / roomHeight);

        if (newRoomY != CurrentRoomY)
        {
            CurrentRoomY = newRoomY;
        }

        Position = new Vector2(0, CurrentRoomY * roomHeight);
    }
}
