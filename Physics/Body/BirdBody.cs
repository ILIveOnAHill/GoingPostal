

namespace GoingPostal.Physics.Body;
public class BirdBody : PhysicsBody
{
    public bool OnGround;

    public bool Flying;

    public float Acceleration = 5f;

    public float MaxSpeed = 800f;

    public float ActionTimer = 0f; // every 10s a new action is performed
}