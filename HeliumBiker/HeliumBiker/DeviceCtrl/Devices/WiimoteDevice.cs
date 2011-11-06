using System;
using System.Threading;
using Microsoft.Xna.Framework;
using WiimoteLib;

namespace HeliumBiker.DeviceCtrl.Devices
{
    /// <summary>
    /// Device controller for wiimote and nunchuck
    /// </summary>
    internal class WiimoteDevice : DeviceManager
    {
        private Vector2 position;
        private Wiimote wiimote;
        private Thread searchThread;
        Game1 game;
        //private bool canSearch;

        public WiimoteDevice(Game1 game)
            : base(game)
        {
            this.game = game;
            this.position = Vector2.Zero;
        }

        /// <summary>
        /// Searchs and connect the wiimote in a new thread
        /// </summary>
        public void startSearchWiimote()
        {
            //canSearch = true;
            searchThread = new Thread(this.initWiimote);
            searchThread.Start();
        }

        private void initWiimote()
        {
            // find all wiimotes connected to the system
            WiimoteCollection wiimoteCollection = new WiimoteCollection();
            //while (canSearch)
            //{
            try
            {
                wiimoteCollection.FindAllWiimotes();
            }
            catch (WiimoteNotFoundException ex)
            {
                //MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WiimoteException ex)
            {
                //MessageBox.Show(ex.Message, "Wiimote error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (wiimoteCollection.Count == 1)
            {
                Connected = true;

                wiimote = wiimoteCollection[0];

                // connect it and set it up as always
                wiimote.WiimoteChanged += wiimoteChanged;
                wiimote.WiimoteExtensionChanged += wiimoteExtensionChanged;

                wiimote.Connect();

                if (wiimote.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
                    wiimote.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);

                wiimote.SetLEDs(false, true, true, false);
            }
            else
            {
                Connected = false;
            }
            //}
        }

        private void wiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            Wiimote wii = (Wiimote)sender;
            WiimoteState wiimoteState = e.WiimoteState;
            VInput = InputE.center;

            if (wiimoteState.ButtonState.Up)
            {
                VInput = InputE.up;
            }
            else if (wiimoteState.ButtonState.Down)
            {
                VInput = InputE.down;
            }

            if (wiimoteState.ButtonState.Left)
            {
                HInput = InputE.left;
            }
            else if (wiimoteState.ButtonState.Right)
            {
                HInput = InputE.right;
            }

            if (wiimoteState.ExtensionType == ExtensionType.Nunchuk)
            {
                if (Math.Abs(wiimoteState.NunchukState.Joystick.X) > 0.3 || Math.Abs(wiimoteState.NunchukState.Joystick.Y) > 0.3)
                {
                    //TODO: Revisar esta formula, como crear el position con base en el nunchuk
                    position = new Vector2(wiimoteState.NunchukState.Joystick.X, wiimoteState.NunchukState.Joystick.Y);

                    Console.WriteLine(position);
                    FiringInput = InputE.shooting;
                }
                else
                {
                    FiringInput = InputE.notShooting;
                }
            }
        }

        private void wiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            //Nunchuck is connected?
            //e.ExtensionType == ExtensionType.Nunchuk ?
            //Then yes
        }

        public override Vector2 getPointPosition()
        {
            return position;
        }

        internal static DeviceManager getDeviceManager(Game1 game)
        {
            WiimoteDevice device = new WiimoteDevice(game);
            device.startSearchWiimote();
            return device;
        }
    }
}