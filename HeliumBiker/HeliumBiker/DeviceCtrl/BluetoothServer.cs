using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using InTheHand.Net.Sockets;

namespace HeliumBiker.DeviceCtrl
{
    internal class BluetoothServer
    {
        private const String GUID_SERVER = "{00112233-4455-6677-8899-aabbccddeeff}";
        private const int MESSAGE_SIZE = 30;

        private BluetoothListener bluetoothListener;
        private Thread threadListen;
        private StreamAnalizer streamAnalizer;
        private bool acceptConnections;

        public BluetoothServer(Game1 game)
        {
            acceptConnections = true;
            bluetoothListener = new BluetoothListener(new Guid(GUID_SERVER));
            streamAnalizer = new StreamAnalizer(game);
        }

        public void StartListen()
        {
            threadListen = new Thread(Listen);
            threadListen.Start();
        }

        private void Listen()
        {
            bluetoothListener.Start();

            while (acceptConnections)
            {
                BluetoothClient client = bluetoothListener.AcceptBluetoothClient();
                if (client != null)
                {
                    NetworkStream stream = client.GetStream();
                    string message = string.Empty;

                    byte[] buffer = new byte[MESSAGE_SIZE];

                    while (stream.Read(buffer, 0, MESSAGE_SIZE) != 0) // 0 = connection is lost
                    {
                        message = Encoding.UTF8.GetString(buffer);

                        streamAnalizer.addMessage(message);
                    }

                    streamAnalizer.clearMessages();
                }
            }
        }

        private void StopListen()
        {
            if (threadListen != null && threadListen.IsAlive)
            {
                //TODO: probar
                bluetoothListener.EndAcceptBluetoothClient(null);
                bluetoothListener.Stop();
                acceptConnections = false;
                threadListen.Join();
            }
        }

        internal static DeviceManager getDeviceManager(Game1 game)
        {
            BluetoothServer bs = new BluetoothServer(game);
            bs.StartListen();
            return bs.streamAnalizer;
        }
    }
}