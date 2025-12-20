
using System;
using System.Collections.Generic;
using GoingPostal.Physics.Body;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal.Entities
{

    public abstract class EntityBase{}

    public abstract class Entity<TBody>(bool IsActive): EntityBase
    where TBody : PhysicsBody
    {
        public Transform Transform { get; protected set; } = new();
        public SpriteRenderer SpriteRenderer { get; protected set; }
        public TBody Body {get; protected set;}

        public Dictionary<(int, int), bool> cGridPositions;
        public bool IsActive = IsActive;
        public bool IsVisible = true;

        public virtual void Update(float dt) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible && SpriteRenderer != null)
                SpriteRenderer.Draw(spriteBatch, Transform);
        }

        public virtual void SetSprite(Texture2D texture, bool isColliderTrigger)
        {
            SpriteRenderer = new(texture);

            // automatically set the collider
            SetCollider(isColliderTrigger);
        }

        public abstract void SetCollider(bool isColliderTrigger = false);

    }
}
