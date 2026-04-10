using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{

    //[SerializeField] int pourthreshold = 5;
    //[SerializeField] Transform origin;
    [SerializeField] GameObject streamPrefab;

    //Script References
    [SerializeField] Order OrderScript;

    //Private
    private bool isPouring = false;
    private Stream currentStream = null;

    public void PourCheck(bool pourCheck)
    {
        //pourCheck = OrderScript.RotValue > pourthreshold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;
            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        Debug.Log("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        Debug.Log("End");
        currentStream.End();
        currentStream = null;
    }

    //private float CalculatePourAngle()
    //{
    //    //I think this function is redundant
    //    //angle value could be replaced with the potentiometer angle value

    //    return transform.forward.y * Mathf.Rad2Deg;
    //}

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, transform.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

}