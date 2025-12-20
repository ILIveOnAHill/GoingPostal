using Microsoft.Xna.Framework.Input;
using GoingPostal.Core.Input;
using GoingPostal.Physics.ColliderShapes;
using GoingPostal.Physics.Body;
using System;
using System.Net.Http.Headers;

namespace GoingPostal.Entities
{
    public class Player(InputManager input) : Entity<PlayerBody>(true)
    {
        private readonly InputManager _input = input;
        public int MoveDirectionX {get;set;}
        public bool WantsToJump {get;set;}
        public bool WantsToStopJumpEarly {get;set;}

        public override void Update(float dt)
        {
            MoveDirectionX = 0;

            var k = _input.Keyboard;

            if (k.IsDown(Keys.A)) MoveDirectionX -= 1;
            if (k.IsDown(Keys.D)) MoveDirectionX += 1;

            WantsToJump = k.IsDown(Keys.Space);
            WantsToStopJumpEarly = k.IsUp(Keys.Space);

        }
        public override void SetCollider()
        {
            if (SpriteRenderer == null)
                throw new InvalidOperationException("SpriteRenderer is null");

            if (SpriteRenderer.Texture == null)
                throw new InvalidOperationException("Texture is null (call after LoadContent)");

            Body ??= new PlayerBody();

            Body.SetCollider(new BoxShape(SpriteRenderer.Texture.Width, SpriteRenderer.Texture.Height));

            Console.WriteLine(Body.Collider.Size.X);
        }
    }
}
