using HeliumBiker.DeviceCtrl;
using HeliumBiker.GameCtrl;
using HeliumBiker.MenuCtrl;

namespace HeliumBiker.ScreenCtrl
{
    public class ScreenManager
    {
        private Game1 game;
        private Menu menu;
        private World world;
        private ConnectionScreen connect;
        private DeviceManager devMan;
        private MenuLevels levels;
        private MenuAbout about;

        public ScreenManager(Game1 game, DeviceManager devman)
        {
            this.game = game;
            this.devMan = devman;
            Initialize();
        }

        public void Initialize()
        {
            connect = new ConnectionScreen(game, this, devMan);
            game.Components.Add(connect);
        }

        internal void switchScreen(Screen screen)
        {
        }

        internal void startGame(int level)
        {
            levels.setState(State.hidden);
            world = new World(game, this, devMan, level);
            game.Components.Add(world); ;
        }

        internal void startMenu()
        {
            connect.setState(State.hidden);
            menu = new Menu(game, this, devMan);
            game.Components.Add(menu);
        }

        internal void startAbout()
        {
            menu.setState(State.hidden);
            about = new MenuAbout(game, this, devMan);
            game.Components.Add(about);
        }

        internal void goBackMenu()
        {
            game.Components.Remove(world);
            game.Components.Remove(levels);
            game.Components.Remove(about);
            menu.setState(State.transitionIn);
        }

        internal void startLevelSelector()
        {
            menu.setState(State.hidden);
            levels = new MenuLevels(game, this, devMan);
            game.Components.Add(levels); ;
        }
    }
}