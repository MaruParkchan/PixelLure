using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : MonoBehaviour
{
    private float timer = 1.1f;

    private void OnEnable()
    {
        StartCoroutine("SetActiveOff");
    }

    IEnumerator SetActiveOff()
    {
        yield return new WaitForSeconds(timer);
        this.gameObject.SetActive(false);
    }
}
