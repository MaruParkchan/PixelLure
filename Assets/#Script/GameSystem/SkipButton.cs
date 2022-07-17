using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkipButton : MonoBehaviour
{
    private bool isSkip = false;
    [SerializeField] private GameObject blinkObject;
    [SerializeField] private string sceneName;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneSkip();
        }
    }

    private void SceneSkip()
    {
        if (isSkip)
            return;
        StartCoroutine("Skip");
    }

    IEnumerator Skip()
    {
        isSkip = true;
        blinkObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
