using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;

public class LockManager : SingletonComponent<LockManager>,ISaveable
{
    public GameObject answare1, answare2, answare3, answare4, answare5;
    private int progress;
    public Sprite lockOpen;
   [HideInInspector]public int trueAnsware = 0;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    #region Analyzer
    int countOfPlay;
    int lastTimeOfPlay;
    int totalTimeOfPlay;
    int bestTimeOfPlay;
    int countOfTouch;
    int countOfCorrectTouch;
    #endregion

    private Gyroscope gyroscope;
    private bool isGyroAvailable = false;

    float movementMagnitude; // This will hold the calculated movement

    #region Properties
    JSONNode savedJson;
    public string SaveId { get { return SceneManager.GetActiveScene().name + " training"; } }
    #endregion

    private void Awake()
    {
        base.Awake();
        SaveManager.Instance.Register(this);
        if (!LoadSave())
        {
            countOfPlay = 0;
            lastTimeOfPlay = 0;
            bestTimeOfPlay = 0;
            totalTimeOfPlay = 0;
            countOfTouch = 0;
            movementMagnitude = 0;
            countOfCorrectTouch = 0;
            Save();

            // SaveManager.Instance.SaveNow();
            savedJson = SaveManager.Instance.DeSerialize99();

        }
        else
        {
            savedJson = SaveManager.Instance.DeSerialize99();
        }
    }

    private void Start()
    {
        progress = 0;
        winPopup.SetActive(false);
        answare1.SetActive(false);
        answare2.SetActive(false);
        answare3.SetActive(false);
        answare4.SetActive(false);
        answare5.SetActive(false);
        HideHelp();
        Timer.Instance.beginTimer();
        StartCoroutine(PlaySound("LevelIntro"));

        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true; // Enable the gyroscope
            isGyroAvailable = true;
        }
        else
        {
            Debug.LogWarning("Gyroscope not supported on this device.");
        }
    }

    public void StartPlay()
    {
        SoundManager.Instance.Play("Ready");
        answare1.SetActive(true);
        answare2.SetActive(true);
        answare3.SetActive(true);
        answare4.SetActive(true);
        answare5.SetActive(true);

    }

    public void AlphabetRealease()
    {
        SoundManager.Instance.Play("Start Level");
    }

    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
    }

    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }

    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1.2f);
        SoundManager.Instance.Play(soundname);
    }

    public void Win()
    {
        trueAnsware++;

        if (trueAnsware == 5)
        {
            CalculateTime();
            StartCoroutine(ShowWinPopup());
        }
    }

    IEnumerator ShowWinPopup()
    {
        yield return new WaitForSeconds(0.6f);
        
        StartCoroutine(PlaySound("like4"));
        winPopup.SetActive(true);
        celebrationParticle.Play();

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);
        

    }
    public void OpenLock(string name)
    {
        Debug.Log(name);

        GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = lockOpen;
    }

    public void HideHelp()
    {
        HelpFinger.SetActive(false);
    }

    public void ShowHelp()
    {
        StartCoroutine(StartHelp());
    }

    IEnumerator StartHelp()
    {
        yield return new WaitForSeconds(0.5f);
        HelpFinger.SetActive(true);
    }

    public void DestoryAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void CalculateTime()
    {
        int sec = Timer.Instance.WhatTimeIsIt();
        int min = Timer.Instance.WhatMinIsIt();
        int time = min * 60 + sec;

        countOfPlay = countOfPlay + 1;
        totalTimeOfPlay = time + totalTimeOfPlay;
        if (time < bestTimeOfPlay && bestTimeOfPlay != 0)
        {
            bestTimeOfPlay = time;
        }
        lastTimeOfPlay = time;
        SaveManager.Instance.SaveNow();
        Save();

    }
    public void TouchCounter()
    {
        // Check for touches on a touch-enabled device
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    countOfTouch++;
                    //Debug.Log("Total Touches: " + countOfTouch);
                }
            }
        }

        // Optional: Check for mouse clicks as simulated touches in the editor
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            countOfTouch++;
            //Debug.Log("Total Touches: " + countOfTouch);
        }
#endif
    }

    public void CalcGyroMovement()
    {


        if (isGyroAvailable)
        {
            // Get the gyroscope rotation rate (angular velocity in radians per second)
            Vector3 gyroRotationRate = gyroscope.rotationRate;

            // Calculate the magnitude of the rotation
            movementMagnitude = gyroRotationRate.magnitude;
        }

    }


    #region Save Methods

    public Dictionary<string, object> Save()
    {
        Dictionary<string, object> json;

        if (savedJson == null || savedJson == "")
        {

            json = new Dictionary<string, object>();
        }
        else
        {
            if (savedJson[SaveId] == null || savedJson[SaveId] == "")
            {
                json = new Dictionary<string, object>();
            }
            else
            {
                json = JsonConvert.DeserializeObject<Dictionary<string, object>>(savedJson[SaveId].ToString());
            }

        }
        json["lastTimeOfPlay"] = lastTimeOfPlay;
        json["bestTimeOfPlay"] = bestTimeOfPlay;
        json["totalTimeOfPlay"] = totalTimeOfPlay;
        json["countOfPlay"] = countOfPlay;
        json["countOfTouch"] = countOfTouch;
        json["countOfCorrectTouch"] = countOfCorrectTouch;
        json["movementMagnitude"] = movementMagnitude;
        //json["lotterylefttime"] = leftTime;

        return json;
    }

    public bool LoadSave()
    {
        JSONNode json = SaveManager.Instance.LoadSave(this);

        if (json == null)
        {

            return false;
        }

        countOfPlay = int.Parse(json["countOfPlay"].Value);
        lastTimeOfPlay = int.Parse(json["lastTimeOfPlay"].Value);
        bestTimeOfPlay = int.Parse(json["bestTimeOfPlay"].Value);
        totalTimeOfPlay = int.Parse(json["totalTimeOfPlay"].Value);
        countOfTouch = int.Parse(json["countOfTouch"].Value);
        countOfCorrectTouch = int.Parse(json["countOfCorrectTouch"].Value);
        movementMagnitude = float.Parse(json["movementMagnitude"].Value);
        return true;
    }
    #endregion
}
