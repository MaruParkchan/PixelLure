using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private PlayerData playerData;

    private void Update()
    {
        Movement();
        LimitPosition();
        Rotate();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * playerData.MoveSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - this.transform.position.x, mousePosition.y - this.transform.position.y);

        transform.up = direction;
    }

    private void LimitPosition() // 이동제한 
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mapData.LimitMin.x, mapData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, mapData.LimitMin.y, mapData.LimitMax.y));
    }
}
