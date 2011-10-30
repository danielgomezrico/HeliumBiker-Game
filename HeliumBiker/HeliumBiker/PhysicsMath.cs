using System.Linq;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using Microsoft.Xna.Framework;

namespace HeliumBiker
{
    internal class PhysicsMath
    {
        public static bool RectanglePoint(Rectangle rectangle, Vector2 point)
        {
            return ((point.X >= rectangle.Left && point.Y >= rectangle.Top) && (point.X <= rectangle.Right && point.Y <= rectangle.Bottom));
        }

        public static bool Circles(CircleShape circle1, Vector2 pos1, CircleShape circle2, Vector2 pos2)
        {
            float distance = Vector2.Distance(pos1, pos2);
            return (circle1.Radius + circle2.Radius >= distance);
        }

        internal static void ObjectsCollition(GameCtrl.GameEntities.PhysicsObject physicsObject, GameCtrl.GameEntities.PhysicsObject physicsObject_2)
        {
            for (int i = 0; i < physicsObject.Shape.Count(); i++)
            {
                for (int j = 0; j < physicsObject_2.Shape.Count(); j++)
                {
                    if (shapesCollide(physicsObject.Position, physicsObject.Shape[i], physicsObject_2.Position, physicsObject_2.Shape[j]))
                    {
                        physicsObject.collitionWith(physicsObject_2);
                        physicsObject_2.collitionWith(physicsObject);
                    }
                }
            }
        }

        private static bool shapesCollide(Vector2 pos1, GameCtrl.GameEntities.ShapeCtrl.Shape shape, Vector2 pos2, GameCtrl.GameEntities.ShapeCtrl.Shape shape2)
        {
            if (shape is CircleShape && shape2 is CircleShape)
            {
                if (Circles((CircleShape)shape, pos1 + shape.Disp, (CircleShape)shape2, pos2 + shape2.Disp))
                {
                    return true;
                }
            }
            return false;
        }
    }
}