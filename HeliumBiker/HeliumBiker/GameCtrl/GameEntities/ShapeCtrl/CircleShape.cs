using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.ShapeCtrl
{
    class CircleShape : Shape
    {
        private float radius;

        public CircleShape(Vector2 disp, float radius) : base(disp)
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
