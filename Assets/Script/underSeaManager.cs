using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;
using System;

public class underSeaManager : SingletonComponent<underSeaManager>,ISaveable
{

    private int progress;
    int trueAnsware = 0;
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

    double movementMagnitude; // This will hold the calculated movement
    double totalMovementMagnitude;
    double averageMovementMagnitude;
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
            totalMovementMagnitude = 0f;
            averageMovementMagnitude = 0f;
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
        StartCoroutine(PlaySound("LevelIntro"));
        StartCoroutine(StartHelp());
        Timer.Instance.beginTimer();

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
    }

    public void Progress()
    {
        progress++;

        if (progress == 1)
        {
            SoundManager.Instance.Play("like2");
        }
    }

    public void AlphabetRealease()
    {
        SoundManager.Instance.Play("Start Level");
    }

    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
    }

    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1.1f);
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
        yield return new WaitForSeconds(0.4f);
        winPopup.SetActive(true);
        
        StartCoroutine(PlaySound("like4"));
        celebrationParticle.Play();

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);

    }

    public void HideHelp()
    {
        HelpFinger.SetActive(false);
    }

    IEnumerator StartHelp()
    {
        yield return new WaitForSeconds(0.5f);
        HelpFinger.SetActive(true);
    }
    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
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

        Save();

        //countOfPlay = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name + "Count");
        //ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name, time + lastTimeOfPlay);
        //ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name + "Count", countOfPlay + 1);
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
            totalMovementMagnitude = 0f;
            // Get the gyroscope rotation rate (angular velocity in radians per second)
            Vector3 gyroRotationRate = gyroscope.rotationRate;

            // Calculate the magnitude of the rotation
            movementMagnitude = gyroRotationRate.magnitude;
            totalMovementMagnitude += movementMagnitude * Time.deltaTime;


            if (averageMovementMagnitude != 0)
            {
                averageMovementMagnitude = (averageMovementMagnitude + totalMovementMagnitude) / 2f;
            }
            else
            {
                averageMovementMagnitude = totalMovementMagnitude;
            }
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
        json["totalMovementMagnitude"] = totalMovementMagnitude;
        json["averageMovementMagnitude"] = averageMovementMagnitude;
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
        totalMovementMagnitude = Convert.ToDouble(json["totalMovementMagnitude"].Value);
        averageMovementMagnitude = Convert.ToDouble(json["averageMovementMagnitude"].Value);
        return true;
    }
    #endregion
}
