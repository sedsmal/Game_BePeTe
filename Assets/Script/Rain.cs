using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour
{
    RawImage rawImage;
    float amount;

    private void Start()
    {
        amount = 0;
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        amount += Time.deltaTime * 10f;
        rawImage.uvRect = new Rect(0f, amount, 17f, 9f) ;
        
    }
}
