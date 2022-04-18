using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHpUISystem : MonoBehaviour
{
    public static Action playerUISystem;

    private int playerHpIndexCount = 3;
    [SerializeField] private GameObject[] playerHpImages;

    private void Awake()
    {
        playerUISystem = () => { PlayerHpDecline(); };
    }

    public void PlayerHpDecline() // �÷��̾� Hp ����
    {
        Debug.Log("��");
        if (playerHpIndexCount == 0)
            return;

        playerHpImages[playerHpIndexCount - 1].GetComponent<Animator>().SetTrigger("PlayerHpDecline");
        StartCoroutine(PlayerHpDeclineSetActive());
        playerHpIndexCount--;
    }

    IEnumerator PlayerHpDeclineSetActive()
    {
        yield return new WaitForSeconds(1.0f);
        playerHpImages[playerHpIndexCount].SetActive(false);
    }
}
