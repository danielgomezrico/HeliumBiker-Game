using Microsoft.Xna.Framework;

namespace HeliumBiker.DeviceCtrl
{
    public abstract class DeviceManager : GameComponent
    {
        private InputE vInput;
        private InputE hInput;
        private InputE firingInput;

        /// <summary>
        /// If the Device is connected to the pc
        /// </summary>
        private bool connected;

        public DeviceManager(Game1 game)
            : base(game)
        {
            connected = false;
        }

        public abstract Vector2 getPointPosition();

        #region gets y sets

        public InputE FiringInput
        {
            get { return firingInput; }
            set { firingInput = value; }
        }

        public InputE VInput
        {
            get { return vInput; }
            set { vInput = value; }
        }

        public InputE HInput
        {
            get { return hInput; }
            set { hInput = value; }
        }

        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        #endregion gets y sets
    }
}