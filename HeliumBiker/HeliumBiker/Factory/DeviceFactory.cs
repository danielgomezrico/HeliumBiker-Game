using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeliumBiker.DeviceCtrl;

namespace HeliumBiker.Factory
{
    class DeviceFactory
    {
        public static DeviceManager getDeviceManagaer(Game1 game)
        {
            Game1.testText = "hi!"; 
            return new ComputerDevice(game); 
            //return BluetoothServer.getDeviceManager(game);
        }
    }
}
