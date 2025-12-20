using Microsoft.Xna.Framework.Input;
using GoingPostal.Core.Input;
using GoingPostal.Physics.ColliderShapes;
using GoingPostal.Physics.Body;
using System;

namespace GoingPostal.Entities
{
    public class Player(InputManager input) : Entity<PlayerBody>(true)
    {
        private readonly InputManager _input = input;
        public int moveDirectionX;
        bool WantsToJump;
        bool WantsToStopJumpEarly;

        public override void Update(float dt)
        {
            moveDirectionX = 0;

            var k = _input.Keyboard;

            if (k.IsDown(Keys.A)) moveDirectionX -= 1;
            if (k.IsDown(Keys.D)) moveDirectionX += 1;

            WantsToJump = k.IsDown(Keys.Space);
            WantsToStopJumpEarly = k.IsUp(Keys.Space);

        }
        public override void SetCollider(bool isColliderTrigger)
        {
            Body ??= new PlayerBody();

            if (SpriteRenderer == null)
                throw new InvalidOperationException("SpriteRenderer is null");

            if (SpriteRenderer.Texture == null)
                throw new InvalidOperationException("Texture is null (call after LoadContent)");

            Body.shape = s;

            Console.WriteLine(Body.shape.Size.X);
        }
    }
}
