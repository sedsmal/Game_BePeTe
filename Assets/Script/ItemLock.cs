using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.Storage;
using BizzyBeeGames;
using System;

public class ItemLock : MonoBehaviour
{
    public bool OpenAtStart;
    public string iD;
    private bool isLock;
    private Image lockImage;
    // Start is called before the first frame update
    void Start()
    {
        lockImage = transform.GetChild(transform.childCount-1).GetComponent<Image>();
        if (!OpenAtStart)
        {
            Debug.Log(iD+ ObscuredPrefs.GetString("ParentLock" + iD));
            if (ObscuredPrefs.GetString("_P") == "B1" && ObscuredPrefs.GetString("ParentLock" + iD) == "")
            {
                lockImage.color = new Color(1, 1, 1, 0);
                isLock = false;
            }
            else
            {
                isLock = true;
                lockImage.color = new Color(1, 1, 1, 1);
            }
        }
        else {
            if (ObscuredPrefs.GetString("ParentLock" + iD) == "")
            {
                lockImage.color = new Color(1, 1, 1, 0);
                isLock = false;
            }
            else
            {
                isLock = true;
                lockImage.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        if (!OpenAtStart)
        {
            if (!isLock && ObscuredPrefs.GetString("ParentLock" + iD) == "")
            {
                Loading.Instance.ShowLoading(levelName);
                SoundManager.Instance.Play("btn");
            }
            else
            {

                MainMenuManager.Instance.ShowParentAuthentication();
            }
        }
        else
        {

            if (ObscuredPrefs.GetString("ParentLock" + iD) == "")
            {
                Loading.Instance.ShowLoading(levelName);
                SoundManager.Instance.Play("btn");
            }
            else
            {

                MainMenuManager.Instance.ShowParentAuthentication();

            }
        }


        
        //PlayAlphabetSound(levelName);
    }



 

}
