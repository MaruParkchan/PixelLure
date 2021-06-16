using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSmallSojuPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject dogSmallSojuCirclePrefab;

    [SerializeField]
    private float rotateSpeed; // ȸ�� �ӵ�
    [SerializeField]
    private float rotateTime; // ���� ȸ�� �ð� 
    [SerializeField]
    private float fadeTime; // ���̵� �ɸ��� �ð�
    [SerializeField]
    private float waitTime; // ���� ���ð�

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator ISojuPattern()
    {
        int randomIndex = Random.Range(0, 2); // 0 = false , 1 = true

        if(randomIndex == 0) // ȸ�� ���� ���ǹ�
        {
            rotateSpeed *= -1;
        }

        animator.SetTrigger("Drink");
        yield return new WaitForSeconds(waitTime);

        GameObject clone = Instantiate(dogSmallSojuCirclePrefab);
        clone.GetComponent<DogSmallSojuCircle>().Init(rotateSpeed, rotateTime, fadeTime);
        clone.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(waitTime + rotateTime); // ���ð� + ȸ�� �ð� + ���̵� �ð�
        animator.SetTrigger("End");

    }
}
