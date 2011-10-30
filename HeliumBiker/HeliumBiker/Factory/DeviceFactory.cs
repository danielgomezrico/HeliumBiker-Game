using HeliumBiker.DeviceCtrl;

namespace HeliumBiker.Factory
{
    internal class DeviceFactory
    {
        public static DeviceManager getDeviceManagaer(Game1 game)
        {
            //Game1.testText = "hi!";
            //return new ComputerDevice(game);
            return BluetoothServer.getDeviceManager(game);
        }
    }
}