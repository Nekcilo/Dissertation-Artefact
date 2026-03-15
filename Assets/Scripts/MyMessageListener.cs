using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] public bool ButtonPressed;
    [SerializeField] public int ButtonIdentifier;
    [SerializeField] public int RawRotation;

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        ButtonDetection(msg);
        RotationDetection(int.Parse(msg));
    }

    void ButtonDetection(string msg)
    {
        string Text = msg;

        if (Text == "Button 1 Pushed")
        {
            ButtonPressed = true;
            ButtonIdentifier = 1;
        }
        if (Text == "Button 2 Pushed")
        {
            ButtonPressed = true;
            ButtonIdentifier = 2;
        }
        if (Text == "Button 3 Pushed")
        {
            ButtonPressed = true;
            ButtonIdentifier = 3;
        }
        if (Text == "All 3 Button Unpushed")
        {
            ButtonPressed = false;
        }
    }

    void RotationDetection(int msg)
    {
        int value = msg;

        RawRotation = value;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}