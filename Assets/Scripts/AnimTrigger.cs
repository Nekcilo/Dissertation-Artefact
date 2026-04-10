using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] Pouring PouringScript;

    public void Animation()
    {
        PouringScript.CupAnimator.SetBool("AnimIsPouring", true);
    }
}
