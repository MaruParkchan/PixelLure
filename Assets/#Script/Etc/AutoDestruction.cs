using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    // 영역밖으로 나가면 자동적으로 오브젝트 삭제 코드
    [SerializeField]
    private MapData mapDataDestroyArea; // 맵 영역 안

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
