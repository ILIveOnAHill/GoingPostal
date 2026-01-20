using Microsoft.Xna.Framework;

namespace GoingPostal.Entities
{
    public class Transform(Vector2 position = default, float theta = 0f, Vector2 scale = default)
    {
        public Vector2 Position = position;
        public float Rotation = theta;
        public Vector2 Scale = scale == default ? Vector2.One : scale;
    }
}
