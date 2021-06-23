using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject boss;

    private bool isPause; // 게임 정지 ( 선택지 or 죽을시 (보스,플레이어))
    public bool IsPause
    {
        get { return isPause; }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            boss.GetComponent<ICoroutineStop>().CoroutineStop();
        }
    }

    public void GameResume() // 게임 재개
    {
        isPause = false;
    }

    public void GamePause() // 게임 정지
    {
        isPause = true;
    }

}
