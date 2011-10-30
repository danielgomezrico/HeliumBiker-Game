using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net.Sockets;
using System.Threading;
using System.Net.Sockets;

namespace HeliumBiker.DeviceCtrl
{ 
    class BluetoothServer
    {
        private const String GUID_SERVER = "{00112233-4455-6677-8899-aabbccddeeff}";
        private const int MESSAGE_SIZE = 30;
        BluetoothListener bluetoothListener;
        Thread threadListen;
        StreamAnalizer streamAnalizer;

        public BluetoothServer(Game1 game)
        {
            bluetoothListener = new BluetoothListener(new Guid(GUID_SERVER));
            streamAnalizer = new StreamAnalizer(game);
        }

        public void startListen()
        {
            threadListen = new Thread(listen);
            threadListen.Start();
        }

        private void listen()
        {
            bluetoothListener.Start();
            BluetoothClient client = bluetoothListener.AcceptBluetoothClient();
            NetworkStream stream = client.GetStream();
            string message = string.Empty;

            while (stream.CanRead)
            {
                byte[] buffer = new byte[MESSAGE_SIZE];

                stream.Read(buffer, 0, MESSAGE_SIZE);

                message = Encoding.UTF8.GetString(buffer);

                streamAnalizer.addMessage(message);
            }
        }


        internal static DeviceManager getDeviceManager(Game1 game)
        {
            BluetoothServer bs = new BluetoothServer(game);
            bs.startListen(); 
            return bs.streamAnalizer;
        }
    }
}

