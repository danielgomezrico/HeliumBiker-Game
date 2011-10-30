using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using HeliumBiker.GameCtrl.GameEntities.PlayerParts;

namespace HeliumBiker.GameCtrl.GameEntities.ThrowableObjects
{
    class Stone : ThrowableObject
    {
        public Stone(Vector2 position, Vector2 acceleration)
            : base(position, new Vector2(10, 10), 0.0f, Color.White, getTexture(), Animation.getAnimation(getTexture()), getShapes())
        {
            LayerDepth = 0.5f;
            Velocity = acceleration;
        }

        private static ShapeCtrl.Shape[] getShapes()
        {
            return new Shape[] { new CircleShape(Vector2.Zero,5)};
        }

        private static Microsoft.Xna.Framework.Graphics.Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.stone);
        }

        public override void update(GameTime gameTime)
        {
            Angle += MathHelper.ToRadians(15f) * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 100f);
            base.update(gameTime);
        }

    }
}
