using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DogBigPoundingPattern : DogBossPatternBase
{
    [SerializeField] private GameObject sojuObject;

    [SerializeField] private int sojuSpawnCount; // 생성 수 
    [SerializeField] private float spawnCycleTime; // 재 생성 시간
    [SerializeField] private float sojuObjectMoveSpeed; // 소주 오브젝트 스피드
    [SerializeField] private float sojuCycleWaitTime; // 소주 패턴 6개 터진후 대기시간
    [SerializeField] private int spawnCount; // 패턴 횟수 

    private float[] xPosData = new float[8];
    private int[] randomIndexs; // 난수 저장 배열

    private Action pounding;

    private List<GameObject> sojuGameObjects = new List<GameObject>();

    protected override void Init()
    {
        randomIndexs = new int[6];
        XposDataInit();
        pounding = () => { };
    }

    public override IEnumerator Attacking()
    {
        int currentSojuSpawnCount = 0; // 소주 생성 수
        int currentSpawnCount = 0; // 패턴 횟수

        yield return new WaitForSeconds(waitTime);

        while (spawnCount > currentSpawnCount)
        {
            RandomNumber();
            while (sojuSpawnCount > currentSojuSpawnCount)
            {
                SpawnObject(xPosData[randomIndexs[currentSojuSpawnCount]]);
                currentSojuSpawnCount++;
                yield return new WaitForSeconds(spawnCycleTime);
            }

            AnimationPounding();
            yield return new WaitForSeconds(sojuCycleWaitTime);
            currentSojuSpawnCount = 0;
            currentSpawnCount++;
        }
    }

    private void SpawnObject(float xPos)
    {
        GameObject clone = Instantiate(sojuObject);
        clone.GetComponent<DogSojuBreak>().Init(sojuObjectMoveSpeed);
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
        for(int i = 0; i < sojuGameObjects.Count; i++)
        {
            sojuGameObjects[i].GetComponent<DogSojuBreak>().Break();
        }
        SojuCountClear();
        CameraShake.cameraShake();
    }

    private void RandomNumber() // 중복없는 난수 출력
    {
        for (int i = 0; i < sojuSpawnCount; i++) 
        {
            randomIndexs[i] = UnityEngine.Random.Range(0, sojuSpawnCount);
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
        xPosData[4] =  1.0f;
        xPosData[5] =  3.0f;
        xPosData[6] =  5.0f;
        xPosData[7] =  7.0f;
    }

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }

}
