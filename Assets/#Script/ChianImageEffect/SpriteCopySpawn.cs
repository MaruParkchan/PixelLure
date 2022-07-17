using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCopySpawn : MonoBehaviour
{
    [SerializeField] private GameObject imageObject;
    [SerializeField] private Sprite[] sprites;
    private float speed;

    private void Start()
    {
       SpriteInit();
    }
    private void SpriteInit()
    {
        GameObject[] clone = new GameObject[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            clone[i] = Instantiate(imageObject);
            clone[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone[i].GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.4f,1.5f);
            clone[i].GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * Random.Range(-3.0f, 3.0f);
            clone[i].transform.position = this.gameObject.transform.position;
        }
    }
}
