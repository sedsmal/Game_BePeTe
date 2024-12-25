using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.EventSystems;

public class AlphabetDragDropLevel1 : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isHit=false;
    Vector3 defScale;
    private void Start()
    {
        defScale = transform.localScale;
    }
    private void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            SoundManager.Instance.Play("alphabet");
            transform.DOShakeScale(0.5f, 0.5f, 10, 90);
            Level1Manager.Instance.HideHelp();
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
        
        if (collision.CompareTag("GrayPuzzleLevel1"))
        {
            isHit = true;
            
            transform.position = collision.transform.position;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            transform.rotation = Quaternion.identity;
            transform.DOShakeScale(0.5f, 0.3f, 10, 90).OnComplete(() => { transform.localScale = defScale; });
            transform.localScale = defScale;
            Level1Example.Instance.Shake();
            SoundManager.Instance.Play("win");
            Level1Manager.Instance.PlaySoundDelay("Afarin");
            //SoundManager.Instance.Play("Afarin");
            Level1Manager.Instance.Win();
           // Level1Manager.Instance.PlaySoundImmediately("like");
            collision.GetComponent<SpriteRenderer>().enabled = false;

        }
    }




}
