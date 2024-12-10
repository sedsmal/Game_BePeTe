using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class AlphabetDragChange : MonoBehaviour
{
    public string iD;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isHit = false;
    Vector3 defScale;
    Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        defScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        transform.parent.GetComponent<Animator>().enabled = false;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            tempPos = transform.position;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
//            SoundManager.Instance.Play("alphabet");
            transform.DOShakeScale(0.5f, 0.5f, 10, 90);
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

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
    }
    private void OnMouseUp()
    {
        // Cast a ray straight down.
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.up);
        
        // If it hits something...

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.tag == "SortAlpha" && hit.transform.gameObject != this.gameObject)
            {
                Debug.Log(hit.transform.gameObject.name);
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, transform.position.z);
                hit.transform.position = new Vector3(tempPos.x, hit.transform.position.y, hit.transform.position.z);
                WordSortManagment.Instance.CheckWin();
                break;
            }
            else
            {
                transform.position=tempPos;
            }
        }
    }
}
