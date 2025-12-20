
using System.Numerics;
using GoingPostal.Physics.ColliderShapes;

namespace GoingPostal.Physics.Body;

public class PlayerBody() : PhysicsBody
{
        public Vector2 Velocitiy;
        public bool OnGround;
        public float MaxRunSpeed = 180f;
        public float MaxFallSpeed = 900f;
        public float Gravity = 1700f;
        public float JumpVelocity = -1200f;

        public float GroundAcceleration = 1800f;
        public float GroundDeceleration = 5000f;
        public float AirAcceleration = 800f;
        public float AirDeceleration = 400f;

        // Jump helpers
        public float CoyoteTime = 0.1f;
        public float JumpBuffer = 0.1f;

        public float CoyoteTimer;
        public float JumpBufferTimer;

        public override ColliderShape shape => {s};
}