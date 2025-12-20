using System;
using System.Collections.Generic;
using GoingPostal.Entities;
using GoingPostal.Physics.Body;

namespace GoingPostal.Physics
{
    public class PhysicsEngine {
        
        public static void Update(List<Entity<PhysicsBody>> entities, Dictionary<(int, int), List<Entity<PhysicsBody>>> cGrid)
        {
            foreach (Entity<PhysicsBody> e in entities)
            {
                if (!e.IsActive || !e.IsVisible) continue;
                Step(e);
                foreach (var coords in e.cGridPositions.Keys)
                {
                    List<Entity<PhysicsBody>> possibleContacts = cGrid[coords];
                    if (possibleContacts.Count > 1 && e.cGridPositions[coords] == false)
                    {
                        foreach(Entity<PhysicsBody> contact in possibleContacts)
                        {
                            if (contact == e || contact.cGridPositions[coords] == true) continue;
                            
                        }
                        e.cGridPositions[coords] = true;
                    }
                }
            }
        }

        public static void Step<TBody>(Entity<TBody> aEntity) where TBody: PhysicsBody
        {
            if (aEntity is Player p)
            {
                PlayerBody Body = p.Body;

                float accel;
                float deaccel;
                
                if (Body.OnGround)
                {
                    Body.CoyoteTimer = Body.CoyoteTime;
                    accel = Body.GroundAcceleration;
                    deaccel = Body.GroundAcceleration;
                }
                else
                {
                    Body.CoyoteTimer -= dt;
                    accel = Body.AirAcceleration;
                    deaccel = Body.AirDeceleration;
                }

                if (p.WantsToJump) Body.JumpBufferTimer = Body.JumpBuffer;
                else Body.JumpBufferTimer -= dt;


                if (p.moveDirectionX != 0) Body.Velocity.X += moveDirectionX * accel * dt;
                else Body.Velocity.X = ApproachZero(Body.Velocity.X, deaccel * dt);

                if (Body.CoyoteTimer > 0 && Body.JumpBufferTimer > 0)
                {
                    Body.Velocity.Y = Body.JumpVelocity;
                    Body.CoyoteTimer = Body.JumpBufferTimer = 0;
                    Body.OnGround = false;
                }

                if(WantsToStopJumpEarly && Body.Velocity.Y < 0)
                {
                    Body.Velocity.Y *= 0.6f;
                }

                Body.Velocity.Y = Math.Min(Body.MaxFallSpeed, Body.Velocity.Y + (Body.Gravity * dt));

                Transform.Position += Body.Velocity * dt;
            }
        }

        public static float ApproachZero(float value, float amount)
        {
            if (value > 0)
                return Math.Max(0, value - amount);
            else if (value < 0)
                return Math.Min(0, value + amount);
            return 0;
        }
        
    }
}