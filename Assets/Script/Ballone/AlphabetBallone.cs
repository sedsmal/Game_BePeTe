using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphabetBallone : MonoBehaviour
{

    private float speed;
    private bool isTiming = false;
    private float startTime = 0f;
    public float responseTime = 0f;
    public bool isPracticeStart;
    public ParticleSystem particle,particle2;

    private void Start()
    {
        speed = Random.Range(0.8f, 1.6f);
        startTime = Time.time;
        StartCoroutine(SelfDestory());
    }
    void Update()
    {
        transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    private void OnMouseDown()
    {
        if (isTiming)
        {
            responseTime = Time.time - startTime;
            BallonesManager.Instance.CollectResponseTime(responseTime);
            isTiming = false;
        }

        if (SceneManager.GetActiveScene().name.Contains("Practice"))
        {
            BallonesPracticeManager.Instance.HitBallone(this.gameObject.name);

            if (isPracticeStart)
            {
                BallonesPracticeManager.Instance.HitBallone();
            }
            BallonesPracticeManager.Instance.HideHelp();
            if (transform.childCount > 1)
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        }
        else
        {
            BallonesManager.Instance.HitBallone();
            BallonesManager.Instance.HideHelp();
        }
        
        particle.Play();
        particle2.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        


    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
