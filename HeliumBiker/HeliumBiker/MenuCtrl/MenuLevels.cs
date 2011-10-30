using HeliumBiker.DeviceCtrl;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.MenuCtrl
{
    internal class MenuLevels : Screen
    {
        private enum buttonsE
        {
            easy = 0,
            medium = 1,
            hard = 2,
            back = 3
        }

        private Button[] buttons;
        private int selected = 1;
        private InputE vertical = InputE.center;
        private InputE enter = InputE.notShooting;
        private Texture2D logo;

        public MenuLevels(Game1 game, ScreenManager screenManager, DeviceManager dev)
            : base(game, screenManager, dev)
        {
            logo = GameLib.getInstance().get(TextureE.logo);
            buttons = new Button[]
            {
                new Button(new Vector2(Game1.width/2 - 300/2 , 0), new Vector2(300, 300),TextureE.easy),
                new Button(new Vector2(Game1.width/2 - 300/2 , 100), new Vector2(300, 300),TextureE.medium),
                new Button(new Vector2(Game1.width/2 - 300/2 , 200), new Vector2(300, 300),TextureE.hard),
                new Button(new Vector2(Game1.width/2 - 300/2 , 350), new Vector2(300, 300),TextureE.back)
            };
            buttons[selected].Focus = true;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.update(gameTime);
            }

            if (DeviceManager.VInput == InputE.down && (vertical == InputE.center || vertical == InputE.up))
            {
                buttons[selected].Focus = false;
                selected = (selected + 1) % buttons.Length;
                buttons[selected].Focus = true;
            }
            if (DeviceManager.VInput == InputE.up && (vertical == InputE.center || vertical == InputE.down))
            {
                buttons[selected].Focus = false;
                selected = selected - 1;
                if (selected < 0)
                {
                    selected = buttons.Length - 1;
                }
                buttons[selected].Focus = true;
            }

            if (DeviceManager.FiringInput == InputE.notShooting && enter == InputE.shooting)
            {
                gotoButton();
            }
            enter = DeviceManager.FiringInput;
            vertical = DeviceManager.VInput;
        }

        private void gotoButton()
        {
            switch (selected)
            {
                case (int)buttonsE.easy:
                case (int)buttonsE.medium:
                case (int)buttonsE.hard:
                    Menu.sound.Stop(Microsoft.Xna.Framework.Audio.AudioStopOptions.AsAuthored);
                    ScreenManager.startGame(selected);
                    break;
                case (int)buttonsE.back:
                    ScreenManager.goBackMenu();
                    break;
            }
        }

        public override void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch sb = GameLib.getInstance().SpriteBatch;
            sb.Begin();
            sb.Draw(GameLib.getInstance().get(TextureE.menuScreen), Vector2.Zero, Color.White);
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