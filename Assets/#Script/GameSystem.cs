using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject boss;

    private bool isPause; // ���� ���� ( ������ or ������ (����,�÷��̾�))
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

    public void GameResume() // ���� �簳
    {
        isPause = false;
    }

    public void GamePause() // ���� ����
    {
        isPause = true;
    }

}
