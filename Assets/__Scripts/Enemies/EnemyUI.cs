using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer exclamationMark;

    public void SetExclamationMark(bool on)
    {
        exclamationMark.enabled = on;
    }
}
