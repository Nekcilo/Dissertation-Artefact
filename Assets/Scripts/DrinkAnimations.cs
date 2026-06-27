using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkAnimations : MonoBehaviour
{
    [SerializeField] VisualDrink VisualDrinkScript;
    [SerializeField] VisualIngredientSwitcher VisualIngredientScript;
    [SerializeField] VisualLiquidSwitcher VisualLiquidScript;

    [SerializeField] Hardware HardwareScript;

    Vector3 MugOriginalPos;
    Vector3 CupOriginalPos;

    //Offsets
    Vector3 mugOffsetRight;
    Vector3 cupOffsetRight;
    Vector3 mugOffsetLeft;
    Vector3 cupOffsetLeft;


    float duration = 1.0f;
    public bool isMoveFinished;

    private void Awake()
    {
        MugOriginalPos = VisualDrinkScript.Mug.transform.position;
        CupOriginalPos = VisualDrinkScript.Cup.transform.position;

        mugOffsetRight = new Vector3(MugOriginalPos.x + 15f, MugOriginalPos.y, MugOriginalPos.z);
        cupOffsetRight = new Vector3(CupOriginalPos.x + 15f, CupOriginalPos.y, CupOriginalPos.z);

        mugOffsetLeft = new Vector3(MugOriginalPos.x - 15f, MugOriginalPos.y, MugOriginalPos.z);
        cupOffsetLeft = new Vector3(CupOriginalPos.x - 15f, CupOriginalPos.y, CupOriginalPos.z);
    }

    public void EnterDrinkAnim()
    {
        VisualIngredientScript.currentIngredientSR.color = new Color32(255, 255, 255, 0);

        if (HardwareScript.SelectedVessel == "Mug")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Mug, VisualDrinkScript.Mug.transform.position, mugOffsetLeft, duration, false));
        }
        if (HardwareScript.SelectedVessel == "Cup")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Cup, VisualDrinkScript.Cup.transform.position, cupOffsetLeft, duration, false));
        }
    }

    public void ExitDrinkAnim()
    {
        VisualIngredientScript.currentIngredientSR.color = new Color32(255, 255, 255, 255);

        if (HardwareScript.SelectedVessel == "Mug")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Mug, VisualDrinkScript.Mug.transform.position, mugOffsetRight, duration, true));
        }
        if (HardwareScript.SelectedVessel == "Cup")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Cup, VisualDrinkScript.Cup.transform.position, cupOffsetRight, duration, true));
        }
    }

    IEnumerator SmoothMove(GameObject vessel, Vector3 startpos, Vector3 endpos, float seconds, bool isEnter)
    {
        if (isEnter)
        {
            float t = 0.0f;

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;

                vessel.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));

                yield return null;
            }

            StartCoroutine(SmoothFade(0.1f, isEnter));
            isMoveFinished = true;
        }
        else if (!isEnter)
        {
            float t = 0.0f;

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;

                vessel.transform.position = Vector3.Lerp(endpos, startpos, Mathf.SmoothStep(0.0f, 1.0f, t));

                yield return null;
            }

            StartCoroutine(SmoothFade(0.1f, isEnter));
            isMoveFinished = false;
        }
    }

    IEnumerator SmoothFade(float seconds, bool isEnter)
    {
        if (isEnter)
        {
            float t = 0.0f;

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;

                VisualIngredientScript.currentIngredientSR.color = new Color32(255, 255, 255, (byte)Mathf.Lerp(255, 0, Mathf.SmoothStep(0.0f, 1.0f, t)));

                yield return null;
            }

            isMoveFinished = true;
        }
        else if (!isEnter)
        {
            float t = 0.0f;

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;

                VisualIngredientScript.currentIngredientSR.color = new Color32(255, 255, 255, (byte)Mathf.Lerp(0, 255, Mathf.SmoothStep(0.0f, 1.0f, t)));

                yield return null;
            }

            isMoveFinished = false;
        }
    }
}
