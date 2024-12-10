using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;

public class AlphabetPlanet : MonoBehaviour
{
    private float speed;
    public ParticleSystem particle, particle2;

    private void Start()
    {
        speed = Random.Range(0.8f, 1.6f);
        transform.DOLocalRotate(
               new Vector3(0f, 0, 360), 3f, RotateMode.FastBeyond360).
               SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        StartCoroutine(SelfDestory());
        //GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
    void Update()
    {
       
        transform.position -= new Vector3(speed * Time.deltaTime,0f , 0f);
    }

    private void OnMouseDown()
    {
        BallonesManager.Instance.HitBallone();
        particle.Play();
        particle2.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Ship"))
        {
            //SoundManager.Instance.Play("alphabet");
            SoundManager.Instance.Play("win");
            SoundManager.Instance.Play("alphabet");
            SpaceManager.Instance.CollectPlanet();
            particle.Play();
            particle2.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ship"))
        {
            //SoundManager.Instance.Play("alphabet");
            SoundManager.Instance.Play("win");
            SpaceManager.Instance.CollectPlanet();
            SoundManager.Instance.Play("alphabet");
            particle.Play();
            particle2.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        }


    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(50f);
        Destroy(this.gameObject);
    }
}
