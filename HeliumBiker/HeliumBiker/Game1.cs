using HeliumBiker.DeviceCtrl;
using HeliumBiker.Factory;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager screenManager;
        DeviceManager deviceManager;

        public static const int width = 1024;
        public static const int height = 600;
        public static string testText = "";

        private const Color backgroundColor = new Color(233, 248, 255);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameLib.getInstance(this, spriteBatch);
            GameLib.getInstance().loadSounds();
            deviceManager = DeviceFactory.getDeviceManagaer(this);
            screenManager = new ScreenManager(this, deviceManager);
            Components.Add(deviceManager);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            base.Draw(gameTime);

            spriteBatch.Begin();

            string fps = "fps " + (1f / (float)gameTime.ElapsedGameTime.TotalSeconds);
            //spriteBatch.DrawString(GameLib.getInstance().get(FontE.percentage), fps, new Vector2(900f, 10f), Color.White);
            //spriteBatch.DrawString(GameLib.getInstance().get(FontE.percentage), testText, new Vector2(10f, 30f), Color.White);
            spriteBatch.End();
        }
    }
}