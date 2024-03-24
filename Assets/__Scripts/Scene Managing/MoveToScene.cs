using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void MoveToSceneName()
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName);
    }
}
