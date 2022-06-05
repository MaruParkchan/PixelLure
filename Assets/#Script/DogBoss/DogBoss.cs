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
    private bool isBulkUp; // ��ũ���Ͽ��°�?

    private float smallDogBossCircleColliderOffsetY;
    private float smallDogBossCircleColliderRadius;

    private float bigDogBossCirCleColliderOffsetY;
    private float bigDogBossCirCleColliderRadius;
    private CircleCollider2D circleCollider2D;


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
        patternRandomValue = new int[3]; // ������ �����Ŀ� ���� ������ ���� �� ũ��� ������ �� ��ŭ �����ؾ���       
    }

    private void BigBossCircleColliderPositionAndSizeData() // �� ���� ��ũ���ϸ� �ݶ��̴� ��ȭ�� 
    {
        smallDogBossCircleColliderOffsetY = 0.2f;
        smallDogBossCircleColliderRadius = 0.27f;
        bigDogBossCirCleColliderOffsetY = 1.0f;
        bigDogBossCirCleColliderRadius = 0.3f;

    }

    private void CircleColliderInit(float offsetY, float radius) // �ݶ��̴� ũ��, ��ġ ��ȭ
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
            yield return StartCoroutine(dogBubblePattern.IBubbleSpawner());
            yield return StartCoroutine(dogSmallSojuPattern.ISojuPattern());
        }
    }

    protected override IEnumerator Phase2()
    {
        animator.SetTrigger("BulkUp");
        CircleColliderInit(bigDogBossCirCleColliderOffsetY, bigDogBossCirCleColliderRadius); ;
        yield return new WaitForSeconds(4.4f);
        ColliderEnableOn();
        IsisInvincibilityOff();
        while (true)
        {
            RandomPatternValue();
            int patternIndex = 0;

            while (patternIndex < 3)
            {
                if (patternRandomValue[patternIndex] == 0)
                    yield return StartCoroutine(dogBigTracePattern.ISpawnSoju());
                if (patternRandomValue[patternIndex] == 1)
                    yield return StartCoroutine(dogBigLaserPattern.ILaserPattern());
                if (patternRandomValue[patternIndex] == 2)
                    yield return StartCoroutine(dogBigPoundingPattern.ISojuRain());

                patternIndex++;
            }
        }
    }

    protected override void SelectionEventTime()
    {
        dogBubblePattern.CoroutineStop();
        dogSmallSojuPattern.CoroutineStop();
    }

    protected override void ColliderEnableOn()
    {
        circleCollider2D.enabled = true;
    }

    protected override void ColliderEnableOff()
    {
        circleCollider2D.enabled = false;
    }
}
