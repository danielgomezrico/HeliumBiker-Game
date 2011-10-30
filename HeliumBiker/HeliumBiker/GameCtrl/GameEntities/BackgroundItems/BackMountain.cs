using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.BackgroundItems
{
    internal class BackMountain : Entity
    {
        private static Vector2 size = new Vector2(1271, 470);

        public BackMountain(Vector2 position)
            : base(position, size, Vector2.Zero, 0f, Color.White, getTexture(), Animation.getAnimation(getTexture(), size))
        {
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.backMountains);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}