
using System.Numerics;
using GoingPostal.Core.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace GoingPostal.Entities;

public class EntityFactory
{
    public static Player CreatePlayer(Transform transform)
    {
        var p = new Player(new InputManager())
        {
            Transform = transform
        };
        p.SetSprite(Base.Content.Load<Texture2D>("images/mailman-temp"));

        return p;
    }

    public static Platform CreatePlatform(Transform transform)
    {
        
        var p = new Platform()
        {
            Transform = transform
        };
        p.SetSprite(Base.Content.Load<Texture2D>("images/brick-platfrom-variant1"));

        return p;
    }
}