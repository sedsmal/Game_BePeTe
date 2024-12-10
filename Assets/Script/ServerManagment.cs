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

public class ServerManagment : SingletonComponent<ServerManagment>
{
    public static ServerManagment Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void InitializeServer()
    {
        if (ObscuredPrefs.GetString("_N") != ""|| ObscuredPrefs.HasKey("_N"))
        {
            ServerManagment.Instance.GetDataByMobile(ObscuredPrefs.GetString("_N"));
        }
        else
        {
            ServerManagment.Instance.GetDataByDevice(SystemInfo.deviceUniqueIdentifier);
        }
    }

    public void Recheck()
    {
        if (Statics.userData == null)
        {
            if (ObscuredPrefs.GetString("_N") != "")
            {
                ServerManagment.Instance.GetDataByMobile(ObscuredPrefs.GetString("_N"));
            }
            else
            {
                ServerManagment.Instance.GetDataByDevice(SystemInfo.deviceUniqueIdentifier);
            }
        }
    }

    public void GetDataByMobile(string mobile)
    {
        StartCoroutine(GetInfoByMobile(mobile));
    }

    public IEnumerator GetInfoByMobile(string mobile)
    {

        string webURLGet = "http://bepeteapi.sedsmall.com/api/WordTower/getuserPackageDataByMobile?mobile=" + mobile;

        UnityWebRequest userData = UnityWebRequest.Get(webURLGet);

        yield return userData.SendWebRequest();

        if (userData.isNetworkError || userData.isHttpError)
        {

            Debug.Log(userData.error);
            //ServerManagment.Instance.GetDataByDevice(SystemInfo.deviceUniqueIdentifier);
            //StartCoroutine(GetInfoByMobile(mobile));

        }
        else
        {
            Debug.Log(userData.downloadHandler.text);

            if (userData.downloadHandler.text != "null")
            {
                if (Statics.userData == null)
                {
                    Statics.userData = JsonConvert.DeserializeObject<DownloadSaveFile_Result>(userData.downloadHandler.text);
                    UpdateGameInfo();
                }
            }
            else
            {
                Statics.userData = null;
                //ServerManagment.Instance.GetDataByDevice(SystemInfo.deviceUniqueIdentifier);

            }
        }
    }

    public void GetDataByDevice(string deviceID)
    {
        StartCoroutine(GetDataByDeviceIe(deviceID));
    }

    public IEnumerator GetDataByDeviceIe(string deviceID)
    {

        string webURLGet = "http://bepeteapi.sedsmall.com/api/WordTower/getuserPackageDataByDeviceCode?deviceCode=" + deviceID;

        UnityWebRequest userData = UnityWebRequest.Get(webURLGet);

        yield return userData.SendWebRequest();

        if (userData.isNetworkError || userData.isHttpError)
        {

            Debug.Log(userData.error);

        }
        else
        {
             Debug.Log(userData.downloadHandler.text);

            if (userData.downloadHandler.text != "null")
            {
                Debug.Log("device GEt:" +"  VV"+ObscuredPrefs.GetString("_AST"));
                if (Statics.userData == null) {
                    Statics.userData = JsonConvert.DeserializeObject<DownloadSaveFile_Result>(userData.downloadHandler.text);
                    UpdateGameInfo();
                    Debug.Log("device GEt2:" + "  VV" + ObscuredPrefs.GetString("_AST"));
                }

            }
            else
            {
                Statics.userData = null;

            }

        }

    }

    public  void UpdateGameInfo()
    {

        ObscuredPrefs.SetString("_N", Statics.userData.Mobile);
        Debug.Log("Updatee AA   " + Statics.userData.ExpireDateServer + "  XXX " + ObscuredPrefs.GetString("_AST"));
        
        ObscuredPrefs.SetString("_AST", Statics.userData.ExpireDateServer);
        ObscuredPrefs.SetString("_AMT", Statics.userData.ExpireDateMobile);
        if (Statics.userData.FreePackage == true)
        {
            ObscuredPrefs.SetString("_Fr", "true");
        }
        else
        {
            ObscuredPrefs.SetString("_Fr", "false");
        }

        WorldTimeAPI.Instance.CheckPlanActivation();
    }

    public class UserData
    {
        public string deviceCode;
        public bool freePackage;
        public string mobile;
        public string exipreDateServer;
        public string expireDateMobile;
        public bool StartDiscount;
    }

    public void UploadLatestData()
    {

        if (Statics.userData == null) { Statics.userData = new DownloadSaveFile_Result(); }

        Statics.userData.DeviceCode = SystemInfo.deviceUniqueIdentifier;
        
        Statics.userData.Mobile = ObscuredPrefs.GetString("_N");
        
        if(Statics.userData.ExpireDateServer == null || Statics.userData.ExpireDateServer == "")
        {
            Statics.userData.ExpireDateServer = ObscuredPrefs.GetString("_AST");
        }

        if (Statics.userData.ExpireDateMobile == null || Statics.userData.ExpireDateMobile == "")
        {
            Statics.userData.ExpireDateMobile = ObscuredPrefs.GetString("_AMT");
        }
       

        if (ObscuredPrefs.GetString("_Fr") == "true")
        {
            Statics.userData.FreePackage = true;
        }
        else
        {
            Statics.userData.FreePackage = false;
        }

        Statics.userData.StartDiscount = false;
        UploadUserData(Statics.userData);

    }
    public void UploadUserData(DownloadSaveFile_Result data)
    {
        StartCoroutine(UploadUserDataIe(data));
    }
    public IEnumerator UploadUserDataIe(DownloadSaveFile_Result data)
    {
        var modell = new UserData();
        Debug.Log("upload Data");
        modell.mobile = data.Mobile;
        modell.exipreDateServer = data.ExpireDateServer;
        modell.expireDateMobile = data.ExpireDateMobile;
        modell.freePackage = data.FreePackage;
        modell.deviceCode = data.DeviceCode;
        modell.StartDiscount = data.StartDiscount;
        //string model = JsonUtility.ToJson(modell);
        string model = JsonConvert.SerializeObject(modell);

        Debug.Log("Json Model send to server: " + model);
        var www = new UnityWebRequest("http://bepeteapi.sedsmall.com/api/WordTower/uploadUserData", "POST");
        byte[] bodyraw = Encoding.UTF8.GetBytes(model);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyraw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.Send();
        //yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        if (www.isDone && www.error == null && www.downloadHandler.text == "Done")
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Server: Data Saved ");
        }
        else if (www.isDone && www.downloadHandler.text == "SqlError")
        {
            Debug.Log("Server: Sql return no record");
        }
        else if (www.isDone)
        {
            Debug.Log("Server: Exception on save data" + www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Unity: Dont save, I have a problem on connection: " + www.error);
        }
        www.Dispose();
    }
}
