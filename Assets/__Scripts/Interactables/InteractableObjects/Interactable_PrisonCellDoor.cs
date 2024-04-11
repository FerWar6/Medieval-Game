using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_PrisonCellDoor : MonoBehaviour, IInteractable
{
    public string promptMessage { get { return message; } }

    [SerializeField] private string message = "Requires Key";

    private Animator anim;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip doorCantOpen;
    [SerializeField] AudioClip doorOpen;

    bool hasKeyItem = false;
    private void Start()
    {
        PlayerData.instance.OnInventoryUpdate.AddListener(UpdateText);

        anim = GetComponent<Animator>();
    }
    public void Interact()
    {
        if (!hasKeyItem)
        {
            PlaySound(doorCantOpen);
            StartCoroutine(SetAnimationTrigger(0.1f, "DoorStuck"));
        }
        if (hasKeyItem)
        {
            message = null;
            anim.SetTrigger("OpenDoor");
            StartCoroutine(PlaySoundWithDelay(0.08f, doorOpen));
        }
    }
    private void UpdateText()
    {
        if (PlayerData.instance.ListContainsItemByName("Keyitem_PrisonCellKey"))
        {
            message = "Open Door";
            hasKeyItem = true;
        }
    }
    private IEnumerator SetAnimationTrigger(float delay, string triggerName)
    {
        yield return new WaitForSeconds(delay);
        anim.SetTrigger(triggerName);
    }
    private void DestroyInteractable()
    {
        Destroy(GetComponent<BoxCollider>());

        Destroy(this);
    }
    private void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    
    private IEnumerator PlaySoundWithDelay(float delay, AudioClip clip)
    {
        yield return new WaitForSeconds(delay);

        PlaySound(clip);
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnInventoryUpdate.RemoveListener(UpdateText);
    }
}
