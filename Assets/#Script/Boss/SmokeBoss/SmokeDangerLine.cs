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
            if (GameSystem.isAccept == false)
            {
                SpawnObject(-10.0f, 0);
            }
            else
            {
                int randomValue = Random.Range(0, 2);
                if (randomValue == 0)
                {
                    SpawnObject(-10.0f, 0);
                }
                else
                {
                    SpawnObject(10.0f, 180.0f);
                }
            }
        }
    }

    private void SpawnObject(float positionX, float rotateZvalue)
    {
        GameObject clone = Instantiate(dropObject);
        clone.GetComponent<SmokeCigaretteEnd>().Direction(Quaternion.Euler(0, 0, rotateZvalue));
        clone.transform.position = new Vector3(positionX, transform.position.y, 0);
    }
}
