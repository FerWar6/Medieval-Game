using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("interacted");
    }
}
