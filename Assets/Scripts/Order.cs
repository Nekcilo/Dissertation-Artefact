using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;
    [SerializeField] MyMessageListener MessageListener;
    int num;

    // Update is called once per frame
    void Update()
    {
        if (MessageListener != null)
        {
            ButtonCheck(); 
            Rotation();
        }
    }

    void ButtonCheck()
    {
        if (MessageListener.ButtonPressed == true && MessageListener.ButtonIdentifier == 1)
        {
            Button1Func();
        }
        if (MessageListener.ButtonPressed == true && MessageListener.ButtonIdentifier == 2)
        {
            Button2Func();
        }
        if (MessageListener.ButtonPressed == true && MessageListener.ButtonIdentifier == 3)
        {
            Button3Func();
        }
        if (MessageListener.ButtonPressed == false)
        {
            //Debug.Log("No Button Pressed");
        }
    }

    void Button1Func()
    {
        OrderLine1.text = ("Line 1: " + num++);
        //Debug.Log("Button 1 Pressed");
    }

    void Button2Func()
    {
        OrderLine2.text = ("Line 2: " + num++);
        //Debug.Log("Button 2 Pressed");
    }

    void Button3Func()
    {
        Debug.Log("Button 3 Pressed");
    }

    void Rotation()
    {
        Debug.Log("Rotation: " + MessageListener.RawRotation * 360 / 1023);
    }

}
