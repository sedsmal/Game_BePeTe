using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;

public class AuthenticationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (ObscuredPrefs.GetString("ParentLockCode") == "")
        {
            ObscuredPrefs.SetString("ParentLockCode", "10 + 99 = ؟");
            ObscuredPrefs.SetString("ParentLockCodeAnsware", "109");
            ObscuredPrefs.SetInt("stT", 21);
            ObscuredPrefs.SetInt("stold", 0);
        }
    }
}
