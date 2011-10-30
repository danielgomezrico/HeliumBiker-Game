using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using HeliumBiker.GameCtrl.GameEntities.ThrowableObjects;
using HeliumBiker.GameCtrl.GameEntities.PlayerParts;

namespace HeliumBiker.GameCtrl.GameEntities.Enemies
{
    class Bird : ThrowableObject
    {
        bool alive = true;

        public Bird(Vector2 position)
            : base(position,new Vector2(60,60), 0.0f ,Color.White, getTexture(), Animation.getAnimation(getTexture()),getShapes())
        {
            LayerDepth = 0.11f;
            Animation.AnimationRate = 20f;
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.bird);
        }

        private static Shape [] getShapes()
        {
            return new Shape[]
            {
                new CircleShape(new Vector2(-24,2),4),
                new CircleShape(new Vector2(-11,-1),8)
            };
        }

        public override void collitionWith(PhysicsObject obj)
        {
            if (alive && ( obj is Stone || obj is Bike) )
            {
                World.points += 300;
                alive = false;
                Velocity = new Vector2(Velocity.X / 2, 10f);
                Acc += new Vector2(0, 1f);
                Animation.Stick = true;
            }
        }

        public override void update(GameTime gameTime)
        {
            if (Alive)
            {
                Acc += new Vector2(-.01f, -World.gravity * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f));
            }
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
