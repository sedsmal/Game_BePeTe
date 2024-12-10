using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpaceBackgroundMove : MonoBehaviour
{
    RawImage rawImage;
    float amount;
    public float speed;

    private void Start()
    {
        amount = 0;
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        amount += Time.deltaTime * speed;
        rawImage.uvRect = new Rect(amount, rawImage.uvRect.y, rawImage.uvRect.width, rawImage.uvRect.height);
    }
}
