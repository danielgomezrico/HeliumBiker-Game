using HeliumBiker.DeviceCtrl;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.MenuCtrl
{
    internal class Menu : Screen
    {
        public enum buttonsE
        {
            start = 1,
            about = 0,
            exit = 2
        }

        private Button[] buttons;
        private int selected = 1;
        private InputE horizontal = InputE.center;
        private InputE enter = InputE.notShooting;
        private Texture2D logo;
        public static Cue sound;

        public Menu(Game1 game, ScreenManager screenManager, DeviceManager dev)
            : base(game, screenManager, dev)
        {
            logo = GameLib.getInstance().get(TextureE.logo);
            buttons = new Button[]
            {
                new Button(new Vector2(10, Game1.height - 270), new Vector2(400, 400),TextureE.about),
                new Button(new Vector2(Game1.width/2 - 500/2 , Game1.height - 330), new Vector2(500, 500),TextureE.start),
                new Button(new Vector2(Game1.width-390, Game1.height - 270), new Vector2(400, 400),TextureE.exit)
            };
            buttons[1].LayerDepth = 0f;
            buttons[selected].Focus = true;
            foreach (Button b in buttons)
            {
                b.addAlpha((byte)0);
            }
            sound = GameLib.getInstance().playCue(SoundE.once);
            sound.Play();
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.update(gameTime);
            }

            if (DeviceManager.HInput == InputE.left && (horizontal == InputE.center || horizontal == InputE.right))
            {
                buttons[selected].Focus = false;
                selected = selected - 1;
                if (selected < 0)
                {
                    selected = buttons.Length - 1;
                }
                buttons[selected].Focus = true;
            }
            if (DeviceManager.HInput == InputE.right && (horizontal == InputE.center || horizontal == InputE.left))
            {
                buttons[selected].Focus = false;

                selected = (selected + 1) % buttons.Length;
                buttons[selected].Focus = true;
            }

            if (DeviceManager.FiringInput == InputE.notShooting && enter == InputE.shooting)
            {
                gotoButton();
            }
            enter = DeviceManager.FiringInput;
            horizontal = DeviceManager.HInput;
        }

        private void gotoButton()
        {
            switch (selected)
            {
                case (int)buttonsE.start:
                    base.ScreenManager.startLevelSelector();
                    break;
                case (int)buttonsE.about:
                    ScreenManager.startAbout();
                    break;
                case (int)buttonsE.exit:
                    Game.Exit();
                    break;
            }
        }

        public override void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch sb = GameLib.getInstance().SpriteBatch;

            sb.Begin();
            sb.Draw(GameLib.getInstance().get(TextureE.menuScreen), Vector2.Zero, Color.White);
            sb.Draw(logo, new Vector2(Game1.width / 2 - logo.Width / 2f, -20), Color.White);
            foreach (Button b in buttons)
            {
                b.draw(sb);
            }
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