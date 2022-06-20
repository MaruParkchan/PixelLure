using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroButtonEffect : MonoBehaviour
{
    // 클릭 했을시 반짝 거리는 효과를 위한 스크립트 

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
