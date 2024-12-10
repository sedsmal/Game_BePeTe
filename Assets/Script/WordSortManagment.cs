using UnityEngine;
using BizzyBeeGames;
using System.Collections;

public class WordSortManagment : SingletonComponent<WordSortManagment>
{
    public GameObject[] alphabet;
    private int count=0;
    
    public void ReStartLevel()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Restart");
        
        StartCoroutine(PlaySound("Intro5"));

    }
    public void PlayAnimation()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("play");
    }
    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(0.6f);
        SoundManager.Instance.Play(soundname);
    }
    public bool CheckWin()
    {
        if (alphabet.Length == 3)
        {
            if(alphabet[0].transform.position.x>alphabet[1].transform.position.x &&
                alphabet[1].transform.position.x > alphabet[2].transform.position.x)
            {
                SoundManager.Instance.Play("win");
                
                count++;
                if (count == 2)
                {
                    ThreealphaWordManager.Instance.ShowWInPopup();
                    SoundManager.Instance.Play("Ali");
                }
                else
                {
                    ThreealphaWordManager.Instance.PlayParticel();
                    ReStartLevel();
                }
                return true;
            }
        }else if (alphabet.Length == 4)
        {
            if (alphabet[0].transform.position.x > alphabet[1].transform.position.x &&
                alphabet[1].transform.position.x > alphabet[2].transform.position.x&&
                alphabet[2].transform.position.x > alphabet[3].transform.position.x)
            {
                count++;
                if (count == 2)
                {
                    ThreealphaWordManager.Instance.ShowWInPopup();
                }
                else
                {
                    ThreealphaWordManager.Instance.PlayParticel();
                    ReStartLevel();
                }
                SoundManager.Instance.Play("win");
                return true;
            }
        }
        else if (alphabet.Length == 2)
        {
            if (alphabet[0].transform.position.x > alphabet[1].transform.position.x)
            {
                count++;
                if (count == 2)
                {
                    ThreealphaWordManager.Instance.ShowWInPopup();
                }
                else
                {
                    ThreealphaWordManager.Instance.PlayParticel();
                    ReStartLevel();
                }
                SoundManager.Instance.Play("win");
                return true;
            }
        }
        else if (alphabet.Length == 5)
        {
            if (alphabet[0].transform.position.x > alphabet[1].transform.position.x &&
                alphabet[1].transform.position.x > alphabet[2].transform.position.x &&
                alphabet[2].transform.position.x > alphabet[3].transform.position.x &&
                alphabet[3].transform.position.x > alphabet[4].transform.position.x)
            {
                count++;
                if (count == 2)
                {
                    ThreealphaWordManager.Instance.ShowWInPopup();
                }
                else
                {
                    ThreealphaWordManager.Instance.PlayParticel();
                    ReStartLevel();
                }

                SoundManager.Instance.Play("win");
                return true;
            }
        }
        return false;
    }

}
