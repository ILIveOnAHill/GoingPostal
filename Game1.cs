using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

using GoingPostal.Scenes;
using System;

namespace GoingPostal;

public class Game1 : Base
{

    public Game1() : base("GoingPostal", 1980, 1080, false)
    {

    }

    protected override void Initialize()
    {

        base.Initialize();

        SceneManager.Switch(SceneType.MainMenu);
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        {
            GameDataManager.Save();
            Exit();
        }
        
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;


        SceneManager.SceneStack.Peek().Update(dt);


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SeaGreen);

        SpriteBatch.Begin();

        SceneManager.SceneStack.Peek().Draw();

        SpriteBatch.End();

        base.Draw(gameTime);
    }

}
