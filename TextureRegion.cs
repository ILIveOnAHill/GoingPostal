
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal;
public class TextureRegion(Texture2D texture, int x, int y, int width, int height)
{
    public Texture2D Texture { get; set; } = texture;
    public Rectangle SourceRectangle { get; set; } = new Rectangle(x, y, width, height);
}