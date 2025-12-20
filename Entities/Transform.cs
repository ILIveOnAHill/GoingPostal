using Microsoft.Xna.Framework;

namespace GoingPostal.Entities
{
    public class Transform
    {
        public Vector2 Position;
        public float Rotation = 0f;
        public Vector2 Scale = Vector2.One;

        public Transform() {}

        public Transform(Vector2 position)
        {
            Position = position;
        }
    }
}
