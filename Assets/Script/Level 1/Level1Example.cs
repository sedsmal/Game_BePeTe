using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;
using UnityEngine.EventSystems;

public class Level1Example : SingletonComponent<Level1Example>
{
    private void OnMouseUpAsButton()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        //{
        //    transform.DOShakeScale(0.5f, 0.3f, 10, 90);
        //    SoundManager.Instance.Play("like");
        //}

        if (!UiHoverListener.Instance.IsPointerOverUIElement())
        {
            transform.DOShakeScale(0.5f, 0.3f, 10, 90);
            SoundManager.Instance.Play("like");
        }

    }

    public void Shake()
    {
        transform.DOShakeScale(0.5f, 0.3f, 10, 90);
    }
}
