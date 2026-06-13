using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] Hardware HardwareScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HardwareScript.ResetAnim();
            HardwareScript.ResetRound();
        }
    }
}
