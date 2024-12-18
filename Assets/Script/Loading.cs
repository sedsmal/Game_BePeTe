using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System;
using ARSupportCheck;

public class Loading : SingletonComponent<Loading>
{
    string nextLevelid { get; set; }
    Animator animator;
    AsyncOperation task;
    bool isSupportAr;

    void Start()
    {
        //StartCoroutine(LoadSceneAsync());
//        Debug.Log(SaveManager.Instance.MainSaveJson);
        animator = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Loading") { ShowUnLoading();
        }

        isSupportAr = ARSupportChecker.IsSupported();
    }

    //internal void LoadLevel(object nextLevel)
    //{
    //    throw new NotImplementedException();
    //}


    public void LoadLevel()
    {
        ////SceneManager.LoadScene(nextLevelid);
        //Debug.Log(nextLevelid + SceneManager.GetActiveScene().name);//b0a9 //a0menualphabet
        

        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //check AR Support Deveice
            nextLevelid = SkipArScene(nextLevelid);

            if (!nextLevelid.Contains("0") && !Statics.tempActiveScene.Contains("Menu"))
            {
                
                SceneManager.LoadScene(nextLevelid);
            }
            else if (nextLevelid.Contains("0") && Statics.tempActiveScene.Contains("9"))
            {
                SceneManager.LoadScene("Complete alpha");
            }
            else
            {
                
                SceneManager.LoadScene(nextLevelid);
            }
        }
        else if(SceneManager.GetActiveScene().name == "Complete alpha")
        {
            SceneManager.LoadScene(Statics.nextLevel);
        }
        else
        {
            Statics.tempActiveScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Loading");
        }
    }

    string SkipArScene(string sceneName)
    {
        Debug.Log(isSupportAr+":is Support AR");
        if (!isSupportAr)
        {

            if (sceneName.Contains("8"))
                sceneName = sceneName.Remove(sceneName.Length-1)+'9'; 
        }
        return sceneName;
    }
    public void ShowLoading(string nextLevel)
    {
        nextLevelid = nextLevel;
        Statics.nextLevel = nextLevel;
        animator.SetTrigger("Load");
        EndSession();
        StartSession(); 
    }

    public void ShowUnLoading()
    {
        animator.SetTrigger("UnLoad");
    }
    public void StartSession()
    {
        XRGeneralSettings instance = XRGeneralSettings.Instance;
        if (instance != null)
        {
            StartCoroutine(instance.Manager.InitializeLoader());
            instance.Manager.StartSubsystems();
        }
    }

    public void EndSession()
    {
        XRGeneralSettings instance = XRGeneralSettings.Instance;
        if (instance != null)
        {
            instance.Manager.DeinitializeLoader();
            instance.Manager.StopSubsystems();
        }
    }

    private void Update()
    {
        if (task == null) return;

        if (task.isDone)
        {


        }
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(7f);

        task = SceneManager.LoadSceneAsync(nextLevelid, LoadSceneMode.Additive);

        task.allowSceneActivation = true;

        while (!task.isDone)
        {
            yield return null;
        }

    }

    public void PlayLevel()
    {
        SceneManager.UnloadSceneAsync("Loading");
    }
}
