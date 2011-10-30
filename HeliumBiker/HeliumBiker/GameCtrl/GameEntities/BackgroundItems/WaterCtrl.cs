using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.BackgroundItems
{
    class WaterCtrl
    {
        private static float yDisp = 500f;
        private List<Water> waters;
        private Water activeWater;
        private float worldDistance;

        public WaterCtrl(float worldDistance, float velHorizontal)
        {
            this.worldDistance = worldDistance;
            waters = new List<Water>();
            activeWater = new Water(new Vector2(0, yDisp));
            waters.Add(activeWater);
        }

        public void update(float currentDistance)
        {
            for (int i = 0; i < waters.Count - 1; i++)
            {
                if (waters.ElementAt(i).Position.X + waters.ElementAt(i).Size.X < currentDistance)
                {
                    waters.RemoveAt(i);
                }
            }
            if (activeWater.Position.X < currentDistance)
            {
                activeWater = new Water(new Vector2(activeWater.Position.X + activeWater.Size.X, yDisp));
                waters.Add(activeWater);
            }
        }

        public void draw(SpriteBatch sp)
        {
            foreach (Water mountain in waters)
            {
                mountain.draw(sp);
            }
        }
    }
}
