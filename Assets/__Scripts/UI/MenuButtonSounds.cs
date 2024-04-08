using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSounds : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> buttonSounds = new List<AudioClip>();

    public void PlaySoundOnClick(int clipSoundIndex)
    {
        if(buttonSounds[clipSoundIndex] != null)
        {
            source.clip = buttonSounds[clipSoundIndex];
            source.Play();
        }
    }
}
