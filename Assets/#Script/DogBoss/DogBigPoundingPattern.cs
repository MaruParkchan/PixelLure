using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DogBigPoundingPattern : MonoBehaviour
{
    [SerializeField] private GameObject sojuObject;

    [SerializeField] private int sojuSpawnCount; // ���� �� 
    [SerializeField] private float spawnCycleTime; // �� ���� �ð�
    [SerializeField] private float sojuObjectMoveSpeed; // ���� ������Ʈ ���ǵ�
    [SerializeField] private float sojuCycleWaitTime; // ���� ���� 6�� ������ ���ð�
    [SerializeField] private int spawnCount; // ���� Ƚ�� 
    [SerializeField] private float waitTime; // ���ð�

    private float[] xPosData = new float[8];
    private int[] randomIndexs; // ���� ���� �迭

    private Animator animator;
    private List<GameObject> sojuGameObjects = new List<GameObject>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        randomIndexs = new int[6];
        XposDataInit();
    }

    public IEnumerator ISojuRain()
    {
        int currentSojuSpawnCount = 0; // ���� ���� ��
        int currentSpawnCount = 0; // ���� Ƚ��

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

        Debug.Log("���� �����");
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
        animator.SetTrigger("Pounding");
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
        //GameObject[] clones = GameObject.FindGameObjectsWithTag("SojuBreak");
        //for(int i = 0; i < clones.Length; i++)
        //{
        //    clones[i].GetComponent<DogSojuBreak>().Break();
        //}
        for(int i = 0; i < sojuGameObjects.Count; i++)
        {
            sojuGameObjects[i].GetComponent<DogSojuBreak>().Break();
        }
        SojuCountClear();
        CameraShake.cameraShake();
    }

    private void RandomNumber() // �ߺ����� ���� ���
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

    public void CoroutineStop()
    {
        StopAllCoroutines();
    }
}
