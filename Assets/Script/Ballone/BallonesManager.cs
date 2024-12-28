using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;
using Newtonsoft.Json;

public class BallonesManager : SingletonComponent<BallonesManager>,ISaveable
{
    public GameObject[] ballones;
    public Image[] goals;
    float timePassed = 0f;
    int count;
    public GameObject ballonesParentObject;
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

    public float movementMagnitude; // This will hold the calculated movement

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
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        foreach(Image a in goals)
        {
            a.color =new Color (0, 0, 0, 0.3f);
        }
        StartCoroutine(PlaySound("LevelIntro"));
        StartCoroutine(StartHelp());
        StartCoroutine(HideHelpWithDelay());
        winPopup.SetActive(false);
        SoundManager.Instance.Play("music1");
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
        yield return new WaitForSeconds(1.2f);
        SoundManager.Instance.Play(soundname);
    }

    private void Update()
    {
        if(count< goals.Length)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 1.5f)
            {
                CreateBallones();
                timePassed = 0f;
            }
        }

    }

    private void CreateBallones()
    {
        int count = Random.Range(1, 2);
        for (int i =0; i < count; i++)
        {
            GameObject ballone = ballones[Random.Range(0, ballones.Length)];

            Vector3 position = new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y, transform.position.z);
            GameObject newBallone = Instantiate(ballone, position, Quaternion.identity);
            newBallone.transform.SetParent(ballonesParentObject.transform);

            if (newBallone.transform.childCount > 1)
                Destroy(newBallone.transform.GetChild(newBallone.transform.childCount - 1).gameObject);
        }

        
    }

    public void HitBallone()
    {
        count++;
        goals[count - 1].color = new Color(1, 1, 1, 1);
        goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90);

        if (UnityEngine.Random.Range(0, 5) < 2)
        {
            SoundManager.Instance.Play("alphabet");
        }
        else
        {
            SoundManager.Instance.Play("like");
        }

        SoundManager.Instance.Play("win");
        CalculateTime();
        StartCoroutine(Win());

    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.4f);
        ShowWin();
    }
    private void ShowWin()
    {
        if (count == goals.Length)
        {
            int star = ObscuredPrefs.GetInt("st");
            ObscuredPrefs.SetInt("st", star+7);

            celebrationParticle.Play();
            winPopup.SetActive(true);
            
            winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);
            StartCoroutine(PlaySound("like"));

            for (int i = 0; i < ballonesParentObject.transform.childCount; i++)
            {
                Destroy(ballonesParentObject.transform.GetChild(i).gameObject);
            }
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
    IEnumerator HideHelpWithDelay()
    {
        yield return new WaitForSeconds(4f);
        HideHelp();
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
