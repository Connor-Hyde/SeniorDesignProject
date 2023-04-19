using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainTableWpf.Extensions
{
    public interface ISerialPortWrapper
    {
        bool IsOpen { get; }
        void Open();
        void Write(string text);
        void Close();
    }

    public class SerialPortWrapper : ISerialPortWrapper
    {
        private readonly SerialPort _serialPort;

        public SerialPortWrapper(SerialPort serialPort)
        {
            _serialPort = serialPort;
        }

        public bool IsOpen => _serialPort.IsOpen;

        public void Open() => _serialPort.Open();

        public void Write(string text) => _serialPort.Write(text);

        public void Close() => _serialPort.Close();
    }
}
