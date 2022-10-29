using System;
using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class BaseArduinoController : MonoBehaviour
{
    /// <summary>
    /// ジャッキのPORT
    /// </summary>
    public enum PortId 
    {
        COM1,
        COM2,
        COM3,
        COM4,
        COM5,
        COM6,
        COM7,
        COM8,
        COM9,
        COM10,
    }
    
    // ジャッキのPORT型の列挙型の変数
    [SerializeField] PortId PortName;
    /// <summary>
    /// baudRateはマイコンのbaudRateと一致させる
    /// </summary>
    [SerializeField] int baudRate = 9600;
    public SerialPort serialPort_;
    [SerializeField] int dataBit = 0;
    
    public void PortOpen()
    {
        if (dataBit == 0)
        {
            serialPort_ = new SerialPort(PortName.ToString(), baudRate);
        }
        else
        {
            serialPort_ = new SerialPort(PortName.ToString(), baudRate, Parity.None, dataBit, StopBits.One);
            Debug.Log(serialPort_);
        }
        serialPort_.Open();
    }
    
    private void OnDestroy()
    {
        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }
}
