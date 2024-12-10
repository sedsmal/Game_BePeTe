using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BizzyBeeGames;
using DG.Tweening;

public class WinImageShake : MonoBehaviour
{
    public int strengh = 1;
    public int vibrate = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.DOShakeRotation(2, 20*strengh, 4*vibrate, 90).SetLoops(-1, LoopType.Yoyo);
        
    }

}
