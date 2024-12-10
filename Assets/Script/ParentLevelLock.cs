using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;
using UnityEngine.UI;


public class ParentLevelLock : MonoBehaviour
{
    public string iD;
    public GameObject lockIcon;
    private void Start()
    {
        if (ObscuredPrefs.GetString("ParentLock" + iD) == "")
        {
            lockIcon.SetActive(false);
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1);

        }
        else
        {
            lockIcon.SetActive(true);
            transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
        }
    }

    public void LockUnlock()
    {
        if (ObscuredPrefs.GetString("ParentLock" + iD) == "")
        {
            Lock() ;

        }
        else
        {
            UnLock();
        }
    }
    public void Lock()

    {
        ObscuredPrefs.SetString("ParentLock" + iD, "is");
        lockIcon.SetActive(true);
        transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f,0.5f);

    }

    public void UnLock()

    {
        ObscuredPrefs.SetString("ParentLock" + iD, "");
        lockIcon.SetActive(false);
        transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f,1);
    }
}
