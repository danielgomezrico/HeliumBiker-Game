using System;
using HeliumBiker.GameCtrl.GameEntities.PlayerParts;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using HeliumBiker.GameCtrl.GameEntities.ThrowableObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.Enemies
{
    internal class Fish : ThrowableObject
    {
        private bool alive = true;

        public Fish(Vector2 position)
            : base(position, new Vector2(30f, 30f), 0.0f, Color.White, getTexture(), Animation.getAnimation(getTexture()), getShapes())
        {
            LayerDepth = 0.1f;
            Animation.AnimationRate = 20f;
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.fish);
        }

        private static Shape[] getShapes()
        {
            return new Shape[]
            {
                new CircleShape(Vector2.Zero,8f)
            };
        }

        public override void collitionWith(PhysicsObject obj)
        {
            if (Alive && (obj is Stone || obj is Bike))
            {
                World.points += 100;
                Alive = false;
                Velocity *= new Vector2(-1, 0f);
                Animation.Stick = true;
            }
        }

        public override void update(GameTime gameTime)
        {
            Angle = (float)Math.Atan2(Velocity.X, -Velocity.Y) + MathHelper.ToRadians(90f);
            base.update(gameTime);
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
    }
}