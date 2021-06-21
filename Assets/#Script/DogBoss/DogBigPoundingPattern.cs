using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigPoundingPattern : MonoBehaviour
{
    [SerializeField] private GameObject sojuObject;

    [SerializeField] private int spawnCount; // ���� �� 
    [SerializeField] private float spawnCycleTime; // �� ���� �ð�
    [SerializeField] private float sojuObjectMoveSpeed; // ���� ������Ʈ ���ǵ�
    [SerializeField] private float waitTime; // ���ð�

    private float[] xPosData = new float[8];
    private int[] randomIndexs; // ���� ���� �迭

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

    private void RandomNumber() // �ߺ����� ���� ���
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

    private void XposDataInit() // Xpos ������ �ʱ�ȭ 
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
