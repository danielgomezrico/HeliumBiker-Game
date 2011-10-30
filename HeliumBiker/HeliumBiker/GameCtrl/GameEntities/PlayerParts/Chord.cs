using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.PlayerParts
{
    class Chord : Entity
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
