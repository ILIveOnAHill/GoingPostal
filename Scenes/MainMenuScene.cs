using System;
using System.Collections.Generic;
using GoingPostal.Core.Input;
using GoingPostal.GUi;
using GoingPostal.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace GoingPostal.Scenes;

public class MainMenuScene() : Scene
{
    private UIManager _ui;

    public InputManager _input;

    public TextureAtlas atlas;

    public override void Init()
    {

        _ui = new();
        _input = new();

        var t = Base.Content.Load<Texture2D>("images/normal_btn");

        var t2 = Base.Content.Load<Texture2D>("images/title");

        _ui.Add(new Button(t, Base.Content.Load<Texture2D>("images/hover_btn"))
        {
            Bounds = new Rectangle(Base.GraphicsDevice.Viewport.Width / 2, 600, t.Width, t.Height),
            OnClick = () => {
                SceneManager.Switch(SceneType.Gameplay);
                SceneManager.SceneStack.Peek().Update(0f);
            }
        });

        _ui.Add(new Button(t2, t2)
        {
            Bounds = new Rectangle(Base.GraphicsDevice.Viewport.Width / 2, 400, t.Width, t.Height),
            OnClick = () => {}
        });
    }

    public override void Load()
    {


        GameDataManager.GameData = GameDataManager.Load();
        

       foreach(string s in GameDataManager.GameData.Highscores)
        {
            Console.WriteLine(s);
        }

    }

    public override void Update(float dt)
    {
        _input.Update();

        if (_input.Keyboard.IsDown(Keys.Enter))
        {
            SceneManager.Switch(SceneType.Gameplay);
            SceneManager.SceneStack.Peek().Update(dt);
        }

        _ui.Update();
    }

    public override void Draw()
    {
        _ui.Draw(Base.SpriteBatch);
    }
}
