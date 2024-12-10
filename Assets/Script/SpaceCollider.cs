using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using DG.Tweening;
public class SpaceCollider : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.8f, 1.8f);
        transform.DOLocalRotate(
                new Vector3(0f, 0, 360), 3f, RotateMode.FastBeyond360).
                SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        StartCoroutine(SelfDestory());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

    IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(50f);
        Destroy(this.gameObject);
    }
}
