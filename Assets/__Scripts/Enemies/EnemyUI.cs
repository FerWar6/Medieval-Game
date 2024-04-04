using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer mark;
    [SerializeField] Sprite exclamationMark;
    [SerializeField] Sprite questionMark;

    public void UIOff()
    {
        mark.enabled = false;
    }

    public void SetQuestionMark()
    {
        mark.enabled = true;
        mark.sprite = questionMark;
    }
    public void SetExclamationMark()
    {
        mark.enabled = true;
        mark.sprite = exclamationMark;
    }
}
