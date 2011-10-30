using Microsoft.Xna.Framework;

namespace HeliumBiker.MenuCtrl
{
    internal class Button : Entity
    {
        private Vector2 disp;
        private bool focus = false;
        private float jumpSpeed = 0f;
        private float floatingTime = 40f;
        private float elapsedTime;
        private float i = 0.1f;
        private string text;
        private Color cText;

        public Button(Vector2 position, Vector2 size, TextureE textureE) :
            base(position, size, Vector2.Zero, 0f, Color.White, getTexture(textureE), Animation.getAnimation(getTexture(textureE)))
        {
            disp = new Vector2(10f, 10f);
            LayerDepth = 0.5f;
        }

        private static Microsoft.Xna.Framework.Graphics.Texture2D getTexture(TextureE textureE)
        {
            return GameLib.getInstance().get(textureE);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (focus)
            {
                if ((elapsedTime += gameTime.ElapsedGameTime.Milliseconds) > floatingTime)
                {
                    jumpSpeed -= i;
                    if (jumpSpeed >= 1)
                    {
                        i *= -1;
                    }
                    if (jumpSpeed < -1)
                    {
                        i *= -1;
                    }
                    elapsedTime -= floatingTime;
                    Position = new Vector2(Position.X, Position.Y + jumpSpeed);
                }
                Color = new Color(240, 240, 240);
            }
            else
            {
                Color = Color.White;
            }
        }

        public bool Focus
        {
            get { return focus; }
            set { focus = value; }
        }
    }
}