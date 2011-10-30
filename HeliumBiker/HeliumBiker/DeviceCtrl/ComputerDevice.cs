using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HeliumBiker.DeviceCtrl
{
    class ComputerDevice : DeviceManager
    {
        private Vector2 position;

        public ComputerDevice(Game1 game)
            : base(game)
        {
            position = Vector2.Zero;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState stateK = Keyboard.GetState();
            MouseState stateM = Mouse.GetState();
            if (stateK.IsKeyUp(Keys.W) && stateK.IsKeyUp(Keys.S))
            {
                VInput = InputE.center;
            }
            else
            {
                if (stateK.IsKeyDown(Keys.W) && stateK.IsKeyUp(Keys.S))
                {
                    VInput = InputE.up;
                }
                if (stateK.IsKeyDown(Keys.S) && stateK.IsKeyUp(Keys.W))
                {
                    VInput = InputE.down;
                }
            }

            if (stateK.IsKeyUp(Keys.A) && stateK.IsKeyUp(Keys.D))
            {
                HInput = InputE.center;
            }
            else
            {
                if (stateK.IsKeyDown(Keys.A) && stateK.IsKeyUp(Keys.D))
                {
                    HInput = InputE.left;
                }
                if (stateK.IsKeyDown(Keys.D) && stateK.IsKeyUp(Keys.A))
                {
                    HInput = InputE.right;
                }
            }
            if (stateM.LeftButton == ButtonState.Pressed)
            {
                FiringInput = InputE.shooting;
            }
            else
            {
                FiringInput = InputE.notShooting;
            }

            position = new Vector2(stateM.X, stateM.Y);

            base.Update(gameTime);
        }

        public override Microsoft.Xna.Framework.Vector2 getPointPosition()
        {
            Vector2 pos = position - new Vector2(Game1.width / 2, Game1.height / 2);
            pos.Normalize();
            return pos * 10;
        }
    }
}
