using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;

public class AlphabetFruit : MonoBehaviour
{
    private float speed;
    public ParticleSystem particle, particle1, particle2, particle3;

    private void Start()
    {
        speed = Random.Range(0.8f, 1.6f);
        transform.DORotate(new Vector3(0, 0, 45), 0.8f).SetLoops(-1, LoopType.Yoyo);
        StartCoroutine(SelfDestory());
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
    void Update()
    {
        transform.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    private void Collect()
    {
        particle.Play();
        SoundManager.Instance.Play("win");
        SoundManager.Instance.Play("alphabet");
        //if (Random.Range(0, 10) < 4)
        //{

        //    particle1.Play();

        //    if (UnityEngine.Random.Range(0, 5) < 2)
        //    {
        //        SoundManager.Instance.Play("alphabet");
        //    }
        //    else
        //    {
        //        SoundManager.Instance.Play("like1");
        //    }

        //}
        //else if(Random.Range(0, 10) >= 4&& Random.Range(0, 10) < 7)
        //{
        //    particle2.Play();
        //    if (UnityEngine.Random.Range(0, 5) < 2)
        //    {
        //        SoundManager.Instance.Play("alphabet");
        //    }
        //    else
        //    {
        //        SoundManager.Instance.Play("like2");
        //    }
        //}
        //else
        //{
        //    particle3.Play();
        //    if (UnityEngine.Random.Range(0, 5) < 2)
        //    {
        //        SoundManager.Instance.Play("alphabet");
        //    }
        //    else
        //    {
        //        SoundManager.Instance.Play("like3");
        //    }
        //}

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Basket"))
        {
            FruiteManager.Instance.CollectFruit();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            Collect();

        }

        
    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
