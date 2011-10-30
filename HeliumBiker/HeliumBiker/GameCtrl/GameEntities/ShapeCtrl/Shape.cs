using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.ShapeCtrl
{
    abstract class Shape
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
