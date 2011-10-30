using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.Enemies
{
    internal class FishGenerator
    {
        private World world;
        private float intensityTime;
        private float elapsedTime;
        private Random random;
        private List<Fish> fishes;

        public FishGenerator(World world, float intensityTime)
        {
            this.world = world;
            this.intensityTime = intensityTime;
            random = new Random();
            fishes = new List<Fish>();
        }

        public void update(GameTime gameTime)
        {
            if ((elapsedTime += gameTime.ElapsedGameTime.Milliseconds) > intensityTime)
            {
                Fish f = new Fish(new Vector2(random.Next(Game1.width / 3, Game1.width) + world.CurrentDistance, world.Floor));
                f.Acc = new Vector2(-2f, -11f + random.Next(-6, 1));
                world.Objs.Add(f);
                fishes.Add(f);
                elapsedTime = 0;
            }
            for (int i = fishes.Count - 1; i >= 0; i--)
            {
                if (fishes[i].Position.Y > world.Floor)
                {
                    world.Objs.Remove(fishes[i]);
                    fishes.Remove(fishes[i]);
                }
            }
        }
    }
}