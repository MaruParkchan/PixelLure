using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSojuBreak : MonoBehaviour
{
    [SerializeField]
    private GameObject sojuEffect;

    public void SojuEffect()
    {
        GameObject clone = Instantiate(sojuEffect);
        clone.transform.position = this.transform.position;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
