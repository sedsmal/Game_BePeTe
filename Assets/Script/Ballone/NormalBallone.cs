using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;

public class NormalBallone : MonoBehaviour
{
    private float speed;
    public ParticleSystem particle;

    private void Start()
    {
        speed = Random.Range(0.8f, 1.8f);
        StartCoroutine(SelfDestory());
    }
    void Update()
    {
        transform.position += new Vector3(0f ,speed * Time.deltaTime,0f);
    }

    private void OnMouseDown()
    {
        particle.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        SoundManager.Instance.Play("hitBallones");
        BallonesManager.Instance.HideHelp();


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

    IEnumerator SelfDestory()
    {


        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
