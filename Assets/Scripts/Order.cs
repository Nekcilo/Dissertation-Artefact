using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    [SerializeField] TMP_Text OrderLine1;
    [SerializeField] TMP_Text OrderLine2;

    // Start is called before the first frame update
    void Start()
    {
        OrderLine1.text = "Test 1";
        OrderLine2.text = "Test 2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
