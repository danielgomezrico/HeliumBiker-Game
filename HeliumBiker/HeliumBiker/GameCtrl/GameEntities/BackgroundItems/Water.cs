using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.BackgroundItems
{
    class Water : Entity
    {
        private List<Water> waves; 
        private static Vector2 size = new Vector2(1024,120);
        private float worldDistance;
        private static float yDisp = 0f;
        private float velX;

        public Water(Vector2 position)
            : base(position, size, Vector2.Zero, 0f, Color.White,getTexture(), Animation.getAnimation(getTexture(),size))
        {
            waves = new List<Water>();
            LayerDepth = 0f;
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.water);
        }

        public override void update(GameTime gameTime)
        {
            
        }
    }
}
