using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.PlayerParts
{
    internal class Hand : Entity
    {
        private bool visible = false;

        public Hand(Vector2 position)
            : base(position, new Vector2(10, 10), new Vector2(getTexture().Height / 2, getTexture().Height / 2), 0f, new Color(177, 137, 94), getTexture(), Animation.getAnimation(getTexture()))
        {
        }

        public override void draw(SpriteBatch sb)
        {
            if (visible)
                base.draw(sb);
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.hand);
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
    }
}