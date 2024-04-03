using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSourceManager : MonoBehaviour
{
    public bool isActive = false;
    void Start()
    {
        //Destroy(gameObject, 3f);
    }
    public void SwitchStates()
    {
        isActive = !isActive;
    }
}
