using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackController : BaseArduinoController
{
    void Start()
    {
        PortOpen();
    }

    void Update()
    {
        if (GameController.IsTestMode)
        {
            if (Input.GetKeyDown(KeyCode.U)) JackUp();
            else if (Input.GetKeyDown(KeyCode.D)) JackDown();
            else if (Input.GetKeyDown(KeyCode.S)) JackStop();
        }
    }

    public void JackUp()
    {
        serialPort_.Write("u");
    }
    
    public void JackDown()
    {
        serialPort_.Write("d");
    }
    
    public void JackStop()
    {
        serialPort_.Write("s");
    }
    
}
