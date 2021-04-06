using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    // sprite로 클릭시스템을 위한 스크립트 
    [SerializeField]
    private MapData mapData;
    
    [SerializeField]
    private BoxCollider2D collider;
    [SerializeField]
    private GameObject titleNameObject; // 타이틀 
    [SerializeField]
    private GameObject stage1Sprite; // 보스1 이미지
    [SerializeField]
    private GameObject stage2Sprite; // 보스2 이미지
    [SerializeField]
    private GameObject stage3Sprite; // 보스3 이미지
    [SerializeField]
    private IntroCrossEffect introCrossEffect; // 인트로 이펙트

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Click");
        }
      //  LimitPosition();
    }

    IEnumerator Click()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Stage1"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            collision.GetComponent<SceneLoader>().StartLoadScene("Stage1");
            this.gameObject.SetActive(false);
            titleNameObject.SetActive(false);
            stage2Sprite.SetActive(false);
            stage3Sprite.SetActive(false);
            introCrossEffect.ParticleSystemFadeOut();
        }
        else if (collision.transform.CompareTag("Stage2"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            collision.GetComponent<SceneLoader>().StartLoadScene("Stage2");
            this.gameObject.SetActive(false);
            titleNameObject.SetActive(false);
            stage1Sprite.SetActive(false);
            stage3Sprite.SetActive(false);
            introCrossEffect.ParticleSystemFadeOut();
        }
        else if (collision.transform.CompareTag("Stage3"))
        {
            collision.GetComponent<IntroButtonEffect>().StartTwinkle();
            collision.GetComponent<SceneLoader>().StartLoadScene("Stage3");
            this.gameObject.SetActive(false);
            titleNameObject.SetActive(false);
            stage1Sprite.SetActive(false);
            stage2Sprite.SetActive(false);
            introCrossEffect.ParticleSystemFadeOut();
        }
    }

    private void LimitPosition() // 이동제한 마우스 포인터
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, mapData.LimitMin.x, mapData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, mapData.LimitMin.y, mapData.LimitMax.y));
    }
}
