using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

using GoingPostal.Entities;
using GoingPostal.Core.Input;
using GoingPostal.Physics;
using System.Collections.Generic;


namespace GoingPostal;

public class Game1 : Base
{

    private InputManager _input;
    // private PhysicsEngine _pe;
    private List<EntityBase> _enities;
    private World _world;
    public Game1() : base("GoingPostal", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        _input = new();
        _enities = [];

        base.Initialize();
        _world = new(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

        Player player = new(_input);
        player.SetSprite(Content.Load<Texture2D>("images/mailman-temp"));
        player.Transform.Position = new(700, 0);
        player.Transform.Scale = new(0.5f, 0.5f);
        _enities.Add(player);

        Platform floor = new();
        floor.SetSprite(Content.Load<Texture2D>("images/brick-platfrom-variant1"));
        floor.Transform.Position = new(
            GraphicsDevice.Viewport.Width / 2,
            GraphicsDevice.Viewport.Height
        );
        
        floor.Transform.Scale = new(
            GraphicsDevice.Viewport.Width / floor.SpriteRenderer.Texture.Width, 
            1f
        );
        _enities.Add(floor);
    }

    protected override void LoadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _input.Update();
        _world.BuildCollisionGrid(_enities);
        foreach(var e in _enities) e.Update(dt);
        PhysicsEngine.Update(_enities, _world.cGrid, dt);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();

        foreach(var e in _enities)
        {
            if(e.IsVisible) e.Draw(SpriteBatch);
        }

        SpriteBatch.End();


        base.Draw(gameTime);
    }
}
