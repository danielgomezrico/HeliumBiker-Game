using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.ThrowableObjects
{
    internal class ThrowableObject : PhysicsObject
    {
        public ThrowableObject(Vector2 position, Vector2 size, float angle, Color color, Texture2D texture, Animation animation, Shape[] shape) :
            base(position, size, angle, color, texture, animation, shape)
        {
        }
    }
}