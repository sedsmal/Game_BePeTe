using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void Replay()
    {

        LoadLevel(SceneManager.GetActiveScene().name);

    }
    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1;
        Loading.Instance.ShowLoading(levelName);


    }
}
