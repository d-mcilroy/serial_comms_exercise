using SerialDataCommon;

namespace SerialDataReceiver
{
    public partial class FormMain : Form
    {
        public FormMain() => InitializeComponent();

        private async void buttonTransmit_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            try
            {
                button.Enabled = false;

                using (SerialPortAccessLayer serialPort = new SerialPortAccessLayer(1))
                {
                    List<byte> outgoingPacket = new List<byte>(SerialPortAccessLayer.PACKET_SIZE)
                    {
                        // Status flags
                        0xad,

                        // Little-endian encoded signed 16-bit temperature in degrees C
                        0xfe,
                        0xff
                    };

                    // Single-precision 32-bit floating-point DC voltage
                    outgoingPacket.AddRange(BitConverter.GetBytes(13.2f));

                    // Two-digit decimal major firmware version,
                    // two-digit decimal minor firmware version,
                    // ASCI alphatical release candidate version
                    outgoingPacket.Add(0x02);
                    outgoingPacket.Add(0x05);
                    outgoingPacket.Add(0x63);

                    // Null-terminated ASCII string message to the user
                    foreach (byte character in "Hello, World!\0")
                        outgoingPacket.Add(character);

                    await serialPort.transmitAsync(outgoingPacket.ToArray());
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            finally
            {
                button.Enabled = true;
            }
        }
    }
}
