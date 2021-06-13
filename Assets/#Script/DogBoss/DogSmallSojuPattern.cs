using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject dogSmallSojuCirclePrefab;

    [SerializeField]
    private float rotateSpeed; // 회전 속도
    [SerializeField]
    private float rotateTime; // 패턴 회전 시간 
    [SerializeField]
    private float fadeTime; // 페이드 걸리는 시간
    [SerializeField]
    private float waitTime; // 패턴 대기시간

    public IEnumerator ISojuPattern()
    {
        int randomIndex = Random.Range(0, 2); // 0 = false , 1 = true

        if(randomIndex == 0) // 회전 방향 조건문
        {
            rotateSpeed *= -1;
        }

        yield return new WaitForSeconds(waitTime);

        GameObject clone = Instantiate(dogSmallSojuCirclePrefab);
        clone.GetComponent<DogSmallSojuCircle>().Init(rotateSpeed, rotateTime, fadeTime);
        clone.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(waitTime + rotateTime); // 대기시간 + 회전 시간 + 페이드 시간

    }
}
