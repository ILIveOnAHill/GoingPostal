using System;
using GoingPostal.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoingPostal.GUi;
public abstract class UIElement
{
    public Rectangle Bounds;
    public bool Visible = true;

    public virtual void Update() { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
}


public class Button(Texture2D normal, Texture2D hover) : UIElement
{
    public Action OnClick;
    private bool _hovered;

    private readonly Texture2D _hoverT = hover;

    private readonly Texture2D _normalT = normal;

    public override void Update()
    {
        MouseState input = InputManager.GetMouseState();
        _hovered = Bounds.Contains(input.Position);

        if (_hovered && input.LeftButton == ButtonState.Pressed)
        {
            OnClick?.Invoke();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _hovered ? _hoverT : _normalT,
            Bounds.Location.ToVector2(),
            null,
            Color.White,
            0f,
            new(_hoverT.Width / 2f, _hoverT.Height / 2f),
            1f,
            SpriteEffects.None,
            0f
        );
    }
}
