using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigPoundingPattern : MonoBehaviour
{
    [SerializeField] private GameObject sojuObject;

    [SerializeField] private int spawnCount; // 생성 수 
    [SerializeField] private float spawnCycleTime; // 재 생성 시간
    [SerializeField] private float sojuObjectMoveSpeed; // 소주 오브젝트 스피드
    [SerializeField] private float waitTime; // 대기시간

    private float[] xPosData = new float[8];
    private int[] randomIndexs; // 난수 저장 배열

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        randomIndexs = new int[spawnCount];
        XposDataInit();
    }

    public IEnumerator ISojuRain()
    {
        int currentCount = 0;
        RandomNumber();
        yield return new WaitForSeconds(waitTime);

        while(spawnCount > currentCount)
        {
            SpawnObject(xPosData[randomIndexs[currentCount]]);
            currentCount++;
            yield return new WaitForSeconds(spawnCycleTime);
        }
    }

    private void SpawnObject(float xPos)
    {
        GameObject clone = Instantiate(sojuObject);
        clone.GetComponent<DogSojuBreak>().Init(sojuObjectMoveSpeed);
        clone.transform.position = new Vector3(xPos, 6.0f, 0);
    }

    public void AnimationPounding()
    {
        animator.SetTrigger("Pounding");
    }

    public void Pounding()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("SojuBreak");
        for(int i = 0; i < clones.Length; i++)
        {
            clones[i].GetComponent<DogSojuBreak>().Break();
        }
    }

    private void RandomNumber() // 중복없는 난수 출력
    {
        for (int i = 0; i < spawnCount; i++) 
        {
            randomIndexs[i] = Random.Range(0, spawnCount);
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
}
