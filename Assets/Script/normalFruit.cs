using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;

public class normalFruit : MonoBehaviour
{
    public int type;
    private float speed;
    public ParticleSystem particle;

    private void Start()
    {
        speed = Random.Range(0.8f, 1.8f);
        transform.DORotate(new Vector3(0, 0, 45), 0.8f).SetLoops(-1,LoopType.Yoyo);
        StartCoroutine(SelfDestory());
    }
    void Update()
    {
        transform.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Basket"))
        {
            particle.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

            if (type == 1)
            {
                SoundManager.Instance.Play("like1");
            }
            else if (type == 2)
            {
                SoundManager.Instance.Play("like2");

            }
            else if (type == 3)
            {
                SoundManager.Instance.Play("like3");
            }

            SoundManager.Instance.Play("hitBallones");


            int rand = UnityEngine.Random.Range(0, 20);
            if (rand < 3)
            {
                SoundManager.Instance.Play("afarin");
            }
            else if (rand >= 3 && rand < 5)
            {
                SoundManager.Instance.Play("ali");
            }
        }
    }


    

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
