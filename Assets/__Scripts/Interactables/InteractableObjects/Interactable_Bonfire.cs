using UnityEngine;

public class Interactable_Bonfire : Interactable
{
    [SerializeField]
    private GameObject fire;
    private void Start()
    {
        fire.SetActive(false);
    }
    protected override void Interact()
    {
        fire.SetActive(true);
        Debug.Log("interacted");
    }
}
