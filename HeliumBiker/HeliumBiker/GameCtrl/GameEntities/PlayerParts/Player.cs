using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeliumBiker.DeviceCtrl;
using HeliumBiker.GameCtrl.GameEntities.ThrowableObjects;

namespace HeliumBiker.GameCtrl.GameEntities.PlayerParts
{
    class Player : BallonListener
    {

        private World world;
        private Vector2 position;
        private Bike bike;
        private Hand hand;
        private List<Ballon> ballons;
        private int counter = 0;
        private DeviceManager dev;
        private static float force = 2f;
        private bool shooting = false;
        private List<Stone> stones;
        private static Vector2 disp = new Vector2(0, -5f);

        public Player(Vector2 position, DeviceManager dev, World world)
        {
            this.world = world;
            this.position = position;
            this.dev = dev;
            bike = new Bike(position, 0f, Color.White);
            bike.Max = 1f;
            ballons = new List<Ballon>();
            stones = new List<Stone>();
            hand = new Hand(bike.Position + disp);
            hand.LayerDepth = 0.5f;
            createBallons(14);
            bike.Animation.Stick = true;
        }

        public void update(GameTime gameTime, float currentGravity)
        {
            if (bike.Position.X < world.CurrentDistance)
            {
                bike.Acc = new Vector2(Math.Abs(bike.Acc.X), bike.Acc.Y);
                bike.Velocity = new Vector2(Math.Abs(bike.Velocity.X) + world.HorizontalVel, bike.Velocity.Y);
                foreach (Ballon b in Ballons)
                {
                    b.Acc = new Vector2(Math.Abs(b.Acc.X), b.Acc.Y);
                    b.Velocity = new Vector2(Math.Abs(b.Velocity.X) + world.HorizontalVel, b.Velocity.Y);
                }
            }
            else if (bike.Position.X > world.CurrentDistance + Game1.width)
            {
                bike.Acc = new Vector2(-Math.Abs(bike.Acc.X), bike.Acc.Y);
                // bike.Velocity = new Vector2(-Math.Abs(bike.Velocity.X), bike.Velocity.Y);
                bike.Velocity = Vector2.Zero;
                foreach (Ballon b in Ballons)
                {
                    b.Acc = new Vector2(-Math.Abs(b.Acc.X), b.Acc.Y);
                    // b.Velocity = new Vector2(-Math.Abs(b.Velocity.X), b.Velocity.Y);
                    b.Velocity = Vector2.Zero;
                }
            }

            if (bike.Position.Y - 100 < 0)
            {
                bike.Acc = new Vector2(bike.Acc.X, Math.Abs(bike.Acc.Y));
                bike.Velocity = new Vector2(bike.Velocity.X, 1f);
                foreach (Ballon b in Ballons)
                {
                    b.Acc = new Vector2(b.Acc.X, Math.Abs(b.Acc.Y));
                    // b.Velocity = new Vector2(-Math.Abs(b.Velocity.X), b.Velocity.Y);
                    b.Velocity = new Vector2(b.Velocity.X, 1f);
                }
            }

            if (ballons.Count > 0)
            {
                float currentForce = force * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f);
                Vector2 newAcc = new Vector2(0, currentGravity / (float)Ballons.Count);
                newAcc += manageInputs(currentForce, world.Objs);
                bike.Acc += newAcc;
                foreach (Ballon b in Ballons)
                {
                    b.Acc += newAcc;
                    b.update(gameTime);
                }
            }
            else
            {
                bike.applyGravity(currentGravity);
            }
            bike.update(gameTime);
            Position = bike.Position;
            checkStones();
        }

        private void checkStones()
        {
            for (int i = stones.Count - 1; i >= 0; i--)
            {
                Stone stone = stones[i];
                if (stone.Position.X > world.CurrentDistance + Game1.width || stone.Position.X < world.CurrentDistance || stone.Position.Y > world.Floor)
                {
                    world.Objs.Remove(stone);
                    stones.Remove(stone);
                }
            }
        }

        private Vector2 manageInputs(float currentForce, List<PhysicsObject> objs)
        {
            Vector2 newAcc = Vector2.Zero;
            bool angled = false;

            if (dev.VInput == InputE.down)
            {
                newAcc.Y += currentForce;
                bike.Angle = MathHelper.ToRadians(10f);
                angled = true;
                bike.Animation.Reverse = true;
                bike.Animation.Stick = false;
            }else
            if (dev.VInput == InputE.up)
            {
                newAcc.Y -= currentForce;
                bike.Angle = MathHelper.ToRadians(-10f);
                angled = true;
                bike.Animation.Reverse = false;
                bike.Animation.Stick = false;
            }
            if (dev.VInput == InputE.center)
            {
                bike.Animation.Stick = true;
            }

            if (dev.HInput == InputE.left)
            {
                newAcc.X -= currentForce;
                bike.Animation.Reverse = true;
                bike.Animation.Stick = false;
            }
            if (dev.HInput == InputE.right)
            {
                newAcc.X += currentForce;
                bike.Animation.Reverse = false;
                bike.Animation.Stick = false;
            }

            if (!angled)
            {
                bike.Angle = MathHelper.ToRadians(-0f);
            }

            if (dev.FiringInput == InputE.shooting)
            {

                hand.Position = bike.Position - dev.getPointPosition() + disp;
                if (hand.Position.X > bike.Position.X)
                {
                    hand.Position = new Vector2(bike.Position.X, hand.Position.Y) + disp;
                }
                shooting = true;
                hand.Visible = true;
            }
            else
            {
                if (shooting)
                {
                    Vector2 diff = bike.Position - hand.Position;
                    diff.Normalize();
                    diff *= 40f;
                    Stone stone = new Stone(Bike.Position, diff);
                    if (diff.X > 0) 
                    {
                        objs.Add(stone);
                        stones.Add(stone);
                    }
                    shooting = false;
                }
                hand.Visible = false;
            }
            return newAcc;
        }

        public void createBallons(int count)
        {
            int balloonCount = count / 2;
            int sum = 2;
            int index = 2;
            int maxRow = 0;
            int ballonCounter = 0;
            counter = 0;
            while (sum < balloonCount)
            {
                maxRow++;
                index++;
                sum += index;
            }
            int id = 0;
            for (int i = 0; i <= maxRow; i++)
            {
                int lenght = i + 2;
                for (int j = 0; j < lenght; j++)
                {
                    createBallon(id, j, lenght);
                    ballonCounter++;
                }
                id++;
            }
            for (int i = maxRow + 1; i <= maxRow * 2; i++)
            {
                if (ballonCounter < count)
                {
                    int length = maxRow * 2 - i + 2;
                    for (int j = 0; j < length; j++)
                    {
                        createBallon(id, j, length);
                        ballonCounter++;
                    }
                    id++;
                }
                else
                {

                }
            }   
        }

        private void createBallon(int i, int j, int length)
        {
            float l = length * 25f;
            float percentage = (float)j / (float)length * l;
            Vector2 pos = new Vector2( (Position.X - l/2) + percentage + 10, getY(length,j) + Position.Y - 40 - (i*25) );
            float rotation = (float) Math.Atan2(pos.Y - bike.Position.Y , pos.X - bike.Position.X) + MathHelper.PiOver2;
            float degree = MathHelper.ToDegrees(rotation);
            float layerDepth = Math.Abs((bike.Position.X - pos.X) / l)/10f + 0.1f;
            Random r = new Random(i * j * length);
            float chordLength = (Position - pos).Length();
            Chord c = new Chord(pos, chordLength, rotation + MathHelper.ToRadians(180));
            Ballon b = new Ballon(pos, rotation, Ballon.colors[counter % Ballon.colors.Length], true, c);
            b.Acc = bike.Acc;
            b.Velocity = bike.Velocity;
            b.addListener(this);
            b.Max = 1f;
            counter++;
            b.LayerDepth = layerDepth;
            Ballons.Add(b);
        }


        private float getY(float length, int x)
        {
            return ((x * x) / length);
        }

        public void draw(SpriteBatch sb)
        {
            bike.draw(sb);
            foreach (Ballon b in Ballons)
            {
                b.draw(sb);
            }
            hand.draw(sb);
        }

        public float BallonPos(float x)
        {
            return (float)Math.Pow((float)-x/0.5, (float)2) - 20;
        }

        internal Bike Bike
        {
            get { return bike; }
            set { bike = value; }
        }
        public List<Ballon> getBallonList()
        {
            return Ballons;
        }

        public Ballon getBallonAt(int at)
        {
            return Ballons.ElementAt(at);
        }
        internal List<Ballon> Ballons
        {
            get { return ballons; }
            set { ballons = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #region BallonListener Members

        public void pop(Ballon b)
        {
            ballons.Remove(b);
        }

        #endregion
    }
}
