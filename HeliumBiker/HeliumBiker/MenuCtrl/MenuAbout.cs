using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework.Graphics;
using HeliumBiker.DeviceCtrl;
using Microsoft.Xna.Framework;

namespace HeliumBiker.MenuCtrl
{
    class MenuAbout : Screen
    {
        Texture2D bg;
        Button b;
        private InputE enter = InputE.notShooting;

        public MenuAbout(Game1 game, ScreenManager screenManager, DeviceManager dev)
            : base(game, screenManager, dev)
        {
            bg = GameLib.getInstance().get(TextureE.aboutScreen);
            b = new Button(new Vector2(Game1.width - 400, Game1.height - 300), new Vector2(300, 300), TextureE.back);
            b.Focus = true;
        }
         

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            b.update(gameTime);
            if (DeviceManager.FiringInput == InputE.notShooting && enter == InputE.shooting)
            {
                ScreenManager.goBackMenu();
            }
            enter = DeviceManager.FiringInput;
        }

        public override void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch sb = GameLib.getInstance().SpriteBatch;
            sb.Begin();
            sb.Draw(bg, Vector2.Zero, Color.White);
            b.draw(sb);
            sb.End();
        }

        public override void transitionOut(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public override void transitionIn(Microsoft.Xna.Framework.GameTime gameTime)
        {
            setState(State.active);
        }
    }
}
