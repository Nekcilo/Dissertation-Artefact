using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] GameObject ScoreScreen;

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

    public Vector3 OriginalPos;
    public Color colorref;

    private void Awake()
    {
        OriginalPos = ScoreScreen.transform.position;

        //ColorGrading tempCol;
        //PostProcessVolume.profile.TryGetSettings<ColorGrading>(out tempCol);
        //colorref = tempCol.colorFilter.value;
        //Debug.Log(colorref);
    }

    public void SetUI(float GoodScoreValue, float BadScoreValue, int BonusOrders, int GoodOrders, int BadOrders, float TotalScoreValue)
    {
        GoodScore.text = DotCalculator(("Good Drinks "), (FindLength(GoodScore)), ("£" + GoodScoreValue.ToString("0.00")));
        BadScore.text = DotCalculator(("Bad Drinks "), (FindLength(BadScore)), ("-£" + BadScoreValue.ToString("0.00")));

        BonusCount.text = DotCalculator(("Bonus Orders "), (FindLength(BonusCount)), BonusOrders.ToString());
        GoodCount.text = DotCalculator(("Successful Orders "), (FindLength(GoodCount)), GoodOrders.ToString());
        BadCount.text = DotCalculator(("Unsuccessful Orders "), (FindLength(BadCount)), BadOrders.ToString());

        TotalScore.text = "Total: £" + TotalScoreValue.ToString("0.00");
    }

    public void ShowUI()
    {
        PostProcessVolume.enabled = true;
        ScoreScreen.SetActive(true);
        
        StartCoroutine(SmoothBlur(1.0f));

        StartCoroutine(SmoothMove(new Vector3(OriginalPos.x, OriginalPos.y - 175f, OriginalPos.z), OriginalPos, 1.0f));
    }

    public void HideUI()
    {
        ScoreScreen.SetActive(false);
        PostProcessVolume.enabled = false;
    }

    IEnumerator SmoothBlur(float seconds)
    {
        float t = 0.0f;
        
        DepthOfField tempDof;
        ColorGrading tempCol;

        PostProcessVolume.profile.TryGetSettings<DepthOfField>(out tempDof);
        PostProcessVolume.profile.TryGetSettings<ColorGrading>(out tempCol);

        while (t <= 1.0f)
        {
            t += Time.deltaTime / seconds;

            tempDof.focusDistance.value = Mathf.Lerp(5f, 0f, Mathf.SmoothStep(0.0f, 1.0f, t));
            tempCol.colorFilter.value = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(0.766f, 0.658f, 0.572f, 1f), t);

            yield return null;
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / seconds;
            ScoreScreen.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }

    float FindLength(TextMeshProUGUI textComponent)
    {
        float length = 0;

        textComponent.ForceMeshUpdate(true);

        Debug.Log("[" + textComponent.name + "]" + " Character Count: " + textComponent.textInfo.characterCount);

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
