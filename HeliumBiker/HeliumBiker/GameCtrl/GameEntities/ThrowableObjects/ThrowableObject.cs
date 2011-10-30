using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.ThrowableObjects
{
    class ThrowableObject : PhysicsObject
    {
        public ThrowableObject(Vector2 position, Vector2 size, float angle, Color color, Texture2D texture, Animation animation, Shape[] shape) : 
            base(position, size, angle, color, texture, animation, shape)
        {

        }
    }
}
