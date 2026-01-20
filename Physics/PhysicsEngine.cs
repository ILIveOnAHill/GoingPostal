using System;
using System.Collections.Generic;
using GoingPostal.Entities;
using GoingPostal.Physics.Body;
using Microsoft.Xna.Framework;

namespace GoingPostal.Physics
{
    public class PhysicsEngine {

        
        public static void Update(List<EntityBase> entities, Dictionary<(int, int), List<EntityBase>> SpatialHash, float dt)
        {
            foreach (EntityBase e in entities)
            {
                if (!e.IsActive) continue;
                Step(e, dt);
                if (e is Player p) p.Body.OnGround = false; 
                foreach (var coords in e.GridPositions.Keys)
                {
                    List<EntityBase> possibleContacts = SpatialHash[coords];
                    if (possibleContacts.Count > 1 && e.GridPositions[coords] == false)
                    {
                        foreach(EntityBase c in possibleContacts)
                        {
                            if (c == e || c.GridPositions[coords]) continue;
                            if (e.BodyBase.Collider.Intersects(
                                c.BodyBase.Collider, 
                                e.Transform,
                                c.Transform, 
                                out CollisionManifold m))
                            {
                                CollisionResolver.Resolve(e, m);
                                
                            }
                        }
                        e.GridPositions[coords] = true;
                    }
                }
            }
        }

        public static void Step(EntityBase aEntity, float dt)
        {
            if (aEntity is Player p)
            {
                PlayerBody Body = p.Body;


                if (Body.OnGround)
                {
                    if (p.WantsToJump)
                    {
                        p.JumpCharge += 2f * dt;
                        Body.Velocity.X = 0;
                    }
                    else
                    {
                        Body.JumpBufferTimer -= dt; 
                        if (p.JumpCharge > 0) Body.JumpBufferTimer = Body.JumpBuffer;  
                        if (Body.JumpBufferTimer > 0)
                        {
                            Body.Velocity.Y = MathF.Max(Body.MaxJumpVelocity, Body.MaxJumpVelocity * p.JumpCharge);
                            p.JumpCharge = Body.JumpBufferTimer = 0;
                            Body.OnGround = false;
                            Body.Velocity.X = p.MoveDirectionX * Body.MaxRunSpeed;
                        } else
                        {
                            if (p.MoveDirectionX != 0)
                            {
                                Body.Velocity.X = MathF.Max(
                                    -Body.MaxRunSpeed, 
                                    MathF.Min(
                                        Body.MaxRunSpeed, 
                                        Body.Velocity.X + p.MoveDirectionX * Body.GroundAcceleration * dt
                                    )
                                );
                            } 
                            else Body.Velocity.X = ApproachZero(Body.Velocity.X, Body.GroundDeceleration * dt); 
                        }                    

  
                    }
                } else
                {
                    p.JumpCharge = 0;
                }
                
                Body.Velocity.Y = Math.Min(Body.MaxFallSpeed, Body.Velocity.Y + (Body.Gravity * dt));


            }
            else if (aEntity is Bird b)
            {
                BirdBody Body = b.Body;

                Body.Velocity.X = MathF.Min(Body.Velocity.X + b.Direction * Body.Acceleration, Body.MaxSpeed);


            }
            // else if (aEntity is Hawk h)
            // {
            //     Platform targetPlatform = h.TargetPlatform;
            //     float targetX = targetPlatform.Transform.Position.X;
            //     float targetY = targetPlatform.Transform.Position.Y - targetPlatform.Body.Collider.Size.Y / 2f - h.Body.Collider.Size.Y / 2f;

            //     var target = new Vector2(targetX, targetY);


            //     var toTarget = target - h.Transform.Position;

            //     if (CollisionResolver.Intersects(h, targetPlatform, out CollisionManifold m))
            //     {
            //         h.Body.Velocity = Vector2.Zero;
            //         h.Moving = 0;
            //         return;
            //     }

            //     h.Body.Velocity += Vector2.Normalize(toTarget) * h.Body.Acceleration;
                
                
            // }

            aEntity.Transform.Position += aEntity.BodyBase.Velocity * dt;
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