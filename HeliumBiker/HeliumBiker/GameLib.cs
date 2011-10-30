using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker
{
    internal class GameLib
    {
        public static AudioEngine engine;               // Motor de sonidos
        public static SoundBank soundBank;              // Banco de sonidos
        public static WaveBank waveBank;                // Almacena los archivos wav

        private static GameLib lib;
        private Dictionary<TextureE, Texture2D> textures;
        private Dictionary<SoundE, string> sounds;
        private Dictionary<FontE, SpriteFont> fonts;
        private Game1 game;
        private SpriteBatch sp;

        private bool gameTextures = false;
        private bool guiTextures = false;
        private bool globalTextures = false;

        private GameLib(Game1 game, SpriteBatch sp)
        {
            this.game = game;
            textures = new Dictionary<TextureE, Texture2D>();
            sounds = new Dictionary<SoundE, string>();
            fonts = new Dictionary<FontE, SpriteFont>();
            engine = new AudioEngine("Content/Sounds/sonidos.xgs");
            waveBank = new WaveBank(engine, "Content/Sounds/Wave Bank.xwb");
            soundBank = new SoundBank(engine, "Content/Sounds/Sound Bank.xsb");
            this.sp = sp;
        }

        public static GameLib getInstance(Game1 game, SpriteBatch sp)
        {
            if (lib == null)
            {
                lib = new GameLib(game, sp);
                lib.loadGlobalTextures();
                lib.loadFonts();
            }
            return lib;
        }

        public static GameLib getInstance()
        {
            return lib;
        }

        public Texture2D get(TextureE texture)
        {
            return textures[texture];
        }

        public string get(SoundE sound)
        {
            return sounds[sound];
        }

        public SpriteFont get(FontE font)
        {
            return fonts[font];
        }

        public void loadGlobalTextures()
        {
            if (!globalTextures)
            {
                textures.Add(TextureE.pixel, game.Content.Load<Texture2D>("Images/pixel"));
            }
        }

        public void loadGameTextures()
        {
            if (!gameTextures)
            {
                gameTextures = true;
                textures.Add(TextureE.bike, game.Content.Load<Texture2D>("Images/Game/bike"));
                textures.Add(TextureE.ballon, game.Content.Load<Texture2D>("Images/Game/ballon"));
                textures.Add(TextureE.frontMountains, game.Content.Load<Texture2D>("Images/Game/frontMountains"));
                textures.Add(TextureE.backMountains, game.Content.Load<Texture2D>("Images/Game/backMountains"));
                textures.Add(TextureE.water, game.Content.Load<Texture2D>("Images/Game/waves1"));
                textures.Add(TextureE.bird, game.Content.Load<Texture2D>("Images/Game/bird"));
                textures.Add(TextureE.fish, game.Content.Load<Texture2D>("Images/Game/pirana"));
                textures.Add(TextureE.stone, game.Content.Load<Texture2D>("Images/Game/stone"));
                textures.Add(TextureE.hand, game.Content.Load<Texture2D>("Images/Game/hand"));
                textures.Add(TextureE.house, game.Content.Load<Texture2D>("Images/Game/house"));
                textures.Add(TextureE.monster, game.Content.Load<Texture2D>("Images/Game/monster"));
                textures.Add(TextureE.bg, game.Content.Load<Texture2D>("Images/Game/bg"));
            }
        }

        public void loadGUITextures()
        {
            if (!guiTextures)
            {
                textures.Add(TextureE.logo, game.Content.Load<Texture2D>("Images/GUI/logo"));
                textures.Add(TextureE.menuScreen, game.Content.Load<Texture2D>("Images/GUI/menuScreen"));
                textures.Add(TextureE.start, game.Content.Load<Texture2D>("Images/GUI/start"));
                textures.Add(TextureE.exit, game.Content.Load<Texture2D>("Images/GUI/exit"));
                textures.Add(TextureE.about, game.Content.Load<Texture2D>("Images/GUI/about"));
                textures.Add(TextureE.connect, game.Content.Load<Texture2D>("Images/GUI/connect"));
                textures.Add(TextureE.easy, game.Content.Load<Texture2D>("Images/GUI/easy"));
                textures.Add(TextureE.medium, game.Content.Load<Texture2D>("Images/GUI/medium"));
                textures.Add(TextureE.hard, game.Content.Load<Texture2D>("Images/GUI/hard"));
                textures.Add(TextureE.back, game.Content.Load<Texture2D>("Images/GUI/back"));
                textures.Add(TextureE.aboutScreen, game.Content.Load<Texture2D>("Images/GUI/aboutScreen"));
            }
        }

        public void loadFonts()
        {
            fonts.Add(FontE.percentage, game.Content.Load<SpriteFont>("Fonts/percentage"));
        }

        public void loadSounds()
        {
            sounds.Add(SoundE.diez, "10");
            sounds.Add(SoundE.pop, "BallonPop");
            sounds.Add(SoundE.once, "11");
        }

        public Cue playCue(SoundE sound)
        {
            return soundBank.GetCue(get(sound));
        }

        public SpriteBatch SpriteBatch
        {
            get { return sp; }
            set { sp = value; }
        }
    }
}