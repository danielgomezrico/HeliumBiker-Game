using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.PlayerParts
{
    internal class Chord : Entity
    {
        public Chord(Vector2 position, float length, float angle)
            : base(position, new Vector2(1f, length), new Vector2(getTexture().Width / 2, getTexture().Height), angle, Color.LightGray, getTexture(), Animation.getAnimation(getTexture()))
        {
            LayerDepth = 0.2f;
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.pixel);
        }
    }
}