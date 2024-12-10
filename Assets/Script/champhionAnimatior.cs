using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class champhionAnimatior : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void ChangeLayer(int n)
    {
        this.GetComponent<Canvas>().sortingOrder = n;
    }
}
