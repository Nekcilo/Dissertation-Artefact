using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] public bool ButtonPressed;
    [SerializeField] public int ButtonIdentifier;
    [SerializeField] public int RawRotation;
    [SerializeField] public int NFCID;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        switch (msg)
        {
            case "Button 1 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 1;
                break;

            case "Button 2 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 2;
                break;

            case "Button 3 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 3;
                break;

            case "All 3 Button Unpushed":
                ButtonPressed = false;
                break;

            default:
                Detector(msg);
                //RawRotation = int.Parse(msg);
                break;
        }
    }
    
    void Detector(string msg)
    {
        if (msg.Substring(0, 7) == "NFC ID:")
        {
            int length = msg.Length - 7;
            NFCID = int.Parse(msg.Substring(7, length));
        }
        else if (msg.Substring(0, 9) == "Rotation:")
        {
            int length = msg.Length - 9;
           RawRotation = int.Parse(msg.Substring(9, length));
        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}