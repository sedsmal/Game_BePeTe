using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using BizzyBeeGames;


public class JigsawPart : MonoBehaviour
{
    public string id;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isHit = false;
    private Vector3 defScale;

    private void Start()
    {
        defScale = transform.localScale;
    }
    private void OnMouseDown()
    {
        if (!isHit)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                transform.DOShakeScale(0.5f, 0.5f, 4, 90);
                transform.rotation = Quaternion.identity;
                SoundManager.Instance.Play("tap");
                LevelManager.Instance.HideHelp();
            }
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

        if (collision.CompareTag(id))
        {
            DOTween.KillAll();
            if (SoundManager.Instance != null) { SoundManager.Instance.Play("tap"); }
            

            transform.localScale = defScale;
            isHit = true;
            transform.DOMove(collision.transform.position, 0.6f).OnComplete(()=>{ collision.GetComponent<SpriteRenderer>().enabled = false; });
            collision.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            transform.rotation = Quaternion.identity;
            transform.DOShakeScale(0.3f, 0.3f, 5, 90);
            transform.localScale = defScale;
            
            LevelManager.Instance.DoProgress();

        }
    }


}
