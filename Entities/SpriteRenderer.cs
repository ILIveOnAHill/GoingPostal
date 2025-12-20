using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal.Entities
{
    public class SpriteRenderer(Texture2D texture)
    {
        public Texture2D Texture = texture;
        public Color Color = Color.White;
        public Vector2 Origin = new(texture.Width / 2f, texture.Height / 2f);
        public float Layer = 0.5f;

        public void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            spriteBatch.Draw(
                Texture,
                transform.Position,
                null,
                Color,
                transform.Rotation,
                Origin,
                transform.Scale,
                SpriteEffects.None,
                Layer
            );
        }
    }
}
