using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;

public class ParentQuestion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = ObscuredPrefs.GetString("ParentLockCode");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
