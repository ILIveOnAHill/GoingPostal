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
        public int MoveDirectionX {get;set;}
        public bool WantsToJump {get;set;}
        public bool WantsToStopJumpEarly {get;set;}
        public float JumpCharge = 0f;

        public override void Update(float dt, World world = null)
        {

            _input.Update();
            
            if (Body.OnGround)
            {
                var k = _input.Keyboard;
                MoveDirectionX = 0;

                if (k.IsDown(Keys.Left)) MoveDirectionX -= 1;
                if (k.IsDown(Keys.Right)) MoveDirectionX += 1;

                WantsToJump = k.IsDown(Keys.Space);
            }

        }
        public override void SetCollider()
        {
            if (SpriteRenderer == null)
                throw new InvalidOperationException("SpriteRenderer is null");

            if (SpriteRenderer.Texture == null)
                throw new InvalidOperationException("Texture is null (call after LoadContent)");

            Body ??= new PlayerBody();

            Body.SetCollider(
                new AABBShape(
                    SpriteRenderer.Texture.Width, 
                    SpriteRenderer.Texture.Height,
                    Transform
                )
            );
        }
    }
}
