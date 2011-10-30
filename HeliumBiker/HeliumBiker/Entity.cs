using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker
{
    internal abstract class Entity
    {
        private Animation animation;
        private Vector2 position;
        private Vector2 size;
        private Texture2D texture;
        private float angle;
        private Color color;
        private Vector2 origin;
        private float layerDepth;

        public Entity(Vector2 position, Vector2 size, Vector2 origin, float angle, Color color, Texture2D texture, Animation animation)
        {
            this.animation = animation;
            this.position = position;
            this.size = size;
            this.origin = origin;
            this.color = color;
            this.texture = texture;
            this.angle = angle;
            layerDepth = 1f;
        }

        public virtual void update(GameTime gameTime)
        {
            Animation.refresh(gameTime.ElapsedGameTime.Milliseconds);
        }

        public virtual void draw(SpriteBatch sb)
        {
            sb.Draw(Texture, getDestRectangle(), Animation.getSrcRect(), color, Angle, origin, SpriteEffects.None, LayerDepth);
        }

        private Rectangle getDestRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        #region gets y sets

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public Rectangle getSpace()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public Vector2 Position
        {
            get
            { return position; }
            set
            { position = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float LayerDepth
        {
            get { return layerDepth; }
            set { layerDepth = value; }
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public Animation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public void addAlpha(byte b)
        {
            if (b < 255)
            {
                color.A += b;
            }
        }

        #endregion gets y sets
    }
}