
using System;
using System.Collections.Generic;
using GoingPostal.Physics.Body;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal.Entities
{

    public abstract class EntityBase(bool active)
    {
        public Transform Transform { get; protected set; } = new();
        public abstract PhysicsBody BodyBase { get; }
        public bool IsActive {get;set;} = active;
        public bool IsVisible {get;set;} = true;
        public virtual void Update(float dt) { }

        public Dictionary<(int, int), bool> GridPositions {get;set;}

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void SetSprite(Texture2D texture);

    }

    public abstract class Entity<TBody>(bool IsActive): EntityBase(IsActive)
    where TBody : PhysicsBody
    {        
        public SpriteRenderer SpriteRenderer { get; protected set; }
        public TBody Body {get; protected set;}
        public override PhysicsBody BodyBase => Body;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible && SpriteRenderer != null)
                SpriteRenderer.Draw(spriteBatch, Transform);
        }

        public override void SetSprite(Texture2D texture)
        {
            SpriteRenderer = new(texture);

            // automatically set the collider
            SetCollider();
        }

        public abstract void SetCollider();

    }
}
