using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using CodeStage.AntiCheat.Storage;

public class ChangePassword : MonoBehaviour
{
    public InputField Number1, Number2;
    public GameObject error;
    // Start is called before the first frame update
    private void Start()
    {
        error.SetActive(false);
    }
    public void ChangePass()
    {
        if(Number1.text.Length>0&& Number2.text.Length > 0)
        {
            int num1 = Int32.Parse(Number1.text);
            int num2 = Int32.Parse(Number2.text);
            int answare = num1 + num2;
            error.SetActive(false);
            ObscuredPrefs.SetString("ParentLockCode", num1.ToString() + " + " + num2.ToString() + " = ؟");
            ObscuredPrefs.SetString("ParentLockCodeAnsware", answare.ToString());
            ParentManager.Instance.HidePasswordPopup();
        }
        else
        {
            error.SetActive(true);
        }

    }
}
