using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;

public class NormalSpace : MonoBehaviour
{
    public int type;
    private float speed;
    public ParticleSystem particle;

    private void Start()
    {
        speed = Random.Range(0.8f, 1.8f);
        transform.DOLocalRotate(
               new Vector3(0f, 0, 360), 3f, RotateMode.FastBeyond360).
               SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        StartCoroutine(SelfDestory());
    }
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
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
        yield return new WaitForSeconds(50f);
        Destroy(this.gameObject);
    }
}
