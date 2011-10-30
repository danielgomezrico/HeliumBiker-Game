using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities
{
    internal abstract class PhysicsObject : Entity
    {
        private Vector2 velocity;
        private Vector2 acc;
        private float maxAcc = 50f;
        private Shape[] shapes;

        public PhysicsObject(Vector2 position, Vector2 size, float angle, Color color, Texture2D texture, Animation animation, Shape[] shape) :
            base(position, size, new Vector2(texture.Height / 2, texture.Height / 2), angle, color, texture, animation)
        {
            this.shapes = shape;
            velocity = Vector2.Zero;
            acc = Vector2.Zero;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (acc.Length() > Max)
            {
                acc.Normalize();
                acc = acc * Max;
            }
            Velocity += acc * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f);
            Position += Velocity * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f);
            base.update(gameTime);

            acc = acc - acc / 9;
        }

        public virtual void collitionWith(PhysicsObject obj)
        {
        }

        internal void applyGravity(float currentGravity)
        {
            acc += new Vector2(0, currentGravity);
        }

        internal Vector2 Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public float Max
        {
            get { return maxAcc; }
            set { maxAcc = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Shape[] Shape
        {
            get { return shapes; }
            set { shapes = value; }
        }
    }
}