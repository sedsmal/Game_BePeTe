using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;
using System;

public class SpaceManager : SingletonComponent<SpaceManager>,ISaveable
{
    public GameObject[] planet;
    public Image[] goals;
    float timePassed = 0f;
    int count;
    public GameObject planetParentObject;
    public GameObject winPopup;
    GameObject ship;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;
    private Vector3 defaultGoalScale;

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
            countOfPlay     = 0;
            lastTimeOfPlay  = 0;
            bestTimeOfPlay  = 0;
            totalTimeOfPlay = 0;
            countOfTouch    = 0;
            totalMovementMagnitude = 0f;
            averageMovementMagnitude = 0f;
            countOfCorrectTouch = 0;
            Save();

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

        ship = GameObject.Find("Ship");
        count = 0;
        foreach (Image a in goals)
        {
            a.color = new Color(1, 1, 1, 0.3f);
        }
        defaultGoalScale = goals[0].transform.localScale;
        StartCoroutine(PlaySound("LevelIntro"));
        winPopup.SetActive(false);
        ShowHelp();
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

    public void PlaySoundImmediately(string soundname)
    {
        SoundManager.Instance.Play(soundname);
    }

    IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.Play(soundname);
    }

    private void Update()
    {
        if (count < goals.Length)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 2f)
            {
                CreatePlanet();
                timePassed = 0f;
            }
        }

    }

    private void CreatePlanet()
    {
        int count = UnityEngine.Random.Range(1, 2);
        for (int i = 0; i < count; i++)
        {
            GameObject ballone = planet[UnityEngine.Random.Range(0, planet.Length)];
            Vector3 position = new Vector3(transform.position.x , transform.position.y + UnityEngine.Random.Range(-4, 4), transform.position.z);
            GameObject newBallone = Instantiate(ballone, position, Quaternion.identity);
            newBallone.transform.SetParent(planetParentObject.transform);
        }


    }

    public void CollectPlanet()
    {
        count++;
        goals[count - 1].color = new Color(1, 1, 1, 1);
        goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90).OnComplete(()=> { goals[count - 1].transform.localScale = defaultGoalScale; });

        //if (UnityEngine.Random.Range(0, 5) < 2)
        //{
        //    SoundManager.Instance.Play("alphabet");
        //}
        //else
        //{

        //    SoundManager.Instance.Play("win");
        //}




        if (count == goals.Length)
        {
            CalculateTime();
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin() {
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.Play("win");
        StartCoroutine( PlaySound("like1"));
        winPopup.SetActive(true);
        
        celebrationParticle.Play();
        Destroy(ship);
        winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);

        int star = ObscuredPrefs.GetInt("st");
        ObscuredPrefs.SetInt("st", star + 7);

        for (int i = 0; i < planetParentObject.transform.childCount; i++)
        {
            Destroy(planetParentObject.transform.GetChild(i).gameObject);
        }



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
        json["totalMovementMagnitude"] = totalMovementMagnitude;
        json["averageMovementMagnitude"] = averageMovementMagnitude;
        json["countOfCorrectTouch"] = countOfCorrectTouch;
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
        totalMovementMagnitude = Convert.ToDouble(json["totalMovementMagnitude"].Value);
        averageMovementMagnitude = Convert.ToDouble(json["averageMovementMagnitude"].Value);
        countOfCorrectTouch = int.Parse(json["countOfCorrectTouch"].Value);


        return true;
    }
    #endregion
}
