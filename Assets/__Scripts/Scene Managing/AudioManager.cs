using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public Transform inActiveAudioPool;
    public Transform activeAudioPool;
    public int numberOfAudioSources;

    public GameObject audioSource;

    public List<GameObject> audioSourceList = new List<GameObject>();

    public AudioMixer mixer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        if(inActiveAudioPool != null && activeAudioPool != null)
        {
            for (int i = 0; i < numberOfAudioSources; i++)
            {
                Vector3 audioSourcePos = new Vector3(0, 0, 0);
                GameObject newAudioSource = Instantiate(audioSource, audioSourcePos, Quaternion.identity, inActiveAudioPool);
                AddToList(newAudioSource);
            }
        }
    }
    public void SetAudioClip(AudioClip clip, Vector3 position, bool is2D = false)
    {
        GameObject openAudioSource = FindEmptyAudioClip();
        if(openAudioSource != null)
        {
            openAudioSource.transform.parent = activeAudioPool;
            openAudioSource.transform.position = position;

            AudioSource source = openAudioSource.GetComponent<AudioSource>();
            if (is2D) source.spatialBlend = 0;
            source.clip = clip;
            source.Play();

            StartCoroutine(WaitForSoundToEnd(source, openAudioSource, clip.length));
        }
    }
    public GameObject FindEmptyAudioClip()
    {
        GameObject openClip = null;

        for (int i = 0; i < audioSourceList.Count; i++)
        {
            if (audioSourceList[i].GetComponent<AudioSource>().clip == null)
            {
                openClip = audioSourceList[i];
                return openClip;
            }
        }
        return null;
    }
    public void AddToList(GameObject source)
    {
        audioSourceList.Add(source);
    }
    private IEnumerator WaitForSoundToEnd(AudioSource audioSourse, GameObject sourceObject, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        sourceObject.transform.parent = inActiveAudioPool;
        audioSourse.clip = null;
    }
    public void SetMusicVolume(int input)
    {
        mixer.SetFloat("Music Volume", input);
    }
    public void SetSoundEffectVolume(int input)
    {
        mixer.SetFloat("Sound Effect Volume", input);
        Debug.Log("sound effect changed" + input);
    }
}
