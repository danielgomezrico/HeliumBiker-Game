using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HeliumBiker.DeviceCtrl
{
    /// <summary>
    /// This class analize the messages getted by BluetoothServer
    ///
    /// At the moment it can analize Acceleration messages, Hold Sling Shot messages
    /// and Shot Sling Shot messages
    /// </summary>
    internal class BluetoothStreamAnalizer : DeviceManager
    {
        private Vector2 position = Vector2.Zero;
        private Queue<string> qMessage;
        //Thread threadAnalizer;

        Game1 game;

        public BluetoothStreamAnalizer(Game1 game)
            : base(game)
        {
            qMessage = new Queue<string>();
            this.game = game;
        }

        public void startAnalizer(Game1 game)
        {
            //threadAnalizer = new Thread(analizer);
            //threadAnalizer.Start();
            qMessage = new Queue<string>();
        }

        public void addMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                qMessage.Enqueue(message);
            }
        }

        /// <summary>
        /// Clear the messages
        /// </summary>
        public void clearMessages()
        {
            qMessage.Clear();
        }

        public void analizeAcceleration(float x, float y)
        {
            if (x < -3)
            {
                HInput = InputE.left;
            }
            else if (x > 1)
            {
                HInput = InputE.right;
            }
            else
            {
                HInput = InputE.center;
            }

            if (y > 3)
            {
                VInput = InputE.up;
            }
            else if (y < -4)
            {
                VInput = InputE.down;
            }
            else
            {
                VInput = InputE.center;
            }
        }

        private void analizeSlingShotPull(float x, float y)
        {
            position = new Vector2(x, y);
            FiringInput = InputE.shooting;
        }

        private void analizeSlingShot(float x, float y)
        {
            position = new Vector2(x, y);
            FiringInput = InputE.notShooting;
        }

        /// <summary>
        /// Check if there's any message to read
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (qMessage != null && qMessage.Count > 0)
            {
                string qm = qMessage.Dequeue();

                if (!string.IsNullOrEmpty(qm))
                {
                    string[] message = qm.Split(' ');

                    switch (message[0])
                    {
                        case "A":
                            float accY = float.Parse(message[1]);
                            float accX = float.Parse(message[2]);
                            analizeAcceleration(accX, accY);
                            break;
                        case "S":
                            analizeSlingShot(float.Parse(message[1]), float.Parse(message[2]));
                            break;
                        case "P":
                            analizeSlingShotPull(float.Parse(message[1]), float.Parse(message[2]));
                            break;
                        default:
                            break;
                    }
                }
                base.Update(gameTime);
            }
        }

        public override Microsoft.Xna.Framework.Vector2 getPointPosition()
        {
            //Game1.testText = position.X + "," + position.Y;
            Vector2 pos = new Vector2(position.X, -position.Y);
            pos.Normalize();
            return pos * 10;
        }
    }
}