
namespace GoingPostal.Physics.Body;

public class PlayerBody() : PhysicsBody
{
        public bool OnGround;
        public float MaxRunSpeed = 400f;
        public float MaxJumpVelocity = -1100f;
        public float MaxFallSpeed = 900f;
        public float Gravity = 1700f;
        public float MaxReboundX = 1700f;


        public float AirAcceleration = 700f;
        public float GroundAcceleration = 1800f;
        public float GroundDeceleration = 5000f;
        public float AirDeceleration = 400f;

        // Jump helpers
        public float JumpBuffer = 0.06f;

        public float JumpBufferTimer;
        
}