using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class MoveToScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadSlider;
    public void MoveToSceneName()
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }
    public void MoveToMenu(int startScreenIndex)
    {
        SettingsManager.instance.startScreenIndex = startScreenIndex;
        Debug.Log("set startindex" + startScreenIndex);
        Debug.Log("set startindex scenemanager" + SettingsManager.instance.startScreenIndex);
        SceneManager.LoadScene("AllMenus");
    }

    private IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            loadSlider.value = operation.progress;
            yield return null;
        }
    }
}
