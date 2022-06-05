using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }
}
