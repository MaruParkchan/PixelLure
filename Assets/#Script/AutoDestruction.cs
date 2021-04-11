using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    // ���������� ������ �ڵ������� ������Ʈ ���� �ڵ�
    [SerializeField]
    private MapData mapDataDestroyArea; // �� ���� ��

    private void Update()
    {
        if(transform.position.x < mapDataDestroyArea.LimitMin.x ||
           transform.position.x > mapDataDestroyArea.LimitMax.x ||
           transform.position.y > mapDataDestroyArea.LimitMax.y ||
           transform.position.y < mapDataDestroyArea.LimitMin.y)
        {
            Destroy(this.gameObject);
        }

    }
}
