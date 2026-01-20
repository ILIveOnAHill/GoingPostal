
using System.Collections.Generic;
using GoingPostal.Physics.Body;
using Microsoft.Xna.Framework.Graphics;

namespace GoingPostal.Entities
{

    public abstract class EntityBase(bool active)
    {
        public Transform Transform { get; set; } = new();
        public abstract PhysicsBody BodyBase { get; }
        public bool IsActive {get;set;} = active;
        public bool IsVisible {get;set;} = true;

        public virtual void Update(float dt, World world = null) { }

        public Dictionary<(int, int), bool> GridPositions {get;set;}

        public abstract void Draw(SpriteBatch spriteBatch, float Height);

        public abstract SpriteRenderer GetSpriteRenderer();
        public abstract void SetSprite(Texture2D texture);

    }

    public abstract class Entity<TBody>(bool IsActive): EntityBase(IsActive)
    where TBody : PhysicsBody
    {        
        public SpriteRenderer SpriteRenderer { get; protected set; }
        public TBody Body {get; protected set;}
        public override PhysicsBody BodyBase => Body;

        public override void Draw(SpriteBatch spriteBatch, float Height)
        {
            if (IsVisible && SpriteRenderer != null)
                SpriteRenderer.Draw(spriteBatch, Transform, Height);
        }

        public override void SetSprite(Texture2D texture)
        {
            SpriteRenderer = new(texture);

            // automatically set the collider
            SetCollider();
        }

        public override SpriteRenderer GetSpriteRenderer()
        {
            return SpriteRenderer;
        }

        public abstract void SetCollider();

    }
}
