using SerialDataCommon;
using System.Text;

namespace SerialDataReceiver
{
    public partial class FormMain : Form
    {
        public FormMain() => InitializeComponent();

        private async void buttonReceive_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            try
            {
                button.Enabled = false;

                StringBuilder sb = new StringBuilder();

                using (SerialPortAccessLayer serialPort = new SerialPortAccessLayer(2))
                    foreach (byte b in await serialPort.receiveAsync())
                        sb.Append($"0x{b.ToString("X2")}\r\n");

                MessageBox.Show(sb.ToString().Trim());
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
