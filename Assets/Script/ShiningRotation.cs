using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShiningRotation : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(
                new Vector3(0f, 0, 360), time, RotateMode.FastBeyond360).
                SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

}
