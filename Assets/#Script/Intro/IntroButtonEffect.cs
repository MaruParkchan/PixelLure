using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroButtonEffect : MonoBehaviour
{
    // Ŭ�� ������ ��¦ �Ÿ��� ȿ���� ���� ��ũ��Ʈ 

    [SerializeField] private GameObject introButtonObject;
    [SerializeField] private GameObject redChainObject;
    [SerializeField] private GameObject blueChainObject;

    public void StartButton()
    {
        introButtonObject.GetComponent<TwinkleEffect>().StartTwinkle();
        redChainObject.GetComponent<TwinkleEffect>().StartTwinkle();
        blueChainObject.GetComponent<TwinkleEffect>().StartTwinkle();
    }
}
