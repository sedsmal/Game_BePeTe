using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonPressAnimation : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    public bool isPulse;
    public bool isHearyPulse;
    public float pressPower;
    Vector3 defaultT;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = transform.localScale * pressPower;
        
        DOTween.KillAll();

        if (SceneManager.GetActiveScene().name == "Menu Alphabets 1"|| SceneManager.GetActiveScene().name == "Menu Alphabets new")
        {
            //            Debug.Log(gameObject.name);
            MainMenuManager.Instance.PlayAlphabetSound(gameObject.name + "0");
        }
        //}else if(SceneManager.GetActiveScene().name == "Menu Learning")
        //{
        //    MainMenuManager.Instance.PlayAlphabetSound(gameObject.name);
        //}
        

        SoundManager.Instance.Play("btn");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = defaultT;

        if (isPulse)
        {
            transform.DOShakeScale(3, 0.4f, 5, 90).SetLoops(-1, LoopType.Yoyo);
        }
        
        SoundManager.Instance.Play("btn");
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultT = transform.localScale;
        if (isPulse)
        {
            transform.DOShakeScale(3, 0.15f, 2, 20).SetLoops(-1, LoopType.Yoyo);
        }
        if (isHearyPulse)
        {
            transform.DOPunchScale(transform.localScale*1.01f,2f,0, 0).SetLoops(-1, LoopType.Yoyo);
        }
        

    }
}
