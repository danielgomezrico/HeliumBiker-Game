using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.BackgroundItems
{
    internal class MountainCtrl
    {
        private static float yDisp = 150f;
        private List<FrontMountain> mountains;
        private FrontMountain activeMountain;
        private float worldDistance;

        public MountainCtrl(float worldDistance, float velHorizontal)
        {
            this.worldDistance = worldDistance;
            mountains = new List<FrontMountain>();
            activeMountain = new FrontMountain(new Vector2(0, yDisp));
            mountains.Add(activeMountain);
        }

        public void update(float currentDistance, float disp)
        {
            float vel = 5f * disp / 6f;
            foreach (FrontMountain mountain in mountains)
            {
                mountain.Position += new Vector2(vel, 0);
            }

            for (int i = 0; i < mountains.Count - 1; i++)
            {
                if (mountains.ElementAt(i).Position.X + mountains.ElementAt(i).Size.X < currentDistance)
                {
                    mountains.RemoveAt(i);
                }
            }
            if (activeMountain.Position.X < currentDistance)
            {
                activeMountain = new FrontMountain(new Vector2(activeMountain.Position.X + activeMountain.Size.X, yDisp));
                mountains.Add(activeMountain);
            }
        }

        public void draw(SpriteBatch sp)
        {
            foreach (FrontMountain mountain in mountains)
            {
                mountain.draw(sp);
            }
        }
    }
}