using System;
using System.Collections.Generic;
using GoingPostal.Entities;
using GoingPostal.Physics.Body;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics
{
    public class PhysicsEngine {
        
        public static void Update(List<EntityBase> entities, Dictionary<(int, int), List<EntityBase>> cGrid, float dt)
        {
            foreach (Entity<PhysicsBody> e in entities)
            {
                if (!e.IsActive || !e.IsVisible) continue;
                Step(e, dt);
                foreach (var coords in e.GridPositions.Keys)
                {
                    List<EntityBase> possibleContacts = cGrid[coords];
                    if (possibleContacts.Count > 1 && e.GridPositions[coords] == false)
                    {
                        foreach(EntityBase contact in possibleContacts)
                        {
                            if (contact == e || contact.GridPositions[coords] == true) continue;
                            
                        }
                        e.GridPositions[coords] = true;
                    }
                }
            }
        }

        public static void Step<TBody>(Entity<TBody> aEntity, float dt) where TBody: PhysicsBody
        {
            Vector2 step = new();

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


                if (p.MoveDirectionX != 0) Body.Velocity.X += p.MoveDirectionX * accel * dt;
                else Body.Velocity.X = ApproachZero(Body.Velocity.X, deaccel * dt);

                if (Body.CoyoteTimer > 0 && Body.JumpBufferTimer > 0)
                {
                    Body.Velocity.Y = Body.JumpVelocity;
                    Body.CoyoteTimer = Body.JumpBufferTimer = 0;
                    Body.OnGround = false;
                }

                if(p.WantsToStopJumpEarly && Body.Velocity.Y < 0)
                {
                    Body.Velocity.Y *= 0.6f;
                }

                Body.Velocity.Y = Math.Min(Body.MaxFallSpeed, Body.Velocity.Y + (Body.Gravity * dt));

                step = Body.Velocity * dt;
            }

            aEntity.Transform.Position += step;
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