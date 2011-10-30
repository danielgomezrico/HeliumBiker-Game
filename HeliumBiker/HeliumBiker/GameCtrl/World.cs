using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeliumBiker.GameCtrl.GameEntities;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework.Graphics;
using HeliumBiker.DeviceCtrl;
using HeliumBiker.GameCtrl.GameEntities.BackgroundItems;
using Microsoft.Xna.Framework;
using HeliumBiker.GameCtrl.GameEntities.PlayerParts;
using HeliumBiker.GameCtrl.GameEntities.Enemies;
using Microsoft.Xna.Framework.Audio;

namespace HeliumBiker.GameCtrl
{
    class World : Screen
    {
        public enum GameState { playing, won, lost }

        private static Vector2 percentagePos = new Vector2(10, 10);
        private static Vector2 pointsPos = new Vector2(900, 10);
        private static Color infoColor = new Color(210, 162, 0);
        private Cue song;
        private List<PhysicsObject> objs;
        private MountainCtrl mountains;
        private BackMountain bMountain;
        private WaterCtrl waters;
        private BirdGenerator birds;
        private FishGenerator fishes;
        private Player player;
        private House house;
        private Monster monster;
        private GameState gameState;

        public GameState GState
        {
            get { return gameState; }
            set { gameState = value; }
        }
        private Texture2D bg;
        private Vector2 bgPos;

        public static float gravity = 0.5f;
        private float horizontalVel;
        private float worldLength;
        private float currentDistance;
        private int percentage = 0;
        public static int points = 0;
        private int floor;

        public World(Game1 game, ScreenManager screenManager, DeviceManager dev, int level)
            : base(game, screenManager, dev)
        {
            points = 0;
            currentDistance = 0;
            GameLib.getInstance().loadGameTextures();
            objs = new List<PhysicsObject>();
            setVariables(level);
            mountains = new MountainCtrl(worldLength,horizontalVel);
            waters = new WaterCtrl(worldLength, horizontalVel);
            bMountain = new BackMountain(new Vector2(0, 0f));
            monster = new Monster(player, this);
            gameState = GameState.playing;
            bg = GameLib.getInstance().get(TextureE.bg);
            bgPos = new Vector2(0, -bg.Height + Game1.height);
            song = GameLib.getInstance().playCue(SoundE.diez);
            song.Play();
        }

        private void setVariables(int level)
        {
            switch (level)
            {
                case 0:
                    worldLength = 20;
                    Floor = Game1.height;
                    horizontalVel = 6;
                    player = new Player(new Vector2(100f, Game1.height/2f), DeviceManager, this);
                    birds = new BirdGenerator(this, 6000f, horizontalVel);
                    fishes = new FishGenerator(this, 4000f);
                    break;
                case 1:
                    worldLength = 30;
                    Floor = Game1.height;
                    horizontalVel = 6;
                    player = new Player(new Vector2(100f, Game1.height/2f), DeviceManager, this);
                    birds = new BirdGenerator(this, 3000f, horizontalVel);
                    fishes = new FishGenerator(this, 1000f);
                    break;
                case 2:
                    worldLength = 50;
                    Floor = Game1.height;
                    horizontalVel = 6;
                    player = new Player(new Vector2(100f, Game1.height/2f), DeviceManager, this);
                    birds = new BirdGenerator(this, 2000f, horizontalVel);
                    fishes = new FishGenerator(this, 600f);
                    break;
                default:
                    break;
            }
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float delta = HorizontalVel * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f);
            CurrentDistance += delta;
            applyPhysics(gameTime, delta);
            mountains.update(CurrentDistance, delta);
            waters.update(CurrentDistance);
            monster.update(gameTime);
            if (percentage < 100)
            {
                birds.update(gameTime);
                fishes.update(gameTime);
                percentage = (int)Math.Round(currentDistance / worldLength);
            }
            else if (percentage == 100)
            {
                house = new House(new Vector2(player.Bike.Position.X + Game1.width, 300));
                Objs.Add(house);
                percentage++;
            }
            else
            {
                PhysicsMath.ObjectsCollition(house, player.Bike);
                if (house.Collided)
                {
                    GState = GameState.won;
                    setState(State.transitionOut);
                }
            }
            foreach (PhysicsObject obj in Objs)
            {
                obj.update(gameTime);
            }
        }

        private void applyPhysics(GameTime gameTime, float horizontalVel)
        {
            checkCollitions();
            float currentGravity = gravity * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f);
            foreach (PhysicsObject obj in Objs)
            {
                if (obj.Position.Y + obj.Size.Y / 2 < Floor)
                {
                    obj.applyGravity(currentGravity);
                }
            }
            player.update(gameTime, currentGravity);
        }

        private void checkCollitions()
        {
            for (int i = 0; i < objs.Count; i++)
            {
                PhysicsMath.ObjectsCollition(objs[i], player.Bike);
                for (int j = player.Ballons.Count - 1; j >= 0 ; j--)
                {
                    PhysicsMath.ObjectsCollition(objs[i], player.Ballons[j]);
                }
                for (int j = i + 1; j < objs.Count; j++)
                {
                    PhysicsMath.ObjectsCollition(objs[i], objs[j]);
                }
            }
        }

        public override void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            drawBackground();
            SpriteBatch sb = GameLib.getInstance().SpriteBatch;
            sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, DepthStencilState.Default, null, null, getCamera());
            waters.draw(sb);
            mountains.draw(sb);
            monster.draw(sb);
            foreach (PhysicsObject obj in Objs)
            {
                obj.draw(sb);
            }
            player.draw(sb);
            sb.End();
        }

        private Matrix getCamera()
        {
            return Matrix.CreateTranslation(new Vector3(-CurrentDistance, 0f, 0f));
        }

        private void drawBackground()
        {
            SpriteBatch sb = GameLib.getInstance().SpriteBatch;
            sb.Begin();
            sb.Draw(bg, bgPos, Color.White);
            bMountain.draw(sb);
            sb.DrawString(GameLib.getInstance().get(FontE.percentage), "% " + percentage, percentagePos, infoColor);
            sb.DrawString(GameLib.getInstance().get(FontE.percentage), points + "", pointsPos, infoColor);
            sb.End();
        }

        public override void transitionOut(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (monster.Animation.CurrentFrame == 0)
            {
                ScreenManager.goBackMenu();
            }
            song.Stop(AudioStopOptions.AsAuthored);
            monster.update(gameTime);
        }

        public override void transitionIn(Microsoft.Xna.Framework.GameTime gameTime)
        {
            setState(State.active);
        }

        #region gets y sets

        public float HorizontalVel
        {
            get { return horizontalVel; }
            set { horizontalVel = value; }
        }
        public int Floor
        {
            get { return floor; }
            set { floor = value; }
        }
        public float CurrentDistance
        {
            get { return currentDistance; }
            set { currentDistance = value; }
        }
        public float WorldLength
        {
            get { return worldLength; }
        }
        internal List<PhysicsObject> Objs
        {
            get { return objs; }
            set { objs = value; }
        }
        #endregion
    }
}
