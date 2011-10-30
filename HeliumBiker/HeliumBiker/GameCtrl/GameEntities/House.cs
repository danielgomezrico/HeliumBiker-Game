using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using HeliumBiker.GameCtrl.GameEntities.PlayerParts;

namespace HeliumBiker.GameCtrl.GameEntities
{
    class House : PhysicsObject
    {
        private List<Ballon> ballons;
        private bool collided = false;

        public House(Vector2 position)
            : base(position, new Vector2(512,512), 0f, Color.White, getLibTexuture(), Animation.getAnimation(getLibTexuture()), getShapes())
        {
            LayerDepth = 0.1f;
            ballons = new List<Ballon>();
            ballons.AddRange(Ballon.createBallons(position + new Vector2(-200, 69),14));
            ballons.AddRange(Ballon.createBallons(position + new Vector2(107, -147), 50));
        }

        private static Texture2D getLibTexuture()
        {
            return GameLib.getInstance().get(TextureE.house);
        }

        private static Shape[] getShapes()
        {
            return new Shape[]
            {
                new CircleShape(Vector2.Zero, 128f), // Rueda izquierda
            };
        }

        public override void draw(SpriteBatch sb)
        {
            foreach (Ballon b in ballons)
            {
                b.draw(sb);
            }
            base.draw(sb);
        }

        public override void update(GameTime gameTime)
        {
        }

        public override void collitionWith(PhysicsObject obj)
        {
            if (obj is Bike)
                collided = true;
        }

        public bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }
    }
}
