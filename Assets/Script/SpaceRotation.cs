using UnityEngine;
using DG.Tweening;

public class SpaceRotation : MonoBehaviour
{
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(
               new Vector3(0f, 0, 360), time, RotateMode.FastBeyond360).
               SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

}
