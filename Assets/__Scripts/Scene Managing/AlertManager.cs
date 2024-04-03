using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager instance { get; private set; }

    public Transform inActiveAlertPool;
    public Transform activeAlertPool;
    public int numberOfAlertSources;

    public GameObject alertSource;

    public List<GameObject> alertSourceList = new List<GameObject>();

    Vector3 baseAlertPos = new Vector3(0, 200, 0);
    public float returnAlertCooldown = 5f;

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
    void Start()
    {
        for (int i = 0; i < numberOfAlertSources; i++)
        {
            GameObject newAudioSource = Instantiate(alertSource, baseAlertPos, Quaternion.identity, inActiveAlertPool);
            AddToList(newAudioSource);
        }
    }
    public void SetAlertPos(Vector3 position)
    {
        GameObject openAlert = FindEmptyAudioClip();
        if (openAlert != null)
        {
            openAlert.transform.parent = activeAlertPool;
            openAlert.transform.position = position;

            openAlert.GetComponent<AlertSourceManager>().isActive = true;

            StartCoroutine(ReturnAlertSource(openAlert));
        }
    }
    public GameObject FindEmptyAudioClip()
    {
        GameObject openAlert = null;

        for (int i = 0; i < alertSourceList.Count; i++)
        {
            if (alertSourceList[i].GetComponent<AlertSourceManager>().isActive == false)
            {
                openAlert = alertSourceList[i];
                return openAlert;
            }
        }
        return null;
    }
    private IEnumerator ReturnAlertSource(GameObject sourceObject)
    {
        yield return new WaitForSeconds(returnAlertCooldown);

        sourceObject.transform.parent = inActiveAlertPool;
        sourceObject.transform.position = baseAlertPos;
        GetComponent<AlertSourceManager>().isActive = false;
    }
    public void AddToList(GameObject source)
    {
        alertSourceList.Add(source);
    }
}
