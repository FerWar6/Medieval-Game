using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Image leftArrow;
    [SerializeField] private Image rightArrow;
    public void ShowArrows()
    {
        if (leftArrow.enabled == true)
        {
            leftArrow.enabled = false;
            rightArrow.enabled = false;
        }
        else if (leftArrow.enabled == false)
        {
            leftArrow.enabled = true;
            rightArrow.enabled = true;
        }
    }
}
