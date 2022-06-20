using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float waitTime; // 씬 넘어갈 대기시간 

    AsyncOperation asyncOper;

    public void SceneActivation()
    {
        asyncOper.allowSceneActivation = true;
    }

    public void StartLoadScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        asyncOper = SceneManager.LoadSceneAsync(sceneName);
        asyncOper.allowSceneActivation = false;
        StartCoroutine("SceneWaitTime");
        while (!asyncOper.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator SceneWaitTime()
    {
        yield return new WaitForSeconds(waitTime);
        SceneActivation(); // 씬 로딩 가동
    }
}
