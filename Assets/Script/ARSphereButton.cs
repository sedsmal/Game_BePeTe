using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSphereButton : MonoBehaviour
{
    public bool isIncrease;
    public bool isRotate;
    public bool isRotateLeft;
    private void OnMouseDown()
    {
        transform.localPosition = transform.localPosition + new Vector3(0, -0.1f, +0.4f);



        
        
    }

    private void OnMouseOver()
    {
        if (!isRotate)
        {
            if (isIncrease)
            {
                FloorPlacementController.Instance.MakeAlphabetBigger();
            }
            else
            {
                FloorPlacementController.Instance.MakeAlphabetSmaller();
            }
        }
        if (isRotate)
        {
            if (isRotateLeft) { FloorPlacementController.Instance.RotateAlphabet(); }

            else
            {
                FloorPlacementController.Instance.RotateLeftAlphabet();
            }
        }
             
    }
    private void OnMouseUp()
    {
        transform.localPosition = transform.localPosition + new Vector3(0, +0.1f, -0.4f);
        FloorPlacementController.Instance.StopAlphabetAnimation();
    }
}
