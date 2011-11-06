using HeliumBiker.DeviceCtrl;
using HeliumBiker.DeviceCtrl.Devices;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.MenuCtrl
{
    internal class ConnectionScreen : Screen
    {
        private Button b;

        public ConnectionScreen(Game1 game, ScreenManager screenManager, DeviceManager dev)
            : base(game, screenManager, dev)
        {
            GameLib.getInstance().loadGUITextures();
            Texture2D bText = GameLib.getInstance().get(TextureE.connect);
            b = new Button(new Vector2(Game1.width / 2 - bText.Width / 2, Game1.height / 2 - bText.Height / 2), new Vector2(400, 400), TextureE.connect);
            b.Focus = true;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (DeviceConnectionManager.Connected)
            {
                // go to menu
                ScreenManager.startMenu();
            }
            else
            {
                ScreenManager.Initialize();
                //Go to connection menu
            }
            b.update(gameTime);
        }

        public override void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch sb = GameLib.getInstance().SpriteBatch;
            sb.Begin();
            sb.Draw(GameLib.getInstance().get(TextureE.menuScreen), Vector2.Zero, Color.White);
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