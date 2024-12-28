using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;

public class VRManagment : SingletonComponent<VRManagment>,ISaveable
{
    public Image[] goals;
    public GameObject winPopup;
    int count;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;

    #region Analyzer
    int countOfPlay;
    int countOfTouch;
    int countOfCorrectTouch;
    int lastTimeOfPlay;
    int totalTimeOfPlay;
    int bestTimeOfPlay;
    #endregion
    private Gyroscope gyroscope;
    private bool isGyroAvailable = false;

    public float movementMagnitude; // This will hold the calculated movement


    #region Properties
    JSONNode savedJson;
    public string SaveId { get { return SceneManager.GetActiveScene().name+" training"; } }

    #endregion

    private void Awake()
    {
        base.Awake();
        SaveManager.Instance.Register(this);
        if (!LoadSave())
        {
            countOfPlay = 0;
            countOfTouch = 0;

            lastTimeOfPlay = 0;
            bestTimeOfPlay = 0;
            totalTimeOfPlay = 0;
            movementMagnitude = 0f;
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

    // Start is called before the first frame update
    void Start()
    {
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

        foreach (Image a in goals)
        {
            a.color = new Color(1, 1, 1, 0.3f);
        }
        StartCoroutine(PlaySound("LevelIntro"));
        winPopup.SetActive(false);

        Timer.Instance.beginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CalcGyroMovement();
    }

    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }

    IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.Play(soundname);
    }

    public void FindAlphabet()
    {
        count++;
        countOfCorrectTouch++;
        goals[count - 1].color = new Color(1, 1, 1, 1);
        goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90);
        SoundManager.Instance.Play("alphabet");
        SoundManager.Instance.Play("win");
        if (count % 2 == 0)
        {
            if (UnityEngine.Random.Range(0, 10) < 6)
            {
                StartCoroutine(PlaySound("Ali"));
            }
        }
        else
        {
            if (UnityEngine.Random.Range(0, 10) < 6)
            {
                StartCoroutine(PlaySound("Afarin"));
            }
        }
        
        HideHelp();
        if (count == goals.Length)
        {
            CalculateTime();
            SaveManager.Instance.SaveNow();
            //gyroscope.enabled = false;
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(PlaySound("like3"));
        winPopup.SetActive(true);
        celebrationParticle.Play();
        winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);
        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);
        
    }


    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
    }

    public void HideHelp()
    {
        if (HelpFinger != null) { HelpFinger.SetActive(false); }


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
    public bool IsHelpActive()
    {
        return HelpFinger.activeInHierarchy;
    }

    public void CalculateTime()
    {
        int sec = Timer.Instance.WhatTimeIsIt();
        int min = Timer.Instance.WhatMinIsIt();
        int time = min * 60 + sec;

        countOfPlay = countOfPlay+1;
        totalTimeOfPlay = time + totalTimeOfPlay;
        if (time < bestTimeOfPlay && bestTimeOfPlay != 0)
        {
            bestTimeOfPlay = time;
        }
        lastTimeOfPlay = time;

        Save();
        SaveManager.Instance.SaveNow();
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

        return json;
    }

    public bool LoadSave()
    {
        JSONNode json = SaveManager.Instance.LoadSave(this);
        Debug.Log(json);
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
