using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBigLaserPattern : MonoBehaviour
{
    [Header("기본 레이저 속성")]
    [SerializeField] private GameObject laserObject;// 레이저 
    [SerializeField] private float laserSpawnCycleTime; // 재 생성 시간
    [SerializeField] private int laserSpawnMaxCount; // 레이저 생성수 
    [Header("추가타 레이저 속성")]
    [SerializeField] private GameObject bundleLaserObject; // 추가타 레이저
    [SerializeField] private float bundleLaserSpawnCycleTime; // 추가타 생성 시간
    [SerializeField] private int bundleLaserSpawnMaxCount; // 추가타 레이저 생성수 
    [SerializeField] private DogBundleLaser[] dogBundleLasers;
    private int bunlaserIndex;

    [SerializeField] private float waitTime;
    private Animator animator;
    private Vector2 limitPosition;
    private Quaternion euler;
    [SerializeField]
    private bool isArousal; // 각성상태인가? ( 추가타 )

    private float xPos = 8.3f;
    private float ypos = 4.4f;
    // -8.3f / 8.3f -- x값 z = 90

    // -4.4f / 4.4f -- y값 z = 0

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

        if (isArousal) // 각성중인가? 추가타를 하는가?
        {
            while(bundleLaserSpawnMaxCount > currentCount)
            {
                yield return new WaitForSeconds(bundleLaserSpawnCycleTime);
                SpawnLaserInitAndSpawn(bundleLaserObject);
                currentCount++;
            }

            for(int i = 0; i < dogBundleLasers.Length; i++) // 추가타 공격
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
