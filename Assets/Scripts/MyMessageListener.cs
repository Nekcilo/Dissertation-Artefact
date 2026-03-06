using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    public bool ButtonPressed;

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        float Text = float.Parse(msg);

        if (Text == 0)
        {
            ButtonPressed = true;
        }
        if (Text == 1)
        {
            ButtonPressed = false;
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