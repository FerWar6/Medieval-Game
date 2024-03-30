using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    public void ChangeSpriteEnter()
    {
        SetOpacity(0.3f);
    }
    public void ChangeSpriteExit()
    {
        SetOpacity(0);
    }
    public void SetOpacity(float opacityValue)
    {
        Color color = sr.color;

        color.a = opacityValue;

        sr.color = color;
    }
}
