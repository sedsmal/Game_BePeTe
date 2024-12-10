using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.UI;

public class ChamphionManager : MonoBehaviour
{
    private string currentLvl;
    private string nextLvl;
    public Image[] dialogueImage;
    public dialoguesClass[] dialogues;

    [System.Serializable]
    public class dialoguesClass
    {
        public string alphabet;
        public Sprite[] dialogue;

    }

    void Start()
    {
        StartCoroutine(PlaySound("LevelIntro"));

        if (nextLvl != null)
        {
            nextLvl = Statics.nextLevel;
            if (nextLvl.Contains("B"))
            {
                currentLvl = "A";
            }
            else if (nextLvl.Contains("P"))
            {
                currentLvl = "B";
            }
            else if (nextLvl.Contains("T2"))
            {
                currentLvl = "P";
            }
            else if (nextLvl.Contains("C3"))
            {
                currentLvl = "T2";
            }
            else if (nextLvl.Contains("J"))
            {
                currentLvl = "C3";
            }
            else if (nextLvl.Contains("Ch"))
            {
                currentLvl = "J";
            }
            else if (nextLvl.Contains("Hj"))
            {
                currentLvl = "Ch";
            }
        }

        InvokeRepeating("randomizeDialogue", 0.1f, 2);
    }

    public void randomizeDialogue()
    {
        foreach(dialoguesClass i in dialogues)
        {
            if (i.alphabet == currentLvl)
            {
                dialogueImage[2].sprite = i.dialogue[0];
                dialogueImage[1].sprite = i.dialogue[UnityEngine.Random.Range(1,i.dialogue.Length)];
                dialogueImage[0].sprite = i.dialogue[UnityEngine.Random.Range(1, i.dialogue.Length)];
            }
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

    public void LoadLevel()
    {
        Loading.Instance.ShowLoading(Statics.nextLevel);
    }
}
