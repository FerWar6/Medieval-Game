using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private string sceneName = "AllMenus";
    private bool canLoad = false;
    public void LoadNextScene()
    {
        SceneManager.LoadScene("AllMenus");
    }
    private IEnumerator LoadSceneAsynchronously()
    {
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone && !canLoad)
        {
            yield return null;
        }
        while(operation.isDone && !canLoad)
        {
            yield return null;
        }
    }
}


