using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DogBigPoundingPattern : DogBossPatternBase
{
    [SerializeField] private GameObject eazySojuObject;
    [SerializeField] private GameObject hardSojuObject;

    private float[] xPosData = new float[8];
    [SerializeField]
    private int[] randomIndexs; // 난수 저장 배열

    private Action pounding;

    private List<GameObject> sojuGameObjects = new List<GameObject>();

    protected override void Init()
    {
        randomIndexs = new int[8];
        XposDataInit();
        pounding = () => { };
    }

    public override IEnumerator Attacking()
    {
        int currentSojuSpawnCount = 0; // 소주 생성 수
        int currentSpawnCount = 0; // 패턴 횟수

        CameraRotate();

        yield return new WaitForSeconds(waitTime);

        while (dogBoss.dogBossData.p5_PatternCount > currentSpawnCount)
        {
            RandomNumber();
            while (dogBoss.dogBossData.p5_AttackCount > currentSojuSpawnCount)
            {
                if (GameSystem.isAccept)
                    SpawnObject(hardSojuObject, xPosData[randomIndexs[currentSojuSpawnCount]]);
                else
                    SpawnObject(eazySojuObject, xPosData[randomIndexs[currentSojuSpawnCount]]);
                currentSojuSpawnCount++;
                yield return new WaitForSeconds(dogBoss.dogBossData.p5_AttackDelayTime);
            }

            AnimationPounding();
            yield return new WaitForSeconds(dogBoss.dogBossData.p5_WaitTime);
            currentSojuSpawnCount = 0;
            currentSpawnCount++;
        }
    }

    private void SpawnObject(GameObject sojuObject, float xPos)
    {
        GameObject clone = Instantiate(sojuObject);
        clone.GetComponent<DogSojuBreak>().Init(dogBoss.dogBossData.p5_SojuMoveSpeed);
        clone.transform.position = new Vector3(xPos, 6.0f, 0);
        SojuCountPlus(clone);
    }

    public void AnimationPounding()
    {
        dogBossAnimator.SetTrigger("Pounding");
    }

    public void SojuCountPlus(GameObject sojuObject)
    {
        sojuGameObjects.Add(sojuObject);
    }

    private void SojuCountClear()
    {
        sojuGameObjects.Clear();
    }

    public void Pounding()
    {
        for (int i = 0; i < sojuGameObjects.Count; i++)
        {
            sojuGameObjects[i].GetComponent<DogSojuBreak>().Break();
        }
        SojuCountClear();
        CameraShake.cameraShake();
    }

    private void RandomNumber() // 중복없는 난수 출력
    {
        for (int i = 0; i < dogBoss.dogBossData.p5_AttackCount; i++)
        {
            randomIndexs[i] = UnityEngine.Random.Range(0, dogBoss.dogBossData.p5_AttackCount);
            for (int j = 0; j < i; j++)
            {
                if (randomIndexs[i] == randomIndexs[j])
                {
                    i--;
                    break;
                }
            }
        }
    }

    private void XposDataInit() // Xpos 데이터 초기화 
    {
        xPosData[0] = -7.0f;
        xPosData[1] = -5.0f;
        xPosData[2] = -3.0f;
        xPosData[3] = -1.0f;
        xPosData[4] = 1.0f;
        xPosData[5] = 3.0f;
        xPosData[6] = 5.0f;
        xPosData[7] = 7.0f;
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

}
