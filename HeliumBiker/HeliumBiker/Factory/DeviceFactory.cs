using HeliumBiker.DeviceCtrl;
using HeliumBiker.DeviceCtrl.Devices;

namespace HeliumBiker.Factory
{
    internal class DeviceFactory
    {
        public static DeviceManager getDeviceManager(Game1 game)
        {
            //Game1.testText = "hi!";
            //return new ComputerDevice(game);

            //return BluetoothServer.getDeviceManager(game);

            WiimoteDevice wiidevice = new WiimoteDevice(game);
            wiidevice.startSearchWiimote();
            return wiidevice;
        }
    }
}