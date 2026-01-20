using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal;
public class TextureAtlas(Texture2D texture, Dictionary<string, TextureRegion> regions)
{
    public Texture2D Texture { get; } = texture;
    private readonly Dictionary<string, TextureRegion> _regions = regions;

    public TextureRegion GetRegion(string name)
        => _regions[name];
}
