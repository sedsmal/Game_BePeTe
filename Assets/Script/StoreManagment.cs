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

public class StoreManagment : SingletonComponent<StoreManagment>
{
    public GameObject confirmationPopup;
    public GameObject confirmType2Popup;
    public GameObject intenetPopup;
    public GameObject ServerErrorPopup;
    public Text ServerErrorText;
    public GameObject FreePlan;
    public GameObject OneDayPlan;
    public GameObject hiBuyPopup;

    private void Start()
    {

        confirmationPopup.SetActive(false);
        confirmType2Popup.SetActive(false);
        hiBuyPopup.SetActive(false);
        intenetPopup.SetActive(false);
        FreePlan.SetActive(false);
        OneDayPlan.SetActive(false);
        ServerManagment.Instance.Recheck();
        CheckFreePlanActivation();
        //InvokeRepeating("CheckFreePlanActivation", 5, 7);

    }

    public void CheckFreePlanActivation()
    {
        if (Statics.userData.FreePackage == false)
        {
            FreePlan.SetActive(true); OneDayPlan.SetActive(false);
        }
        else
        {
            OneDayPlan.SetActive(true); FreePlan.SetActive(false);
        }
    }

    public void OneYearUnlock()
    {
        //ShowHiBuy(7);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(365);
    }
    public void OneYearOffUnlock()
    {
        //ShowHiBuy(8);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(365);
    }
    public void SixMonthUnlock()
    {
        //ShowHiBuy(5);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(180);
    }
    public void SixMonthOffUnlock()
    {
        //ShowHiBuy(6);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(180);
    }
    public void ThreeMonthUnlock()
    {
        //ShowHiBuy(4);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(90);
    }
    public void OneMonthUnlock()
    {
        //ShowHiBuy(3);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(30);
    }
    public void WeekUnlockGame()
    {
        //ShowHiBuy(2);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(7);
    }
    public void DayUnlockGame()
    {
        //ShowHiBuy(1);
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        WorldTimeAPI.Instance.CompairTime();
        WorldTimeAPI.Instance.ActivePlan(1);
    }

    public void FreeUnlockGame()
    {
        //ShowHiBuy(0);
        if (Statics.userData != null)
        {
            if (Statics.userData.FreePackage == false)
            {
                ObscuredPrefs.SetString("_Fr", "true");
                WorldTimeAPI.Instance.CompairTime();
                WorldTimeAPI.Instance.ActivePlan(3);
                FreePlan.SetActive(false);
            }
        }
        else
        {
            if (WorldTimeAPI.Instance.isOnline)
            {
                ServerManagment.Instance.Recheck();
            }
            else
            {
                ShowInternet();
            }
        }

    }

    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
        SoundManager.Instance.Play("btn");

    }

    public void ShowConfirm()
    {
        if (!confirmationPopup.activeInHierarchy)
        {
            confirmationPopup.SetActive(true);
            confirmationPopup.GetComponent<CanvasGroup>().alpha = 0f;
            confirmationPopup.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            confirmationPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90);
        }
    }

    public void HideConfirm()
    {
        confirmationPopup.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        confirmationPopup.transform.DOShakeScale(0.2f, 0.05f, 3, 90).OnComplete(() => { confirmationPopup.SetActive(false); });
    }

    public void ShowInternet()
    {
        intenetPopup.SetActive(true);
        intenetPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90);
    }

    public void HideInternet()
    {
        
        intenetPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90).OnComplete(()=>{ intenetPopup.SetActive(false); });
    }
    public void ShowServerError()
    {
        ServerErrorPopup.SetActive(true);
        ServerErrorPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90);
    }

    public void HideServerError()
    {

        ServerErrorPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90).OnComplete(() => { ServerErrorPopup.SetActive(false); });
    }
    public void ShowConfirm2()
    {
        confirmType2Popup.SetActive(true);
        confirmType2Popup.transform.DOShakeScale(0.3f, 0.05f, 3, 90);
    }

    public void HideConfirm2()
    {

        confirmType2Popup.transform.DOShakeScale(0.3f, 0.05f, 3, 90).OnComplete(() => { confirmType2Popup.SetActive(false); });
    }

    public void ShowHiBuy(int type)
    {
        hiBuyPopup.SetActive(true);
        hiBuyPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90);
        HiBuy hibuy = hiBuyPopup.GetComponent<HiBuy>();

        if (type == 0)
        {
            //free
            hibuy.Set(0);
        }
        else if(type == 1)
        {
            hibuy.Set(1);
        }
        else if (type == 2)
        {
            hibuy.Set(2);
        }
        else if (type == 3)
        {
            hibuy.Set(3);
        }
        else if (type == 4)
        {
            hibuy.Set(4);
        }
        else if (type == 5)
        {
            hibuy.Set(5);
        }
        else if (type == 6)
        {
            hibuy.Set(6);
        }
        else if (type == 7)
        {
            hibuy.Set(7);
        }
        else if (type == 8)
        {
            //1 year normal
            hibuy.Set(8);
        }


    }

    public void HideHiBuy()
    {
        hiBuyPopup.transform.DOShakeScale(0.3f, 0.05f, 3, 90).OnComplete(() => { hiBuyPopup.SetActive(false); });
    }

    //    public IEnumerator UploadUserData(string mobile,string eds,string edm,bool getFree,string deviceCode)
    //    {
    //        var modell = new DownloadSaveFile_Result();

    //        modell.mobile = mobile;
    //        modell.exipreDateServer = eds;
    //        modell.expireDateMobile = edm;
    //        modell.freePackage = getFree;
    //        modell.deviceCode = deviceCode;

    //        //string model = JsonUtility.ToJson(modell);
    //        string model = JsonConvert.SerializeObject(modell);

    //        Debug.Log("Json Model send to server: " + model);
    //        var www = new UnityWebRequest("http://bepeteapi.sedsmall.com/api/WordTower/uploadUserData", "POST");
    //        byte[] bodyraw = Encoding.UTF8.GetBytes(model);
    //        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyraw);
    //        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //        www.SetRequestHeader("Content-Type", "application/json");
    //        yield return www.Send();
    //        //yield return www.SendWebRequest();

    //        if (www.isDone && www.error == null && www.downloadHandler.text == "Done")
    //        {
    //            Debug.Log(www.downloadHandler.text);
    //            Debug.Log("Server: Data Saved ");
    //        }
    //        else if (www.isDone && www.downloadHandler.text == "SqlError")
    //        {
    //            Debug.Log("Server: Sql return no record");
    //        }
    //        else if (www.isDone)
    //        {
    //            Debug.Log("Server: Exception on save data" + www.downloadHandler.text);
    //        }
    //        else
    //        {
    //            Debug.Log("Unity: Dont save, I have a problem on connection: " + www.error);
    //        }
    //        www.Dispose();
    //    }

    //    public void GetDataByMobile(string mobile)
    //    {
    //        StartCoroutine(GetInfoByMobile(mobile));
    //    }

    //    public IEnumerator GetInfoByMobile(string mobile)
    //    {

    //        string webURLGet = "http://bepeteapi.sedsmall.com/api/WordTower/getuserPackageDataByMobile?mobile=" + mobile;

    //        UnityWebRequest userData = UnityWebRequest.Get(webURLGet);

    //        yield return userData.SendWebRequest();



    //        if (userData.isNetworkError || userData.isHttpError)
    //        {

    //            if (WorldTimeAPI.Instance.isOnline)
    //            {
    //                ShowServerError();
    //                ServerErrorText.text = userData.error;
    //            }
    //            else { ShowInternet(); }


    //        }
    //        else
    //        {
    //            Debug.Log(userData.downloadHandler.text);

    //            if (userData.downloadHandler.text != "null")
    //            {
    //                Statics.userData = JsonConvert.DeserializeObject<DownloadSaveFile_Result>(userData.downloadHandler.text);
    //            }
    //            else
    //            {
    //                Statics.userData = null;

    //            }

    //        }

    //    }
}
