using DG.Tweening;
using UnityEngine;

public class spacePlanetVibrate : MonoBehaviour
{

    void Start()
    {
        transform.DOLocalRotate(
        new Vector3(0f, 0, 30), 20).SetLoops(-1, LoopType.Yoyo);
    }
}
