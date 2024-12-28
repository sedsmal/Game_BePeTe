using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isHit = false;
    public string tagg; 

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            LockManager.Instance.HideHelp();
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            SoundManager.Instance.Play("alphabet");
            transform.DOShakeScale(0.5f, 0.5f, 5, 1);
        }
    }

    private void OnMouseDrag()
    {
        if (!isHit)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 DefaultScale;
        Vector3 DefaultColScale;

        if (collision.CompareTag(tagg))
        {
            DefaultScale = transform.localScale;
            DefaultColScale = collision.transform.localScale;
            isHit = true;
            LockManager.Instance.DestoryAnimator();
            LockManager.Instance.OpenLock(collision.name);
            transform.position = collision.transform.GetChild(0).transform.position;
            collision.transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().isTrigger = true;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            
            transform.rotation = Quaternion.identity;
            collision.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            
            SoundManager.Instance.Play("win");
            
            LockManager.Instance.Win();

            int rand = Random.Range(0, 10);
            if (LockManager.Instance.trueAnsware < 3)
            {
                if (rand < 4)
                {
                    StartCoroutine(LockManager.Instance.PlaySound("like1"));
                }
                else if (rand >= 4 && rand < 7)
                {
                    StartCoroutine(LockManager.Instance.PlaySound("like2"));
                }
                else
                {
                    StartCoroutine(LockManager.Instance.PlaySound("like3"));
                }

            }

            //collision.transform.DOShakeScale(0.5f, 0.5f, 1, 1).OnComplete(()=>
            //{ collision.transform.localScale = DefaultColScale; transform.DOShakeScale(0.5f, 0.3f, 1, 1).OnComplete(()=>
            //{ transform.localScale = DefaultScale; }); });


        }
        else
        {
            if (!collision.name.Contains("key"))
            {
                LockManager.Instance.PlaySoundImmediately("Unlock");

                GameObject.FindGameObjectWithTag(tagg).transform.DOShakeRotation(0.3f, 30, 1, 1).OnComplete(() => { GameObject.FindGameObjectWithTag(tagg).transform.rotation = Quaternion.identity; });

            }
               
        }
    }
}
