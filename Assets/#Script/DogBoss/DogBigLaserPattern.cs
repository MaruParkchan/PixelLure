using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigLaserPattern : MonoBehaviour
{
    [Header("�⺻ ������ �Ӽ�")]
    [SerializeField] private GameObject laserObject;// ������ 
    [SerializeField] private float laserSpawnCycleTime; // �� ���� �ð�
    [SerializeField] private int laserSpawnMaxCount; // ������ ������ 
    [Header("�߰�Ÿ ������ �Ӽ�")]
    [SerializeField] private GameObject bundleLaserObject; // �߰�Ÿ ������
    [SerializeField] private float bundleLaserSpawnCycleTime; // �߰�Ÿ ���� �ð�
    [SerializeField] private int bundleLaserSpawnMaxCount; // �߰�Ÿ ������ ������ 
    [SerializeField] private DogBundleLaser[] dogBundleLasers;
    private int bunlaserIndex;

    [SerializeField] private float waitTime;
    private Animator animator;
    private Vector2 limitPosition;
    private Quaternion euler;
    [SerializeField]
    private bool isArousal; // ���������ΰ�? ( �߰�Ÿ )

    private float xPos = 8.3f;
    private float ypos = 4.4f;
    // -8.3f / 8.3f -- x�� z = 90

    // -4.4f / 4.4f -- y�� z = 0

    private void Start()
    {
        animator = GetComponent<Animator>();
        dogBundleLasers = new DogBundleLaser[bundleLaserSpawnMaxCount];
    }

    public IEnumerator ILaserPattern()
    {
        int currentCount = 0;
        animator.SetTrigger("Posing");
        yield return new WaitForSeconds(waitTime);
        while (laserSpawnMaxCount > currentCount)
        {
            SpawnLaserInitAndSpawn(laserObject);
            yield return new WaitForSeconds(laserSpawnCycleTime);
            currentCount++;
        }

        currentCount = 0;

        if (isArousal) // �������ΰ�? �߰�Ÿ�� �ϴ°�?
        {
            while(bundleLaserSpawnMaxCount > currentCount)
            {
                yield return new WaitForSeconds(bundleLaserSpawnCycleTime);
                SpawnLaserInitAndSpawn(bundleLaserObject);
                currentCount++;
            }

            for(int i = 0; i < dogBundleLasers.Length; i++) // �߰�Ÿ ����
            {
                dogBundleLasers[i].Attack();
            }
            bunlaserIndex = 0;
        }

        animator.SetTrigger("End");
        yield return new WaitForSeconds(waitTime);
    }

    private void SpawnLaserInitAndSpawn(GameObject laserObject)
    {
        int index = Random.Range(0, 2);

        if (index == 0)
        {
            limitPosition = new Vector2(Random.Range(-xPos, xPos), 0);
            euler = Quaternion.Euler(0, 0, 90.0f);
            LaserSpawn(laserObject, limitPosition, euler);
        }
        else
        {
            limitPosition = new Vector2(0, Random.Range(-ypos, ypos));
            euler = Quaternion.Euler(0, 0, 0);
            LaserSpawn(laserObject, limitPosition, euler);
        }
    }

    private void LaserSpawn(GameObject laserObject, Vector3 pos, Quaternion euler)
    {
        GameObject clone = Instantiate(laserObject);
        clone.transform.position = pos;
        clone.transform.rotation = euler;

        if (clone.GetComponent<DogBundleLaser>() != null)
        {
            dogBundleLasers[bunlaserIndex] = clone.GetComponent<DogBundleLaser>();
            bunlaserIndex++;
        }
    }

    //private void BundleLaserSpawn(Vector3 pos, Quaternion euler)
    //{
    //    GameObject clone = Instantiate(laserObject);
    //    clone.transform.position = pos;
    //    clone.transform.rotation = euler;
        
    //    if(clone.GetComponent<DogBundleLaser>() != null)
    //    {
    //        dogBundleLasers[bunlaserIndex] = clone.GetComponent<DogBundleLaser>();
    //        bunlaserIndex++;
    //    }
    //}
}
