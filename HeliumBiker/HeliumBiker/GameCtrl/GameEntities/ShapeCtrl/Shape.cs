using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.ShapeCtrl
{
    internal abstract class Shape
    {
        private Vector2 disp;

        public Shape(Vector2 disp)
        {
            this.disp = disp;
        }

        public Vector2 Disp
        {
            get { return disp; }
            set { disp = value; }
        }
    }
}