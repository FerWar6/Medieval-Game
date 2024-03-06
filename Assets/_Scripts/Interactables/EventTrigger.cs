using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventTrigger : MonoBehaviour
{
    public void BaseTriggerEvent()
    {
        TriggerEvent();
    }
    protected virtual void TriggerEvent()
    {

    }
}