using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using BizzyBeeGames;

public class Basket : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 defaultScale;
    private bool isHit = false;

    private void Start()
    {
        defaultScale = transform.localScale;
    }
    private void OnMouseDown()
    {
        if (!Statics.ispause)
        {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            SoundManager.Instance.Play("alphabet");
            transform.DOShakeScale(0.5f, 0.5f, 10, 90).OnComplete(() => { transform.localScale = defaultScale; }); 
            FruiteManager.Instance.HideHelp();
        }

        
    }

    private void OnMouseUp()
    {
        if (!Statics.ispause)
        {
            transform.localScale = defaultScale;
        }
    }

    private void OnMouseDrag()
    {
        if (!Statics.ispause)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;





            transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);

            if (transform.position.y > -2f)
            {
                transform.position = new Vector3(transform.position.x, -2f, transform.position.z);

            }

            if (transform.position.y < -3.85f)
            {
                transform.position = new Vector3(transform.position.x, -3.85f, transform.position.z);

            }

            if (transform.position.x < -7f)
            {
                transform.position = new Vector3(-7f, transform.position.y, transform.position.z);

            }

            if (transform.position.x > 7f)
            {
                transform.position = new Vector3(7f, transform.position.y, transform.position.z);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.DOShakeScale(0.4f, 0.1f, 5, 90).OnComplete(() => { transform.localScale = defaultScale; });

    }
}
