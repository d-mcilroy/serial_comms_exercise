using System.IO.Ports;

namespace SerialDataCommon
{
    /// <summary>
    /// Transmit and receive data packets from a serial COM port.
    /// </summary>
    internal class SerialPortAccessLayer : IDisposable
    {
        /// <summary>
        /// Required data packet size in number of bytes.
        /// </summary>
        internal const int PACKET_SIZE = 24;
        
        /// <summary>
        /// Buffer polling duration in milliseconds.
        /// </summary>
        private const int POLLING_DURATION = 10;

        /// <summary>
        /// COM port interface.
        /// </summary>
        private SerialPort _serialPort;
        /// <summary>
        /// Indicates object disposal state.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Open a serial COM port with the given number.
        /// </summary>
        /// <param name="comPortNumber">COM port number.</param>
        internal SerialPortAccessLayer(int comPortNumber)
        {
            _serialPort = new SerialPort("COM" + comPortNumber.ToString());
            _disposed = false;

            _serialPort.Open();
        }

        /// <summary>
        /// Transmit the given packet of byte value.
        /// </summary>
        /// <param name="outgoingPacket">Outgoing byte packet.</param>
        internal async Task transmitAsync(params byte[] outgoingPacket)
        {
            // Abort if the packet is not the correct size
            if (outgoingPacket.Length < PACKET_SIZE)
                throw new ArgumentException($"Outgoing packet size '{outgoingPacket.Length}' is less than '{PACKET_SIZE}' bytes.");
            if (outgoingPacket.Length > PACKET_SIZE)
                throw new ArgumentException($"Outgoing packet size '{outgoingPacket.Length}' is greater than '{PACKET_SIZE}' bytes.");
            
            await _serialPort.BaseStream.WriteAsync(outgoingPacket, 0, outgoingPacket.Length);
        }

        /// <summary>
        /// Receive a packet of byte values.
        /// </summary>
        /// <returns>Incoming byte packet.</returns>
        internal async Task<byte[]> receiveAsync()
        {
            byte[] incomingPacket = new byte[PACKET_SIZE];
            int rxLength;

            // Loop polling for the next buffered incoming packet until it's available
            do rxLength = await _serialPort.BaseStream.ReadAsync(incomingPacket, 0, incomingPacket.Length);
            while (rxLength != PACKET_SIZE);            

            return incomingPacket;
        }

        ~SerialPortAccessLayer() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _serialPort.Dispose();

            _disposed = true;
        }
    }
}