using System;
using System.Collections.Generic;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using HeliumBiker.GameCtrl.GameEntities.ThrowableObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.PlayerParts
{
    internal class Ballon : PhysicsObject
    {
        public static Color[] colors =
        {   new Color(182,14,42),
            new Color(227,216,214),
            new Color(93,171,217),
            new Color(27,96,163),
            new Color(224,91,34),
            new Color(249,133,172),
            new Color(0,158,117),
            new Color(246,229,1)
        };

        private float ballonAcceleration = -0.1f;
        private Random random;
        private bool owned = false;
        private List<BallonListener> listeners;
        private Chord chord;

        public Ballon(Vector2 position, float angle, Color color, bool owned, Chord chord)
            : base(position, new Vector2(40, 40), angle, color, getLibTexuture(), Animation.getAnimation(getLibTexuture()), getShapes())
        {
            this.owned = owned;
            this.chord = chord;
            random = new Random();
            listeners = new List<BallonListener>();
        }

        public override void update(GameTime gameTime)
        {
            if (!owned)
            {
                Vector2 actualAcc = new Vector2(0, ballonAcceleration * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f));
                Acc += actualAcc;
            }
            chord.Position = this.Position;
            base.update(gameTime);
        }

        public override void collitionWith(PhysicsObject obj)
        {
            if (obj is ThrowableObjects.ThrowableObject && !(obj is Stone))
            {
                foreach (BallonListener listener in listeners)
                {
                    GameLib.getInstance().playCue(SoundE.pop).Play();
                    listener.pop(this);
                }
            }
        }

        private static Texture2D getLibTexuture()
        {
            return GameLib.getInstance().get(TextureE.ballon);
        }

        private static Shape[] getShapes()
        {
            return new Shape[]
            {
                new CircleShape(new Vector2(1,-3), 15f), // Rueda izquierda
            };
        }

        public void addListener(BallonListener listener)
        {
            listeners.Add(listener);
        }

        public bool Owned
        {
            get { return owned; }
            set { owned = value; }
        }

        public override void draw(SpriteBatch sb)
        {
            chord.draw(sb);
            base.draw(sb);
        }

        private static int counter = 0;

        public static List<Ballon> createBallons(Vector2 position, int count)
        {
            List<Ballon> ballons = new List<Ballon>();
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
                    createBallon(id, j, lenght, position, ballons);
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
                        createBallon(id, j, length, position, ballons);
                        ballonCounter++;
                    }
                    id++;
                }
            }
            return ballons;
        }

        private static void createBallon(int i, int j, int length, Vector2 position, List<Ballon> ballons)
        {
            float l = length * 25f;
            float percentage = (float)j / (float)length * l;
            Vector2 pos = new Vector2((position.X - l / 2) + percentage + 10, getY(length, j) + position.Y - 40 - (i * 20));
            float rotation = (float)Math.Atan2(pos.Y - position.Y, pos.X - position.X) + MathHelper.PiOver2;
            float degree = MathHelper.ToDegrees(rotation);
            float layerDepth = Math.Abs((position.X - pos.X) / l) / 10f + 0.1f;
            Random r = new Random(i * j * length);
            float chordLength = (position - pos).Length();
            Chord c = new Chord(pos, chordLength, rotation + MathHelper.ToRadians(180));
            Ballon b = new Ballon(pos, rotation, Ballon.colors[counter % Ballon.colors.Length], true, c);
            b.Max = 1f;
            counter++;
            b.LayerDepth = layerDepth;
            ballons.Add(b);
        }

        private static float getY(float length, int x)
        {
            return (-(x * x) / length);
        }
    }
}