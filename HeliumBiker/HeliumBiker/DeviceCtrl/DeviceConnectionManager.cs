namespace HeliumBiker.DeviceCtrl.Devices
{
    internal class DeviceConnectionManager
    {
        /// <summary>
        /// Says
        /// </summary>
        private static bool connected;

        public static bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
    }
}