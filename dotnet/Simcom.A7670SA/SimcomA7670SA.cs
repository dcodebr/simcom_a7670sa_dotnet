using System.IO.Ports;

namespace Simcom.A7670SA;

public class SimcomA7670SA
{
    private SerialPort SerialPort { get; set; }
    public string PortName { get; private set; }
    public int BaudRate { get; private set; }
    public int DefaultTimeout { get; set; }
    public int DataBits => 8;
    public StopBits StopBits => StopBits.One;

    public SimcomA7670SA(string portName, int baudRate, int defaultTimeout = 1000)
    {
        PortName = portName;
        BaudRate = baudRate;
        DefaultTimeout = defaultTimeout;

        SerialPort = new SerialPort()
        {
            PortName = PortName,
            BaudRate = BaudRate,
            DataBits = DataBits,
            StopBits = StopBits
        };
    }

    private void SerialPortOpen(int? writeTimeout = null, int? readTimeout = null)
    {
        SerialPort.Open();
        SerialPort.WriteTimeout = writeTimeout ?? DefaultTimeout;
        SerialPort.ReadTimeout = readTimeout ?? DefaultTimeout;
    }

    private void SerialPortClose()
    {
        SerialPort.Close();
    }

    private void SerialPortDisableEcho()
    {
        SerialPort.WriteLine("ATE0\r");
        SerialPortReadFullResponse();
    }

    private void SerialPortSetTextMode()
    {
        string cmgfCommand = "AT+CMGF=1\r";
        SerialPort.WriteLine(cmgfCommand);
        SerialPortReadFullResponse();
    }

    private string SerialPortReadFullResponse()
    {
        var result = string.Empty;

        while (SerialPort.BytesToRead > 0)
        {
            result += SerialPort.ReadExisting();
        }

        return result;
    }


    public string SendATCommand(String command, int? writeTimeout = null, int? readTimeout = null)
    {
        string atCommand = $"{command}\r";
        SerialPortOpen(writeTimeout, readTimeout);
        SerialPortDisableEcho();

        SerialPort.WriteLine(atCommand);
        string result = SerialPortReadFullResponse();
        SerialPortClose();

        return result;
    }

    public string SendSMS(string phoneNumber, string message)
    {
        string cmgsCommand = $"AT+CMGS=\"{phoneNumber}\"\r";
        string messageCommand = $"{message}\x1A";

        SerialPortOpen();
        SerialPortDisableEcho();

        SerialPort.WriteLine(cmgsCommand);
        Thread.Sleep(200);
        SerialPortReadFullResponse();

        SerialPort.WriteLine(messageCommand);
        Thread.Sleep(2000);
        var result = SerialPortReadFullResponse();
        
        SerialPortClose();

        return result;
    }

    public string ListSMS(SimcomA7670SASmsReadEnum smsRead)
    {
        var smsReadType = smsRead.GetDescription();
        var cmglCommand = $"AT+CMGL=\"{smsReadType}\"\r";

        SerialPortOpen();
        SerialPortDisableEcho();

        SerialPort.WriteLine(cmglCommand);
        Thread.Sleep(2000);

        var result = SerialPortReadFullResponse();
        SerialPortClose();

        return result;
    }

    public string ReadSMS(int index)
    {
        var cmgrCommand = $"AT+CMGR={index}\r";

        SerialPortOpen();
        SerialPortDisableEcho();

        SerialPort.WriteLine(cmgrCommand);

        Thread.Sleep(2000);
        var result =SerialPortReadFullResponse();
        SerialPortClose();

        return result;
    }

    public string DeleteSMS(int index)
    {
        var cmgdCommand = $"AT+CMGD={index}\r";

        SerialPortOpen();
        SerialPortDisableEcho();

        SerialPort.WriteLine(cmgdCommand);
        var result = SerialPortReadFullResponse();
        SerialPortClose();

        return result;
    }
}
