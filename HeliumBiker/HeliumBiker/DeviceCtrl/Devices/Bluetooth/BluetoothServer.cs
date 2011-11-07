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

        private BluetoothDevice bluetoothDevice;
        private BluetoothListener bluetoothListener;
        private Thread threadListen;

        public BluetoothServer(Game1 game, BluetoothDevice device)
        {
            bluetoothListener = new BluetoothListener(new Guid(GUID_SERVER));
            bluetoothDevice = device;
        }

        #region Bluetooth listening

        public void StartListening()
        {
            threadListen = new Thread(Listen);
            threadListen.Start();
        }

        private void StopListening()
        {
            if (threadListen != null && threadListen.IsAlive)
            {
                //TODO: probar
                bluetoothListener.EndAcceptBluetoothClient(null);
                bluetoothListener.Stop();
                threadListen.Join();
            }
        }

        private void Listen()
        {
            bluetoothListener.Start();

            bluetoothDevice.Connected = false;

            BluetoothClient client = bluetoothListener.AcceptBluetoothClient();

            bluetoothDevice.Connected = true;

            if (client != null)
            {
                NetworkStream stream = client.GetStream();
                string message = string.Empty;

                byte[] buffer = new byte[MESSAGE_SIZE];

                while (stream.Read(buffer, 0, MESSAGE_SIZE) != 0) // 0 = connection is lost
                {
                    message = Encoding.UTF8.GetString(buffer);

                    bluetoothDevice.addMessage(message);
                }

                bluetoothDevice.clearMessages();
            }
        }

        #endregion Bluetooth listening
    }
}