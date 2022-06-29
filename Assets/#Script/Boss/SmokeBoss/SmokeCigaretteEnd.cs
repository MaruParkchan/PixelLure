using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCigaretteEnd : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 direction;

    public void Direction(Quaternion euler)
    {
        transform.rotation = euler; 
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
