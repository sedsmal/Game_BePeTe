using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;

public class ObjectToFindOther : MonoBehaviour
{
    public string type;
    private void OnMouseUpAsButton()
    {
        
        GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.4f);
        transform.DOShakeScale(0.5f, 0.2f, 2, 90).OnComplete(() => { Destroy(this.gameObject); });
        SoundManager.Instance.Play("like" + type);


    }
}
