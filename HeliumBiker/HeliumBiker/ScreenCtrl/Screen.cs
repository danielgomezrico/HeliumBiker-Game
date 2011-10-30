using HeliumBiker.DeviceCtrl;
using Microsoft.Xna.Framework;

namespace HeliumBiker.ScreenCtrl
{
    public enum State { active, hidden, transitionIn, transitionOut }

    public abstract class Screen : DrawableGameComponent
    {
        private State state = State.transitionIn;
        private DeviceManager devMan;
        private ScreenManager screenManager;

        public Screen(Game game, ScreenManager screenManager, DeviceManager devman)
            : base(game)
        {
            this.screenManager = screenManager;
            this.devMan = devman;
        }

        public DeviceManager DeviceManager
        {
            get { return devMan; }
            set { devMan = value; }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.active:
                    update(gameTime);
                    break;
                case State.transitionIn:
                    transitionIn(gameTime);
                    break;
                case State.transitionOut:
                    transitionOut(gameTime);
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            switch (state)
            {
                case State.active:
                case State.transitionIn:
                case State.transitionOut:
                    draw(gameTime);
                    break;
            }
        }

        public abstract void update(GameTime gameTime);

        public abstract void draw(GameTime gameTime);

        public abstract void transitionOut(GameTime gameTime);

        public abstract void transitionIn(GameTime gameTime);

        public void setState(State state)
        {
            this.state = state;
            switch (state)
            {
                case State.active:
                    break;
                case State.hidden:
                    break;
                case State.transitionIn:
                    if (!Game.Components.Contains(this))
                        Game.Components.Add(this);
                    break;
                case State.transitionOut:
                    break;
            }
        }

        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }
    }
}