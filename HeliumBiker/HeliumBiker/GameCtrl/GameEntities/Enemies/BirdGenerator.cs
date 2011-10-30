using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HeliumBiker.GameCtrl.GameEntities.Enemies
{
    internal class BirdGenerator
    {
        private World world;
        private float intensityTime;
        private float elapsedTime;
        private float horVel;
        private Random random;
        private List<Bird> birds;

        public BirdGenerator(World world, float intensityTime, float horVel)
        {
            this.world = world;
            this.intensityTime = intensityTime;
            this.horVel = horVel;
            random = new Random();
            birds = new List<Bird>();
        }

        public void update(GameTime gameTime)
        {
            if ((elapsedTime += gameTime.ElapsedGameTime.Milliseconds) > intensityTime)
            {
                Bird b = new Bird(new Vector2(1280 + world.CurrentDistance, random.Next(0, (int)(Game1.height * 0.6f))));
                world.Objs.Add(b);
                birds.Add(b);
                elapsedTime = 0;
            }
            for (int i = birds.Count - 1; i >= 0; i--)
            {
                if (birds[i].Position.X < world.CurrentDistance - birds[i].Size.X || birds[i].Position.Y > world.Floor)
                {
                    world.Objs.Remove(birds[i]);
                    birds.Remove(birds[i]);
                }
            }
        }
    }
}