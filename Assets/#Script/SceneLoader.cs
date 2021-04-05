using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    AsyncOperation asyncOper;

    public void SceneActivation()
    {
        asyncOper.allowSceneActivation = true;
    }

    public IEnumerator LoadScene(string sceneName)
    {
        asyncOper = SceneManager.LoadSceneAsync(sceneName);
        asyncOper.allowSceneActivation = false;
        while (!asyncOper.isDone)
        {
            yield return null;
            Debug.Log(asyncOper.progress);
        }
    }


}
