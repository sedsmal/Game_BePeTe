using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectToFind : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseUpAsButton()
    {
        VRManagment.Instance.FindAlphabet();
        Destroy(GetComponent<BoxCollider>());
        GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.4f);
        transform.DOShakeScale(0.5f, 0.2f, 2, 90).OnComplete(()=> { Destroy(this.gameObject); });
        
        
    }


}
