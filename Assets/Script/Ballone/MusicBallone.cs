using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;

public class MusicBallone : MonoBehaviour
{
    private float speed;
    public ParticleSystem particle;

    private void Start()
    {
        speed = Random.Range(1f, 1.6f);
        StartCoroutine(SelfDestory());
    }

    void Update()
    {
        transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    private void OnMouseDown()
    {
        SoundManager.Instance.Play("Start Level");
        particle.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        BallonesManager.Instance.HideHelp();
    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
