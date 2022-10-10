using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class UpperSerialHandler : MonoBehaviour
{
    const string portName = "COM3"; // ジャッキのぽーと
    const int baudRate = 9600;
    public SerialPort serialPort_ = new SerialPort(portName, baudRate);

    void Start()
    {

        serialPort_.Open();
    }


}