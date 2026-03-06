using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;
    int num;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OrderLine1.text = ("Line 1: " + num++);
            OrderLine2.text = ("Test 2: " + num++);
        }
    }
}
