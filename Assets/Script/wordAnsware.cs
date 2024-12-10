using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class wordAnsware : MonoBehaviour
{
    public string iD;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isHit = false;
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

            transform.DOShakeScale(0.5f, 0.5f, 10, 90);
            //ThreealphaWordManager.Instance.HideHelp();
            playSound();
        }
    }

    private void playSound()
    {
        if (iD == "A")
        {
            SoundManager.Instance.Play("A");
        }
        else if (iD == "B")
        {
            SoundManager.Instance.Play("B");
        }
        else if (iD == "P")
        {
            SoundManager.Instance.Play("P");
        }
        else if (iD == "C3")
        {
            SoundManager.Instance.Play("C3");
        }
        else if (iD == "T")
        {
            SoundManager.Instance.Play("T");
        }
        else if (iD == "J")
        {
            SoundManager.Instance.Play("J");
        }
        else if (iD == "Ch")
        {
            SoundManager.Instance.Play("Ch");
        }
        else if (iD == "Hj")
        {
            SoundManager.Instance.Play("Hj");
        }
        else if (iD == "Kh")
        {
            SoundManager.Instance.Play("Kh");
        }
        else if (iD == "D")
        {
            SoundManager.Instance.Play("D");
        }
        else if (iD == "Zl")
        {
            SoundManager.Instance.Play("Zl");
        }
        else if (iD == "R")
        {
            SoundManager.Instance.Play("R");
        }
        else if (iD == "Zr")
        {
            SoundManager.Instance.Play("Zr");
        }
        else if (iD == "S")
        {
            SoundManager.Instance.Play("S");
        }
        else if (iD == "Sh")
        {
            SoundManager.Instance.Play("Sh");
        }
        else if (iD == "Sd")
        {
            SoundManager.Instance.Play("Sd");
        }
        else if (iD == "Zd")
        {
            SoundManager.Instance.Play("Zd");
        }
        else if (iD == "T2")
        {
            SoundManager.Instance.Play("T2");
        }
        else if (iD == "Z2")
        {
            SoundManager.Instance.Play("Z2");
        }
        else if (iD == "Eyn")
        {
            SoundManager.Instance.Play("Eyn");
        }
        else if (iD == "Ghyn")
        {
            SoundManager.Instance.Play("Ghyn");
        }
        else if (iD == "F")
        {
            SoundManager.Instance.Play("F");
        }
        else if (iD == "Gh")
        {
            SoundManager.Instance.Play("Gh");
        }
        else if (iD == "K")
        {
            SoundManager.Instance.Play("K");
        }
        else if (iD == "G")
        {
            SoundManager.Instance.Play("G");
        }
        else if (iD == "L")
        {
            SoundManager.Instance.Play("L");
        }
        else if (iD == "M")
        {
            SoundManager.Instance.Play("M");
        }
        else if (iD == "N")
        {
            SoundManager.Instance.Play("N");
        }
        else if (iD == "V")
        {
            SoundManager.Instance.Play("V");
        }
        else if (iD == "H")
        {
            SoundManager.Instance.Play("H");
        }
        else if (iD == "Y")
        {
            SoundManager.Instance.Play("Y");
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

        if (gameObject.name==collision.name+"A")
        {
            isHit = true;
            transform.localScale = defScale;
            transform.position = collision.transform.position;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            transform.rotation = Quaternion.identity;
            transform.DOShakeScale(0.8f, 0.3f, 10, 10).OnComplete(()=> { transform.DOScale(defScale,0.3f); });
            SoundManager.Instance.Play("win");
            ThreealphaWordManager.Instance.Win();
            collision.GetComponent<SpriteRenderer>().enabled = false;
            if (collision.transform.childCount != 0)
            {
                collision.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }

}
