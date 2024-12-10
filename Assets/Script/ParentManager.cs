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

public class ParentManager : SingletonComponent<ParentManager>
{
    public Text leftTime;
    public Text onlineStatus;
    //public GameObject timePopup;
    //public GameObject countPopup;
    public GameObject markPopup;
    public GameObject supportPopup;
    public GameObject internetPopup;
    public GameObject lockGamePopup;
    public GameObject settingPopup;
    public GameObject loginPopup;
    public GameObject changePasswordPopup;
    public GameObject activeLoginPopup;
    public InputField phoneNumber;
    public InputField codeNumber;
    public GameObject phoneError;
    public GameObject codeError;
    string webURLGet = "";
    string saveURL = "";
    public GameObject waitButton;
    // Start is called before the first frame update

    void Start()
    {
        //countPopup.SetActive(true);
        //countPopup.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(960, 0, 0);
        //countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        //countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
        //countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;

        //timePopup.SetActive(true);
        //timePopup.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(960, 0, 0);
        //timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        //timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
        //timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;

        markPopup.SetActive(true);
        markPopup.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(960, 0, 0);
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;

        waitButton.SetActive(false);
        //manage login
        if (ObscuredPrefs.GetString("_N") == "")
        {
            loginPopup.SetActive(true);
            activeLoginPopup.SetActive(false);
        }
        else
        {
            loginPopup.SetActive(false);
            activeLoginPopup.SetActive(true);
        }


        phoneError.SetActive(false);
        codeError.SetActive(false);
        supportPopup.SetActive(false);
        settingPopup.SetActive(false);
        internetPopup.SetActive(false);
        lockGamePopup.SetActive(false);
        HidePasswordPopup();

        ServerManagment.Instance.Recheck();


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(WorldTimeAPI.Instance.leftMinute);
        WriteLeftTime();

        if (WorldTimeAPI.Instance.isOnline)
        {
            onlineStatus.text = "ﺖﻧﺮﺘﻨﯾﺍ ﻪﺑ ﻞﺼﺘﻣ";
        }
        else { onlineStatus.text = "ﺖﻧﺮﺘﻨﯾﺍ ﻪﺑ ﻝﺎﺼﺗﺍ ﻡﺪﻋ"; }
    }

    public void WriteLeftTime()
    {
        leftTime.text = " ﻪﻘﯿﻗﺩ " +
    ArabicFixer.Fix(WorldTimeAPI.Instance.leftMinute.ToString(), true, true) + " ﺖﻋﺎﺳ " +
    ArabicFixer.Fix(WorldTimeAPI.Instance.leftHours.ToString(), true, true) + " ﺯﻭﺭ " + ArabicFixer.Fix(WorldTimeAPI.Instance.leftDay.ToString(), true, true)
    ;
    }
    //public void ShowTimePopup()
    //{
    //    timePopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(-960, 1.5f);
    //    timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = true;
    //    timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
    //    timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    //}

    //public void HideTimePopup()
    //{
    //    timePopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(960, 1.5f);
    //    timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, 0.4f).OnComplete(() =>
    //    { timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable=false;
    //        timePopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
    //    }); 
    //}

    public void ShowMarkPopup()
    {
        StudentMarkManager.Instance.Initialize();
        markPopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(-960, 0.5f);
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = true;
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    }

    public void HidemarkPopup()
    {
        markPopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(960, 0.3f);
        markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, 0.4f).OnComplete(() =>
        {
            markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
            markPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
        });
    }

    //public void ShowCountPopup()
    //{
    //    countPopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(-960, 1.5f);
    //    countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable=true;
    //    countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
    //    countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    //}

    //public void HideCountPopup()
    //{
    //    countPopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(960, 1.5f);
    //    countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, 0.4f).OnComplete(() =>
    //    { countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
    //        countPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
    //    });
    //}

    public void LoadLevel(string levelName)
    {
            //if(levelName== "Store"&& ObscuredPrefs.GetString("_N")=="")
            //{

            //}else if (levelName == "Store" && ObscuredPrefs.GetString("_N") != "")
            //{
            //    Loading.Instance.ShowLoading(levelName);
            //}
            //else
            //{
            
            //}
        Loading.Instance.ShowLoading(levelName);
        SoundManager.Instance.Play("btn");
        //PlayAlphabetSound(levelName);
    }

    public void Login()
    {
        // if phone and code true and user dont exist add the user and time fo subsctibe
        //if phone and code true and user exist return the time of subscribe
        if (waitButton.activeInHierarchy)
        {
            waitButton.transform.GetChild(9).gameObject.GetComponent<Image>().DOFillAmount(0.01f, 90).
                OnComplete(() => { waitButton.SetActive(false); });
        }
        if (phoneNumber.text.Length != 11 || phoneNumber.text[0] != '0')
        {
            phoneError.SetActive(true);
        }
        else
        {
            phoneError.SetActive(false);
        }
        //Debug.Log("AA"+ Statics.smsCode.ToString()+ codeNumber.text);
        if (codeNumber.text.Length == 6 && codeNumber.text == Statics.smsCode.ToString())
        {
            if (phoneNumber.text.Length == 11 || phoneNumber.text[0] == '0')
            {
                ObscuredPrefs.SetString("_N", phoneNumber.text);
                ServerManagment.Instance.UploadLatestData();
                loginPopup.SetActive(false);
                activeLoginPopup.SetActive(true);
                //Debug.Log("AA");
            }
        }
        else
        {
            codeError.SetActive(true);
        }
    }
    public void RequestCode()
    {

        if (WorldTimeAPI.Instance.isOnline)
        {
            if (phoneNumber.text.Length != 11 || phoneNumber.text[0] != '0')
            {
                phoneError.SetActive(true);
            }
            else
            {
                if (Statics.smsCode == 0) { Statics.smsCode = UnityEngine.Random.Range(100000, 999999); }
                Debug.Log(Statics.smsCode);
                phoneError.SetActive(false);
                StartCoroutine(SendSMS(phoneNumber.text, Statics.smsCode.ToString()));
                waitButton.SetActive(true);
                waitButton.transform.GetChild(9).gameObject.GetComponent<Image>().fillAmount = 1;
                waitButton.transform.GetChild(9).gameObject.GetComponent<Image>().DOFillAmount(0.01f, 90).
                    OnComplete(() => { waitButton.SetActive(false); });
            }
        }
        //Send phone number to server, and send a code.

    }

    public void GetSubscribeTime()
    {
        //post phone number and get the subscribe time.
        //subscribe time of server and phone must be the same.
        

    }

    public void Logout()
    {
        ObscuredPrefs.DeleteKey("_N");
        Statics.userData = null;
        loginPopup.SetActive(true);
        activeLoginPopup.SetActive(false);
    }

    IEnumerator SendSMS(string phone,string code)
    {
        UnityWebRequest getNotificationInfo = UnityWebRequest.Get("http://bepeteapi.sedsmall.com/api/WordTower/sendSMS?code="+code+ "&receiver="+ phone);
        yield return getNotificationInfo.SendWebRequest();

        if (getNotificationInfo.isNetworkError || getNotificationInfo.isHttpError)
        {

            ShowInternetPopup();
        }
    }




    public void BaleChannel()
    {
        string url = "https://ble.ir/bepete";
        Application.OpenURL(url);
    }
    public void BaleSupport()
    {
        string url = "https://ble.ir/sedsmall";
        Application.OpenURL(url);
    }
    public void TelegramSupport()
    {
        string url = "https://web.telegram.org/k/#@bepete";
        Application.OpenURL(url);
    }

    public void ShowSupportPopup()
    {
        supportPopup.SetActive(true);
        supportPopup.transform.DOShakeScale(0.3f, 0.1f, 1, 90);
    }

    public void HideSupportPopup()
    {
        supportPopup.SetActive(false);
    }
    public void ShowSettingPopup()
    {
        settingPopup.SetActive(true);
        //settingPopup.transform.DOShakeScale(0.3f, 0.5f, 3, 90);
        settingPopup.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(960, 0, 0);
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;

        settingPopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(-960, 0.5f);
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = true;
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    }

    public void HideSettingPopup()
    {

        settingPopup.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos3DX(960, 0.3f);
        settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0, 0.4f).OnComplete(() =>
        {
            settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
            settingPopup.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
            settingPopup.SetActive(false);
        });
    }
    public void ShowInternetPopup()
    {
        internetPopup.SetActive(true);
        internetPopup.transform.DOShakeScale(0.3f, 0.1f, 1, 2);
    }

    public void HideInternetPopup()
    {
        internetPopup.SetActive(false);
    }

    public void ShowLockPopup()
    {
        lockGamePopup.SetActive(true);
        lockGamePopup.transform.DOShakeScale(0.3f, 0.1f, 1, 2);
    }

    public void HideLockPopup()
    {
        lockGamePopup.SetActive(false);
    }

    public void ShowPasswordPopup()
    {
        changePasswordPopup.SetActive(true);
        changePasswordPopup.transform.DOShakeScale(0.3f, 0.1f, 1, 2);
    }

    public void HidePasswordPopup()
    {
        changePasswordPopup.SetActive(false);
    }
}
