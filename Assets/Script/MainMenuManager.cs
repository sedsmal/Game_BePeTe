using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using DG.Tweening;
using CodeStage.AntiCheat.Storage;
using UPersian.Utils;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using ArabicSupport;

public class MainMenuManager : SingletonComponent<MainMenuManager>
{
    //public GameObject lockpopUp;
    public InputField inputField;
    public GameObject parentAuthen;
    public GameObject timeDetection;
    private Gyroscope gyro;
    public Text starText;

    public bool gyroEnabled = false;

    private void Start()
    {
        gyroEnabled = EnableGyro();

        //if(SceneManager.GetActiveScene().name== "Menu Alphabets 1")
        //{
        //    lockpopUp.SetActive(false);
        //}
        if (ObscuredPrefs.GetString("ParentLockCode") == ""||!ObscuredPrefs.HasKey("ParentLockCode"))
        {
            ObscuredPrefs.SetString("ParentLockCode", "10 + 99 = ؟");
            ObscuredPrefs.SetString("ParentLockCodeAnsware", "109");
        }

        parentAuthen.SetActive(false);
        timeDetection.SetActive(false);

        SoundManager.Instance.Play("music1");
        StartCoroutine(PlaySound("LevelIntro"));
        ServerManagment.Instance.InitializeServer();
        starText.text=ArabicFixer.Fix( ObscuredPrefs.GetInt("st").ToString(),true,true);

    }

    private void GiveStar()
    {
        if (ObscuredPrefs.GetInt("st") > ObscuredPrefs.GetInt("stold"))
        {
            starText.DOText(ObscuredPrefs.GetInt("st").ToString(),1f);
        }
    }
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }
    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
        SoundManager.Instance.Play("btn");
        //PlayAlphabetSound(levelName);
    }

    public void PlayAlphabetSound(string levelName) {

        if (levelName == "B0")
        {
            SoundManager.Instance.Play("B");
        }
        else if (levelName == "A0")
        {
            SoundManager.Instance.Play("A");
        }
        else if (levelName == "Y0")
        {
            SoundManager.Instance.Play("Y");
        }
        else if (levelName == "Z20")
        {
            SoundManager.Instance.Play("Z2");
        }
        else if (levelName == "T20")
        {
            SoundManager.Instance.Play("T2");
        }
        else if (levelName == "T0")
        {
            SoundManager.Instance.Play("T");
        }
        else if (levelName == "P0")
        {
            SoundManager.Instance.Play("P");
        }
        else if (levelName == "C30")
        {
            SoundManager.Instance.Play("C3");
        }
        else if (levelName == "J0")
        {
            SoundManager.Instance.Play("J");
        }
        else if (levelName == "Ch0")
        {
            SoundManager.Instance.Play("Ch");
        }
        else if (levelName == "Hj0")
        {
            SoundManager.Instance.Play("Hj");
        }
        else if (levelName == "Kh0")
        {
            SoundManager.Instance.Play("Kh");
        }
        else if (levelName == "Zl0")
        {
            SoundManager.Instance.Play("Zl");
        }
        else if (levelName == "D0")
        {
            SoundManager.Instance.Play("D");
        }
        else if (levelName == "Zr0")
        {
            SoundManager.Instance.Play("Zr");
        }
        else if (levelName == "R0")
        {
            SoundManager.Instance.Play("R");
        }
        else if (levelName == "Zh0")
        {
            SoundManager.Instance.Play("Zh");
        }
        else if (levelName == "S0")
        {
            SoundManager.Instance.Play("S");
        }
        else if (levelName == "Sh0")
        {
            SoundManager.Instance.Play("Sh");
        }
        else if (levelName == "Sd0")
        {
            SoundManager.Instance.Play("Sd");
        }
        else if (levelName == "Zd0")
        {
            SoundManager.Instance.Play("Zd");
        }
        else if (levelName == "T20")
        {
            SoundManager.Instance.Play("T2");
        }
        else if (levelName == "Z20")
        {
            SoundManager.Instance.Play("Z2");
        }
        else if (levelName == "Eyn0")
        {
            SoundManager.Instance.Play("Eyn");
        }
        else if (levelName == "Ghyn0")
        {
            SoundManager.Instance.Play("Ghyn");
        }
        else if (levelName == "F0")
        {
            SoundManager.Instance.Play("F");
        }
        else if (levelName == "Gh0")
        {
            SoundManager.Instance.Play("Gh");
        }
        else if (levelName == "K0")
        {
            SoundManager.Instance.Play("K");
        }
        else if (levelName == "G0")
        {
            SoundManager.Instance.Play("G");
        }
        else if (levelName == "L0")
        {
            SoundManager.Instance.Play("L");
        }

        else if (levelName == "M0")
        {
            SoundManager.Instance.Play("M");
        }
        else if (levelName == "N0")
        {
            SoundManager.Instance.Play("N");
        }
        else if (levelName == "V0")
        {
            SoundManager.Instance.Play("V");
        }
        else if (levelName == "H0")
        {
            SoundManager.Instance.Play("H");
        }
        else if (levelName == "Practice0")
        {
            SoundManager.Instance.Play("Practice");
        }
        else if (levelName == "Alpha")
        {
            SoundManager.Instance.Play("Alpha");
        }


    }


    public void laught()
    {
        SoundManager.Instance.Play("kidslau");
    }

    public void babytalk()
    {
        SoundManager.Instance.Play("kidstalk");
    }
    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(0.75f);
        SoundManager.Instance.Play(soundname);
    }

    //public void ShowLockInfo()
    //{
    //    if (!lockpopUp.activeInHierarchy)
    //    {
    //        lockpopUp.SetActive(true);
    //        lockpopUp.GetComponent<CanvasGroup>().alpha = 0f;
    //        lockpopUp.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    //        lockpopUp.transform.DOShakeScale(0.3f, 0.5f, 3, 90);
    //    }
    //}

    //public void HideLockInfo()
    //{
    //    lockpopUp.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
    //    lockpopUp.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { lockpopUp.SetActive(false); });

    //}

    //public void UnlockGame()
    //{
    //    WorldTimeAPI.Instance.CompairTime();
    //    WorldTimeAPI.Instance.activeTest();
    //    //hi();
    //}

    public void Type(string character)
    {
        if (inputField.text.Length < 6) { inputField.text += character; }


    }

    public void Remove()
    {
        if (inputField.text.Length > 0) { inputField.text = inputField.text.Remove(inputField.text.Length - 1); }


    }

    public void Confirm()
    {
        if (inputField.text == ObscuredPrefs.GetString("ParentLockCodeAnsware"))
        {
            SoundManager.Instance.Play("btn");

            //if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Main Menu New")
            //{
            //    Loading.Instance.ShowLoading("Parents Menu");
            //}
            //else if (SceneManager.GetActiveScene().name == "Menu Alphabets 1" || SceneManager.GetActiveScene().name == "Menu Alphabets new")
            //{
            //    Loading.Instance.ShowLoading("Store");

            //}

            Loading.Instance.ShowLoading("Parents Menu");


        }
        else
        {
            SoundManager.Instance.Play("wrong parents");
            inputField.text = "";
        }
    }

    public void ShowParentAuthentication()
    {
        if (!parentAuthen.activeInHierarchy)
        {
            parentAuthen.SetActive(true);
            parentAuthen.GetComponent<CanvasGroup>().alpha = 0f;
            parentAuthen.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            parentAuthen.transform.DOShakeScale(0.3f, 0.5f, 3, 90);
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                StartCoroutine(PlaySound("parents"));
            }
            else if (SceneManager.GetActiveScene().name == "Menu Alphabets 1")
            {
               // SoundManager.Instance.Play("buyPremium");
                StartCoroutine(PlaySound("buyPremium"));

            }

        }




    }
    public void HideParentAuthentication()
    {
        //parentAuthen.transform.DOScale(Vector3.zero, 0.1f).OnComplete(()=> { parentAuthen.SetActive(false); });
        parentAuthen.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        parentAuthen.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { parentAuthen.SetActive(false); });

    }


    public void ShowTimeDetection()
    {
        if (!parentAuthen.activeInHierarchy)
        {
            timeDetection.SetActive(true);
            timeDetection.GetComponent<CanvasGroup>().alpha = 0f;
            timeDetection.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            timeDetection.transform.DOShakeScale(0.3f, 0.5f, 3, 90);

        }




    }
    public void HidetimeDetection()
    {
        //parentAuthen.transform.DOScale(Vector3.zero, 0.1f).OnComplete(()=> { parentAuthen.SetActive(false); });
        timeDetection.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        timeDetection.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { timeDetection.SetActive(false); });

    }

}
