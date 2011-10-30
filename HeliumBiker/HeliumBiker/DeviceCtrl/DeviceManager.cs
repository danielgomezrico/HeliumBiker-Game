using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeliumBiker.DeviceCtrl
{
    public abstract class DeviceManager : GameComponent
    {
        private InputE vInput;
        private InputE hInput;
        private InputE firingInput;

        public DeviceManager(Game1 game)
            : base(game)
        {
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
        #endregion
    }
}
