using HeliumBiker.GameCtrl.GameEntities.PlayerParts;
using HeliumBiker.GameCtrl.GameEntities.ShapeCtrl;
using HeliumBiker.ScreenCtrl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeliumBiker.GameCtrl.GameEntities.Enemies
{
    internal class Monster : PhysicsObject
    {
        private Player player;
        private World world;
        private int frameCount = 0;

        public Monster(Player player, World world)
            : base(new Vector2(0, world.Floor), new Vector2(200, 200), 0.0f, Color.White, getTexture(), Animation.getAnimation(getTexture()), getShapes())
        {
            this.player = player;
            this.world = world;
            LayerDepth = 0.09f;
            Animation.Reverse = true;
            Animation.Stick = true;
            frameCount = Animation.getFrameCount(0);
        }

        private static Texture2D getTexture()
        {
            return GameLib.getInstance().get(TextureE.monster);
        }

        private static Shape[] getShapes()
        {
            return new Shape[]
            {
            };
        }

        public override void update(GameTime gameTime)
        {
            float limit = 0.4f;
            float percent = (world.Floor - player.Position.Y) / world.Floor;
            float yLimit = world.Floor;
            float fishP = (percent - limit) / percent;

            yLimit -= world.Floor / 3 * (1f - percent) - 50;
            if (percent < limit)
            {
                Animation.Reverse = false;
            }
            else
            {
                Animation.Reverse = true;
            }
            Position = new Vector2(player.Position.X, yLimit);

            if (player.Position.Y > Position.Y + 20)
            {
                world.GState = World.GameState.lost;
                world.setState(State.transitionOut);
                Animation.Reverse = true;
            }
            base.update(gameTime);
        }
    }
}