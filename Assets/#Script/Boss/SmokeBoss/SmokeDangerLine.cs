using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDangerLine : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private GameObject dropObject;
    [SerializeField] private bool isDropAttack;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }
    private void OnDestroy()
    {
        if (isDropAttack)
        {
            GameObject clone = Instantiate(dropObject);
            clone.transform.position = new Vector3(transform.position.x, 6.5f, 0);
        }
        else
        {
            GameObject clone = Instantiate(dropObject);
            clone.transform.position = new Vector3(-10.0f, transform.position.y, 0);
        }
    }
}
