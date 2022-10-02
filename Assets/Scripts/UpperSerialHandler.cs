using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class UpperSerialHandler : MonoBehaviour
{
    const string portName = "COM7"; // ポート番号は自分で確認してください
    const int baudRate = 9600;
    public SerialPort serialPort_ = new SerialPort(portName, baudRate);

    void Start()
    {

        serialPort_.Open();
    }


}