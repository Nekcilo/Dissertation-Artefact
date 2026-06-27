using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] CanvasGroup ScoreScreen;

    //PostProcessing
    [SerializeField] PostProcessVolume PostProcessVolume;

    //UI Text
    [Header("Score Values")]
    [SerializeField] TextMeshProUGUI GoodScore;
    [SerializeField] TextMeshProUGUI BadScore;
    [SerializeField] TextMeshProUGUI TotalScore;

    [Header("Drink Count Values")]
    [SerializeField] TextMeshProUGUI BonusCount;
    [SerializeField] TextMeshProUGUI GoodCount;
    [SerializeField] TextMeshProUGUI BadCount;

    [SerializeField] Image CooldownIndicator;

    [SerializeField] GameTimer GameTimerScript;

    public Vector3 OriginalPos;
    Vector3 OffsetPos;

    bool isMoveFinished = false;

    float maximum = 255f;
    float minimum = 0f;
    float t = 0.0f;

    private void Awake()
    {
        OriginalPos = ScoreScreen.transform.position;

        OffsetPos = new Vector3(OriginalPos.x, OriginalPos.y - 175f, OriginalPos.z);

        ScoreScreen.transform.position = OffsetPos;
    }

    public void SetUI(float GoodScoreValue, float BadScoreValue, int BonusOrders, int GoodOrders, int BadOrders, float TotalScoreValue)
    {
        GoodScore.text = DotCalculator(("Good Drinks "), (FindLength(GoodScore)), ("£" + GoodScoreValue.ToString("0.00")));
        
        if (BadScoreValue < 0.01)
        {
            BadScore.text = DotCalculator(("Bad Drinks "), (FindLength(BadScore)), ("£" + BadScoreValue.ToString("0.00")));
        }
        else
        {
            BadScore.text = DotCalculator(("Bad Drinks "), (FindLength(BadScore)), ("-£" + BadScoreValue.ToString("0.00")));
        }

        BonusCount.text = DotCalculator(("Bonus Orders "), (FindLength(BonusCount)), BonusOrders.ToString());
        GoodCount.text = DotCalculator(("Successful Orders "), (FindLength(GoodCount)), GoodOrders.ToString());
        BadCount.text = DotCalculator(("Unsuccessful Orders "), (FindLength(BadCount)), BadOrders.ToString());

        if (TotalScoreValue < 0)
        {
            TotalScore.text = "Total: -£" + (-TotalScoreValue).ToString("0.00");
        }
        else
        {
            TotalScore.text = "Total: £" + TotalScoreValue.ToString("0.00");
        }
    }

    public void ShowUI()
    {
        PostProcessVolume.enabled = true;

        CooldownIndicator.color = new Color32(255, 255, 255, 0);

        StartCoroutine(SmoothBlur(1.0f, true));
        StartCoroutine(SmoothMove(OffsetPos, OriginalPos, 1.0f, true));
    }

    public void HideUI()
    {
        if (isMoveFinished)
        {
            StartCoroutine(SmoothBlur(1.0f, false));
            StartCoroutine(SmoothMove(OffsetPos, OriginalPos, 1.0f, false));
        }
        else if (!isMoveFinished)
        {
            ScoreScreen.alpha = 0;
            PostProcessVolume.enabled = false;
        }
    }

    IEnumerator SmoothBlur(float seconds, bool isEnter)
    {
        DepthOfField tempDof;
        ColorGrading tempCol;

        PostProcessVolume.profile.TryGetSettings<DepthOfField>(out tempDof);
        PostProcessVolume.profile.TryGetSettings<ColorGrading>(out tempCol);

        if (isEnter)
        {
            float t = 0.0f;
            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;

                tempDof.focusDistance.value = Mathf.Lerp(4.5f, 0.1f, Mathf.SmoothStep(0.0f, 1.0f, t));
                tempCol.colorFilter.value = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(0.766f, 0.658f, 0.572f, 1f), Mathf.SmoothStep(0.0f, 1.0f, t));

                yield return null;
            }
        }
        else if (!isEnter)
        {
            float t = 0.0f;

            yield return new WaitForSeconds(0.15f);

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;

                tempDof.focusDistance.value = Mathf.Lerp(0.1f, 4.5f, Mathf.SmoothStep(0.0f, 1.0f, (t * 3f)));
                tempCol.colorFilter.value = Color.Lerp(new Color(0.766f, 0.658f, 0.572f, 1f), new Color(1f, 1f, 1f, 1f), Mathf.SmoothStep(0.0f, 1.0f, (t * 3f)));

                yield return null;
            }
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds, bool isEnter)
    {   
        if (isEnter)
        {
            float t = 0.0f;

            yield return new WaitForSeconds(0.35f);

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;
                ScoreScreen.alpha = Mathf.Lerp(0f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
                ScoreScreen.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));
                yield return null;
            }

            isMoveFinished = true;
        }
        else if (!isEnter)
        {
            float t = 0.0f;
            ScoreScreen.transform.position = endpos;

            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;
                ScoreScreen.alpha = Mathf.Lerp(1f, 0f, Mathf.SmoothStep(0.0f, 1.0f, (t * 3f)));
                yield return null;
            }

            isMoveFinished = false;
        }
    }

    public IEnumerator CooldownFade() //Needs to be revisited
    {
        float seconds = 2.5f;

        Debug.Log("Cooldown Fade run");

        while (!GameTimerScript.Replay)
        {
            // animate the position of the game object...
            CooldownIndicator.color = new Color32(255, 255, 255, (byte) Mathf.Lerp(minimum, maximum, t));

            t += Time.deltaTime / seconds;

            if (t > 1.0f)
            {
                float temp = maximum;
                maximum = minimum;
                minimum = temp;

                t = 0.0f;
            }
            
            yield return null;
        }

        CooldownIndicator.color = new Color32(255, 255, 255, 0);
    }

    float FindLength(TextMeshProUGUI textComponent)
    {
        float length = 0;

        textComponent.ForceMeshUpdate(true);

        //Debug.Log("[" + textComponent.name + "]" + " Character Count: " + textComponent.textInfo.characterCount);

        for (int i = 0; length < textComponent.textInfo.characterCount; ++i)
        {
            length++;
        }

        return length;
    }

    string DotCalculator(string textStart, float targetLength, string scoreText)
    {
        while ((textStart.Length + scoreText.ToString().Length) < targetLength)
        {
            textStart += ".";
        }

        return textStart + scoreText;
    }
}
