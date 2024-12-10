using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.SceneManagement;


public class BackButton : MonoBehaviour
{

    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
        SoundManager.Instance.Play("btn");

    }



}
