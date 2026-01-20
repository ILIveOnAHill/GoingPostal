using System.Collections.Generic;
using GoingPostal.Core.Input;
using GoingPostal.GUi;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal.GUI;
public class UIManager
{
    private readonly List<UIElement> _elements = [];

    public void Add(UIElement e) => _elements.Add(e);

    public void Update()
    {
        foreach (var e in _elements)
            e.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var e in _elements)
            e.Draw(spriteBatch);
    }
}
