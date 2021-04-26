using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMini : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject smokeEffect;
    [SerializeField] private float bombTimer;
    private GameObject target;
    private Animator animator;
    private bool isDie = false;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        StartCoroutine(Timebomb());
    }

    private void Update()
    {
        if (isDie == false)
            TargetTrace(target.transform.position);
    }

    private void TargetTrace(Vector3 target) // 타겟 위치로 이동 
    {
        float distance = Vector3.Distance(target, transform.position);

        Vector3 dir = (target - transform.position).normalized;

        transform.position += dir * Time.deltaTime * moveSpeed;

        Vector2 direction = new Vector2(target.x - this.transform.position.x, target.y - this.transform.position.y);
        transform.up = direction;

        if (distance < 0.125f)
        {
            IsDie();
        }
    }

    public void TakeDamage()
    {

    }

    public void SpawnSmokeEffect() // 죽을시 연기 이펙트 생성
    {
        GameObject clone = Instantiate(smokeEffect);
        clone.transform.position = this.transform.position;
    }

    public void DestroyObjectEvent()
    {
        SpawnSmokeEffect();
        Destroy(this.gameObject);
    }

    private void IsDie()
    {
        isDie = true;
        animator.SetTrigger("Die");
    }

    IEnumerator Timebomb()
    {
        yield return new WaitForSeconds(bombTimer);
        IsDie();
    }
}
