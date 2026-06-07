using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    //Public Variables
    [HideInInspector] public bool ButtonPressed;
    [HideInInspector] public int ButtonIdentifier;
    [HideInInspector] public int RawRotation;
    [HideInInspector] public string NFCID;

    //Script References
    [Header("Script References")]
    [SerializeField] Hardware HardwareScript;
    [SerializeField] Order OrderScript;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        switch (msg)
        {
            case "Button 1 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 1;
                HardwareScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
                break;

            case "Button 2 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 2;
                HardwareScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
                break;

            case "Button 3 Pushed":
                ButtonPressed = true;
                ButtonIdentifier = 3;
                HardwareScript.ButtonCheck(ButtonPressed, ButtonIdentifier);
                break;

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
            HardwareScript.Rotation(RawRotation);
        }
        else if (msg.Substring(0, 20) == "Reader 1: Card UID: ")
        {
            int length = msg.Length - 20;
            NFCID = msg.Substring(20, length);
            HardwareScript.NFC(NFCID, 1);
        }
        else if (msg.Substring(0, 20) == "Reader 2: Card UID: ")
        {
            int length = msg.Length - 20;
            NFCID = msg.Substring(20, length);
            HardwareScript.NFC(NFCID, 2);
        }
        else
        {
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