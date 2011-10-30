using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.BackgroundItems
{
    class Cloud : PhysicsObject
    {
        public Cloud(Vector2 position, Vector2 size, float angle, Color color, Animation animation, Shape[] shape)
            : base(position, size, angle, color, GameLib.getInstance().get(TextureE.cloud), animation, shape)
        {

        }
    }
}
