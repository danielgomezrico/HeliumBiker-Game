using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.ShapeCtrl
{
    internal class CircleShape : Shape
    {
        private float radius;

        public CircleShape(Vector2 disp, float radius)
            : base(disp)
        {
            this.radius = radius;
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }
    }
}