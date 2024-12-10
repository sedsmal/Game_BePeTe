using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

public class SpaceManager : SingletonComponent<SpaceManager>
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
        int count = Random.Range(1, 2);
        for (int i = 0; i < count; i++)
        {
            GameObject ballone = planet[Random.Range(0, planet.Length)];
            Vector3 position = new Vector3(transform.position.x , transform.position.y + Random.Range(-4, 4), transform.position.z);
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
            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin() {
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.Play("win");
        StartCoroutine( PlaySound("like1"));
        winPopup.SetActive(true);
        CalculateTime();
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
        int count = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name + "Count");
        int lastTime = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name);
        ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name, time + lastTime);
        ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name + "Count", count + 1);
    }
}
