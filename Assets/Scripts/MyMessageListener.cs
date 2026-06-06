using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    //Public Variables
    [SerializeField] public bool ButtonPressed;
    [SerializeField] public int ButtonIdentifier;
    [SerializeField] public int RawRotation;
    [SerializeField] public string NFCID;

    //Script References
    [SerializeField] Order OrderScript;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        switch (msg)
        {
            case "Button 1 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 1;
                OrderScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
                break;

            case "Button 2 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 2;
                OrderScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
                break;

            case "Button 3 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 3;
                OrderScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
                break;

            //case "All 3 Button Unpushed":
            //    ButtonPressed = false;
            //    OrderScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
            //    break;

            default:
                Detector(msg);
                break;
        }
    }
    
    void Detector(string msg)
    {
      
        if (msg.Substring(0, 9) == "Rotation:")
        {
            int length = msg.Length - 9;
            RawRotation = int.Parse(msg.Substring(9, length));
            OrderScript.Rotation(RawRotation);
        }
        else if (msg.Substring(10, 21) == "Card UID: ")
        {
            int length = msg.Length - 21;
            NFCID = msg.Substring(21, length);
            OrderScript.NFC(NFCID);
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