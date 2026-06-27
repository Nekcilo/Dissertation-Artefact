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
    Vector3 mugOffsetPos;
    Vector3 cupOffsetPos;


    float duration = 1.0f;
    bool isMoveFinished;

    private void Awake()
    {
        MugOriginalPos = VisualDrinkScript.Mug.transform.position;
        CupOriginalPos = VisualDrinkScript.Cup.transform.position;

        mugOffsetPos = new Vector3(MugOriginalPos.x + 15f, MugOriginalPos.y, MugOriginalPos.z);
        cupOffsetPos = new Vector3(CupOriginalPos.x + 15f, CupOriginalPos.y, CupOriginalPos.z);
    }

    public void EnterDrinkAnim()
    {
        if (HardwareScript.SelectedVessel == "Mug")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Mug, VisualDrinkScript.Mug.transform.position, mugOffsetPos, duration, false));
        }
        if (HardwareScript.SelectedVessel == "Cup")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Cup, VisualDrinkScript.Cup.transform.position, cupOffsetPos, duration, false));
        }
    }

    public void ExitDrinkAnim()
    {
        if (HardwareScript.SelectedVessel == "Mug")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Mug, VisualDrinkScript.Mug.transform.position, mugOffsetPos, duration, true));
        }
        if (HardwareScript.SelectedVessel == "Cup")
        {
            StartCoroutine(SmoothMove(VisualDrinkScript.Cup, VisualDrinkScript.Cup.transform.position, cupOffsetPos, duration, true));
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

                VisualIngredientScript.currentSpriteRenderer.color = new Color32(255, 255, 255, (byte) Mathf.Lerp(255, 0, Mathf.SmoothStep(0.0f, 1.0f, t)));

                vessel.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));
                
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

                VisualIngredientScript.currentSpriteRenderer.color = new Color32(255, 255, 255, (byte)Mathf.Lerp(0, 255, Mathf.SmoothStep(0.0f, 1.0f, t)));

                vessel.transform.position = Vector3.Lerp(endpos, startpos, Mathf.SmoothStep(0.0f, 1.0f, t));

                yield return null;
            }

            isMoveFinished = false;
        }
    }
}
