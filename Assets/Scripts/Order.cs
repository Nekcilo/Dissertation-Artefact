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
            if (MessageListener.ButtonPressed == true && MessageListener.ButtonIdentifier == 1)
            {
                OrderLine1.text = ("Line 1: " + num++);
                Debug.Log("Button 1 Pressed");
            }
            if (MessageListener.ButtonPressed == true && MessageListener.ButtonIdentifier == 2)
            {
                OrderLine2.text = ("Line 2: " + num++);
                Debug.Log("Button 2 Pressed");
            }
            if (MessageListener.ButtonPressed == true && MessageListener.ButtonIdentifier == 3)
            {
                //OrderLine3.text = ("Line 3: " + num++);
                Debug.Log("Button 3 Pressed");
            }
            if (MessageListener.ButtonPressed == false)
            {
                //Debug.Log("No Button Pressed");
            }
        }
    }
}
