using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DogBossPatternBase : MonoBehaviour
{
    protected Animator dogBossAnimator;
    protected DogBoss dogBoss;
    protected float waitTime = 2.0f;
    protected Transform mainCamera;

    //[SerializeField]
    //protected int attackCount; // 공격 횟수
    //[SerializeField]
    //protected float attackDelayTime; // 재사용 공격 대기시간

    private void Start()
    {
        dogBossAnimator = GetComponent<Animator>();
        dogBoss = GetComponent<DogBoss>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        Init();
    }
    protected void CameraRotate()
    {
        if(GameSystem.isAccept == true)
            return;

        int index = Random.Range(0, 2);
        if (index == 0)
            mainCamera.rotation = Quaternion.Euler(0, 0, 180.0f);
        if (index == 1)
            mainCamera.rotation = Quaternion.Euler(0, 0, 0.0f);
    }
    protected abstract void Init();
    public abstract IEnumerator Attacking();
}
