using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ARalphabetAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(transform.position + new Vector3(0, 12f, 0), 5f).SetLoops(-1,LoopType.Incremental);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
