using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace HeliumBiker.DeviceCtrl
{
    class StreamAnalizer : DeviceManager
    {
        private Vector2 position = Vector2.Zero;
        private Queue<string> qMessage;
        Thread threadAnalizer;
        Game1 game;
        public StreamAnalizer(Game1 game)
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

        public void addMessage(string message){
            qMessage.Enqueue(message);
        }

        public void analizeAcceleration(float x, float y)
        {
            
            if (x < -3)
            {
                HInput = InputE.left;
            }// Up
            else if (x > 1)
            {
                HInput = InputE.right;
            } //Down
            else
            {
                HInput = InputE.center;
            }

            if (y > 3)
            {
                VInput = InputE.up;
            }  //Right
            else if (y < -4)
            {
                VInput = InputE.down;
            } //Left  
            else
            {
                VInput = InputE.center;
            }
        }

        public void analizeSlingshotTouch(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
            if (qMessage != null && qMessage.Count > 0)
            {
                string qm = qMessage.Dequeue();
                string[] message = qm.Split(' ');
                Game1.testText = message[0];
                switch (message[0])
                {
                    case "A":
                        float accY = float.Parse(message[1]);
                        float accX = float.Parse(message[2]);
                        analizeAcceleration(accX, accY);
                        break;
                    case "S":
                        position = new Vector2(float.Parse(message[1]), float.Parse(message[2]));
                        FiringInput = InputE.notShooting;
                        break;
                    case "P":
                        position = new Vector2(float.Parse(message[1]), float.Parse(message[2]));
                        FiringInput = InputE.shooting;
                        break;
                    default:
                        break;
                }
            }
            base.Update(gameTime);
        }

        public override Microsoft.Xna.Framework.Vector2 getPointPosition()
        {
            //Game1.testText = position.X + "," + position.Y;
            Vector2 pos = new Vector2(position.X,-position.Y);
            pos.Normalize();
            return pos * 10;
        }
    }


}
