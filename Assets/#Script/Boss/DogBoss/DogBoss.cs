using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : Boss
{
    private DogBubblePattern dogBubblePattern;
    private DogSmallSojuPattern dogSmallSojuPattern;

    private DogBigTracePattern dogBigTracePattern;
    private DogBigLaserPattern dogBigLaserPattern;
    private DogBigPoundingPattern dogBigPoundingPattern;

    [SerializeField]
    private bool isBulkUp; // 벌크업하였는가?
    public DogBossData dogBossData;
    public DogBossData phase1DogBossData;
    public DogBossData phase2DogBossData;


    #region 콜라이더 변화값
    private float smallDogBossCircleColliderOffsetY;
    private float smallDogBossCircleColliderRadius;

    private float bigDogBossCirCleColliderOffsetY;
    private float bigDogBossCirCleColliderRadius;
    #endregion
    private CircleCollider2D circleCollider2D;

    [SerializeField] private AudioClip miniDogSoundClip;
    [SerializeField] private AudioClip bigDogSoundClip;


    private void Start()
    {
        dogBubblePattern = GetComponent<DogBubblePattern>();
        dogSmallSojuPattern = GetComponent<DogSmallSojuPattern>();
        dogBigTracePattern = GetComponent<DogBigTracePattern>();
        dogBigLaserPattern = GetComponent<DogBigLaserPattern>();
        dogBigPoundingPattern = GetComponent<DogBigPoundingPattern>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        BigBossCircleColliderPositionAndSizeData();
        CircleColliderInit(smallDogBossCircleColliderOffsetY, smallDogBossCircleColliderRadius);

        patternRandomValue = new int[3]; // 선택지 선택후에 랜덤 패턴을 위한 값 크기는 패턴의 수 만큼 조정해야함       
        PhaseChange(phase1DogBossData);
        AudioClipChangeAndPlay(miniDogSoundClip);
    }

    private void PhaseChange(DogBossData dogBossData)
    {
        this.dogBossData = dogBossData;
    }

    private void BigBossCircleColliderPositionAndSizeData() // 개 보스 벌크업하면 콜라이더 변화값 
    {
        smallDogBossCircleColliderOffsetY = 0.2f;
        smallDogBossCircleColliderRadius = 0.27f;
        bigDogBossCirCleColliderOffsetY = 1.0f;
        bigDogBossCirCleColliderRadius = 0.3f;

    }

    private void CircleColliderInit(float offsetY, float radius) // 콜라이더 크기, 위치 변화
    {
        circleCollider2D.offset = new Vector2(0, offsetY);
        circleCollider2D.radius = radius;
    }

    protected override IEnumerator Phase1()
    {
        yield return new WaitForSeconds(3.0f);
        ColliderEnableOn();
        IsisInvincibilityOff();
        while (true)
        {
            if (dogBossData.isP1 == true)
                yield return StartCoroutine(dogBubblePattern.Attacking());
            if (dogBossData.isP2 == true)
                yield return StartCoroutine(dogSmallSojuPattern.Attacking());

            if (!dogBossData.isP1 && !dogBossData.isP2)
                dogBossData.isP1 = true;

            yield return null;
        }
    }

    protected override IEnumerator Phase2()
    {
        animator.SetTrigger("BulkUp");
        CircleColliderInit(bigDogBossCirCleColliderOffsetY, bigDogBossCirCleColliderRadius);
        if (GameSystem.isAccept)
            PhaseChange(phase2DogBossData);
        yield return new WaitForSeconds(4.4f);
        AudioClipChangeAndPlay(bigDogSoundClip);
        ColliderEnableOn();
        IsisInvincibilityOff();
        while (true)
        {
            // RandomPatternValue();
            // int patternIndex = 0;
            // while (patternIndex < 3)
            // {
            //     if (patternRandomValue[patternIndex] == 0)
            //         yield return StartCoroutine(dogBigTracePattern.Attacking());
            //     if (patternRandomValue[patternIndex] == 1)
            //         yield return StartCoroutine(dogBigLaserPattern.Attacking());
            //     if (patternRandomValue[patternIndex] == 2)
            //         yield return StartCoroutine(dogBigPoundingPattern.Attacking());
            //     patternIndex++;
            // }
            if (dogBossData.isP3 == true)
                yield return StartCoroutine(dogBigTracePattern.Attacking());
            if (dogBossData.isP4 == true)
                yield return StartCoroutine(dogBigLaserPattern.Attacking());
            if (dogBossData.isP5 == true)
                yield return StartCoroutine(dogBigPoundingPattern.Attacking());

            if (!dogBossData.isP3 && !dogBossData.isP4 && !dogBossData.isP5)
                dogBossData.isP3 = true;

            yield return null;

        }
    }

    protected override void SelectionEventTime()
    {
        dogBubblePattern.CoroutineStop();
        dogSmallSojuPattern.CoroutineStop();
    }

    protected override void CoroutineAllStop()
    {
        dogBubblePattern.CoroutineStop();
        dogSmallSojuPattern.CoroutineStop();
        StopAllCoroutines();
    }

    public override void BossDiedEvent()
    {
        CoroutineAllStop();
        animator.SetTrigger("Die");
    }

    protected override void ColliderEnableOn()
    {
        circleCollider2D.enabled = true;
    }

    protected override void ColliderEnableOff()
    {
        circleCollider2D.enabled = false;
    }

    public void AudioClipChangeAndPlay(AudioClip clip)
    {
        bossAudioSource.clip = clip;
        bossAudioSource.Play();
    }

    public void AudioOneShot(AudioClip clip)
    {
        bossAudioSource.PlayOneShot(clip);
    }
    
    public void AudioStop()
    {
        bossAudioSource.Stop();
    }
}
