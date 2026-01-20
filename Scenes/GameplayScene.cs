using System;
using System.Collections.Generic;
using GoingPostal.Core.Input;
using GoingPostal.Entities;
using GoingPostal.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace GoingPostal.Scenes;

public class GameplayScene : Scene
{
    private InputManager _input;
    private World _world;
    private Camera _camera;

    private float _playTime;



    public override void Init()
    {

        _world = new(Base.GraphicsDevice.Viewport.Width, Base.GraphicsDevice.Viewport.Height);
        _input = new InputManager();
        _camera = new();

        _world.Init();
        _playTime = 0f;

        // Bird flappy = new();
        // flappy.SetSprite(Content.Load<Texture2D>("images/bird"));
        // flappy.Transform.Position = new(0, 600);

        // Hawk hawk = new();
        // hawk.SetSprite(Content.Load<Texture2D>("images/bird2"));
        // hawk.Transform.Position = new(700, 331);
    }

    public override void Load()
    {
        // var _content = Game1.GetContentManager();

        

        // _font = _content.Load<SpriteFont>("fonts/comic-sans-ms");
    }

    public override void Update(float dt)
    {
        _playTime += dt;

        if (_input.Keyboard.IsDown(Keys.Escape))
        {
            GameDataManager.GameData.Highscores.Add(_playTime.ToString());
            SceneManager.Switch(SceneType.MainMenu);
        }
        
        _input.Update();
        
        _world.BuildCollisionGrid();

        _world.Levels = [];

        foreach(var e in _world.Entities) {

            e.Update(dt, _world);
            // rearrange which level Entities are in based on position
            int lid =(int)MathF.Floor(e.Transform.Position.Y / _world.Size.Y);
            // check if entity is partially in another level
            int lid2 =(int)MathF.Floor(e.Transform.Position.Y + e.GetSpriteRenderer().Texture.Height / _world.Size.Y);

            
            _world.GetLevel(lid).Entities.Add(e);
            if(lid != lid2) _world.GetLevel(lid2).Entities.Add(e);

            if (e is Player p) _camera.FollowPlayer(p.Transform.Position, _world.Size.Y);
            if (e is Hawk h)
            {
                h.LevelID = lid;
            }
        }

        PhysicsEngine.Update(_world.Entities, _world.SpatialHash, dt);
    }

    public override void Draw()
    {
        foreach(var e in _world.Levels[_camera.CurrentRoomY].Entities)
        {
            if(e.IsVisible) e.Draw(Base.SpriteBatch, _world.Size.Y);
        }

    }
}