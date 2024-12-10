using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;

public class Bubble : MonoBehaviour
{
    public int type;
    public bool isAlphabet;
    public ParticleSystem alphabet, normal;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakePosition(4f, 0.1f, 1, 90).SetLoops(-1, LoopType.Yoyo);
        transform.DOShakeScale(4f, 0.1f, 1, 90).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnMouseUpAsButton()
    {
        if (!UiHoverListener.Instance.IsPointerOverUIElement())
        {
            underSeaManager.Instance.HideHelp();
            normal.Play();
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            transform.GetChild(0).DOScale(new Vector3(3f, 3f, 3f), 0.6f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.6f);
            StartCoroutine(SelfDestory());
            if (isAlphabet)
            {
                alphabet.Play();
                SoundManager.Instance.Play("win");
                underSeaManager.Instance.Win();
            }
            else
            {

                if (Random.Range(0, 5) < 3)

                {
                    int rand = UnityEngine.Random.Range(0, 20);
                    if (rand < 3)
                    {
                        SoundManager.Instance.Play("afarin");
                    }
                    else if (rand >= 3 && rand < 5)
                    {
                        SoundManager.Instance.Play("ali");
                    }
                    else
                    {
                        SoundManager.Instance.Play("false");
                    }
                    
                }
                else
                {
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
                    else
                    {
                        SoundManager.Instance.Play("hit");
                    }




                }
            }

        }
    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
