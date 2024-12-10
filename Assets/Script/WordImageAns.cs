using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.EventSystems;

public class WordImageAns : MonoBehaviour
{
    public bool isAnsware;
    public bool isText;
    public string id;
    private Vector3 defaultScale;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        defaultScale = transform.localScale;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isAnsware)
            {
                transform.parent.GetComponent<Animator>().enabled = false;
                SoundManager.Instance.Play(id);
                transform.DOShakeScale(0.5f, 0.5f, 10, 90);
                ThreealphaWordManager.Instance.HideHelp();
                ThreealphaWordManager.Instance.PlayParticel();
                SoundManager.Instance.Play("win");
                GetComponent<SpriteRenderer>().DOFade(0,0.8f) ;
               
                transform.DOScale(new Vector3(5, 5, 5), 1f).OnComplete(()=> {

                    if (!isText)
                    {

                        ThreealphaWordManager.Instance.hideWOrdImage();
                        SoundManager.Instance.Play("Ali");
                    }
                    else
                    {
                        ThreealphaWordManager.Instance.ShowSortRound();
                        ThreealphaWordManager.Instance.PlayParticel();
                        ThreealphaWordManager.Instance.HideRoundOne();
                        SoundManager.Instance.Play("Afarin");

                    }

                });

                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
                transform.parent.GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, 0.3f);


            }
            else
            {
                SoundManager.Instance.Play(id);
                transform.DOShakeScale(0.5f, 0.5f, 10, 90);

                //ThreealphaWordManager.Instance.HideHelp();
            }
        }
    }

    private void OnMouseUp()
    {
        transform.localScale = defaultScale;
    }

}
