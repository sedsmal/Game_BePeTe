using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using BizzyBeeGames;
using System.Collections.Generic;
using CodeStage.AntiCheat.Storage;
using UnityEngine.SceneManagement;

public class WorldTimeAPI : SingletonComponent<WorldTimeAPI>
{
#region Singleton class: WorldTimeAPI

	public static WorldTimeAPI Instance;
    [HideInInspector]public bool isOnline;
    [HideInInspector]public int leftDay, leftHours, leftMinute;


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

#endregion

	//json container
	struct TimeData {
		//public string client_ip;
		//...
		public string datetime;
		//..
	}

	const string API_URL = "http://worldtimeapi.org/api/ip";

	[HideInInspector] public bool IsTimeLodaed = false;

	private DateTime _currentDateTime = DateTime.Now;

	void Start ( ) {
        StartGetRealTime();
        Debug.Log("Start TIme Check:" + ObscuredPrefs.GetString("_AMT") + "AA" + ObscuredPrefs.GetString("_AST"));
        InvokeRepeating("CheckPlanActivation", 1, 20);
    }
    public void StartGetRealTime()
    {
        StartCoroutine(GetRealDateTimeFromAPI());
    }
	public DateTime GetCurrentDateTime ( ) {
        //here we don't need to get the datetime from the server again
        // just add elapsed time since the game start to _currentDateTime

        //return _currentDateTime+(System.DateTime.Now-last);
        StartGetRealTime();
        return _currentDateTime;
	}
   
    //public DateTime GetLiveTime()
    //{
    //    return _currentDateTime;
    //}
    
    
    IEnumerator GetRealDateTimeFromAPI ( ) {

        UnityWebRequest webRequest = UnityWebRequest.Get ( API_URL );

		//Debug.Log ( "getting real datetime..." );


		yield return webRequest.Send ( );

		if ( webRequest.isNetworkError ) {
			//error
//			Debug.Log ( "Error: " + webRequest.error );
            IsTimeLodaed = false;
            isOnline = false;
            StartGetRealTime();

        } else {
            //success
            isOnline = true;
			TimeData timeData = JsonUtility.FromJson<TimeData> ( webRequest.downloadHandler.text );
			//timeData.datetime value is : 2020-08-14T15:54:04+01:00

			_currentDateTime = ParseDateTime ( timeData.datetime );
			IsTimeLodaed = true;

		//	Debug.Log ( "Success."+ webRequest.downloadHandler.text);
		}
	}
	//datetime format => 2020-08-14T15:54:04+01:00
	DateTime ParseDateTime ( string datetime ) {
		//match 0000-00-00
		string date = Regex.Match ( datetime, @"^\d{4}-\d{2}-\d{2}" ).Value;

		//match 00:00:00
		string time = Regex.Match ( datetime, @"\d{2}:\d{2}:\d{2}" ).Value;

		return DateTime.Parse ( string.Format ( "{0} {1}", date, time ) );
	}

    //private void OnApplicationPause(bool pause)
    //{
    //    last = System.DateTime.Now;
    //}
    private void OnApplicationFocus(bool focus)
    {
        StartGetRealTime();
    }

    public void CompairTime()
    {
        DateTime mobileTime = DateTime.UtcNow;
        DateTime serverTime = WorldTimeAPI.Instance.GetCurrentDateTime();

        TimeSpan diffrence = mobileTime - serverTime;

        //check if the diffrence 
        float diff = (float)diffrence.TotalMinutes;

        ObscuredPrefs.SetFloat("_TDif", diff);
        Debug.Log("time deffrence is:" + diff);
    }

    public bool isCheat()
    {
        DateTime mobileTime = DateTime.UtcNow;

        TimeSpan mobileDiffrence = DateTime.Parse(ObscuredPrefs.GetString("_AMT")) - mobileTime;
       
        if(Mathf.CeilToInt((float) mobileDiffrence.TotalDays)-1 > ObscuredPrefs.GetInt("_LD"))
        {
            return true;
        }

        return false;
    }

   
    //public void activeTest() { ActivePlan(3); }

    public void ActivePlan(int days)
    {
        if (isOnline)
        {

            if (ObscuredPrefs.GetString("_AMT") == "")
            {
                DateTime activeMobileTime = DateTime.UtcNow.AddDays(days);
                ObscuredPrefs.SetString("_AMT", activeMobileTime.ToString());

            }
            else
            {
                DateTime activeMobileTime = DateTime.Parse(ObscuredPrefs.GetString("_AMT"));
                 activeMobileTime = activeMobileTime.AddDays(days);
                ObscuredPrefs.SetString("_AMT", activeMobileTime.ToString());
            }



            //_AST mean active server time
            if (ObscuredPrefs.GetString("_AST") == "")
            {
                DateTime activeServerTime = WorldTimeAPI.Instance.GetCurrentDateTime().AddDays(days);
                ObscuredPrefs.SetString("_AST", activeServerTime.ToString());

            }
            else
            {
                DateTime activeServerTime = DateTime.Parse(ObscuredPrefs.GetString("_AST"));
                activeServerTime = activeServerTime.AddDays(days);
                ObscuredPrefs.SetString("_AST", activeServerTime.ToString());
            }
                

            //active plan _P mean purchase, B1 means buy
            ObscuredPrefs.SetString("_P", "B1");

            if(SceneManager.GetActiveScene().name== "Store")
            {
                if (ObscuredPrefs.GetString("_N") != "")
                {
                    StoreManagment.Instance.ShowConfirm2();
                }
                else
                {
                    StoreManagment.Instance.ShowConfirm();
                }
                
            }

            

        }
        else
        {
            Debug.Log("we are offline");
            StoreManagment.Instance.ShowInternet();
        }

        ServerManagment.Instance.UploadLatestData();

    }


    public void CheckPlanActivation()
    {
        if (ObscuredPrefs.HasKey("_AMT")&& ObscuredPrefs.GetString("_AMT")!=""&& ObscuredPrefs.GetString("_AST") != ""&& ObscuredPrefs.HasKey("_AST"))
        {
           // Debug.Log("checkPlan Activation 1:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
            DateTime mobileTime = DateTime.UtcNow;

            TimeSpan mobileDiffrence =  DateTime.Parse(ObscuredPrefs.GetString("_AMT"))- mobileTime  ;
            leftDay = mobileDiffrence.Days;
            leftHours = mobileDiffrence.Hours; ;
            leftMinute = mobileDiffrence.Minutes;
            

            // Debug.Log("___"+WorldTimeAPI.Instance.GetCurrentDateTime());
            if (isOnline)
            {
                DateTime serverTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                TimeSpan serverDiffrence =  DateTime.Parse(ObscuredPrefs.GetString("_AST"))- serverTime  ;

                //if (!isCheat())
                //{
                if (serverDiffrence.Minutes < 0 && ObscuredPrefs.HasKey("_P"))
                {

                    Debug.Log("plan finished");
                    ObscuredPrefs.DeleteKey("_P");
                    ObscuredPrefs.DeleteKey("_AMT");
                    ObscuredPrefs.DeleteKey("_AST");
                    leftDay = 0;
                    leftHours = 0;
                    leftMinute = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    ServerManagment.Instance.UploadLatestData();
                 //   Debug.Log("checkPlan Activation 2:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
                }
                else
                {
 //                   Debug.Log("game is active");
//                    Debug.Log(serverDiffrence);
                    
                    leftDay = serverDiffrence.Days;
                    leftHours = serverDiffrence.Hours; ;
                    leftMinute = serverDiffrence.Minutes;
                    ObscuredPrefs.SetInt("_LD", leftDay);


                    //reset Mobile time
                    DateTime activeMobileTime = DateTime.UtcNow.AddDays(leftDay);
                    
                    ObscuredPrefs.SetString("_AMT", activeMobileTime.ToString());

                    if(MainMenuManager.Instance!=null)
                     MainMenuManager.Instance.HidetimeDetection();
                    ServerManagment.Instance.UploadLatestData();
                    ObscuredPrefs.SetString("_P", "B1");
                  //  Debug.Log("checkPlan Activation 3:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
                }
            }
            else
            {
                if (!isCheat())
                {
                    if (mobileDiffrence.Minutes < 0 && ObscuredPrefs.HasKey("_P"))
                    {

                        Debug.Log("plan finished");
                        ObscuredPrefs.DeleteKey("_P");
                        ObscuredPrefs.DeleteKey("_AMT");
                        ObscuredPrefs.DeleteKey("_AST");

                        leftDay = 0;
                        leftHours = 0;
                        leftMinute = 0;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                      //  Debug.Log("checkPlan Activation 4:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
                    }
                    else
                    {
                        Debug.Log("offline game is active");
                        Debug.Log(mobileDiffrence);
                        leftDay = mobileDiffrence.Days;
                        leftHours = mobileDiffrence.Hours; ;
                        leftMinute = mobileDiffrence.Minutes; ;
                        ObscuredPrefs.SetString("_P", "B1");
                        //Debug.Log("AA" + leftDay);
                        //Debug.Log("checkPlan Activation 5:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
                    }
                }
                else
                {
                    //Debug.Log("Make sure you are online");

                    if (MainMenuManager.Instance != null)
                    {
                        MainMenuManager.Instance.ShowTimeDetection();
                    }
                  //  Debug.Log("checkPlan Activation 6:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));

                }
            }

            
        }
        else
        {
           // Debug.Log("checkPlan Activation 7:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
            leftDay = 0;
            leftHours = 0;
            leftMinute = 0;
            ServerManagment.Instance.UploadLatestData();
        }

       // Debug.Log("checkPlan Activation 8:" + ObscuredPrefs.GetString("_AMT") + "" + ObscuredPrefs.GetString("_AST"));
    }

}


/* API (json)
{
	"abbreviation" : "+01",
	"client_ip"    : "190.107.125.48",
	"datetime"     : "2020-08-14T15:544:04+01:00",
	"dst"          : false,
	"dst_from"     : null,
	"dst_offset"   : 0,
	"dst_until"    : null,
	"raw_offset"   : 3600,
	"timezone"     : "Asia/Brunei",
	"unixtime"     : 1595601262,
	"utc_datetime" : "2020-08-14T15:54:04+00:00",
	"utc_offset"   : "+01:00"
}

We only need "datetime" property.
*/