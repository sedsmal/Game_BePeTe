using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

public class BallonesPracticeManager : SingletonComponent<BallonesPracticeManager>
{
    
    public GameObject[] NormalBallones;
    public GameObject[] AlphabetBallones;
    public Sprite[] AlphabetBallonesSprite;
    public Sprite[] AlphabetSprites;
    public Sprite[] whiteAlphabetSprites;
    public Sprite[] PronounciationSprites;
    public Image[] goals;
    float timePassed = 0f;
    int count;
    public GameObject ballonesParentObject;
    public GameObject winPopup;
    public GameObject HelpFinger;
    public ParticleSystem celebrationParticle;
    public GameObject StartBalloneHelpSr;
    public Image winMainAlphabetImg,winAlphabetPronImg;

    int currentAlphaIndex;
    GameObject currentAlphabet;
    List<GameObject> otherLvlAlphabets;
    // Start is called before the first frame update

    void Start()
    {

        otherLvlAlphabets = new List<GameObject>();

        count = 0;

        currentAlphaIndex = UnityEngine.Random.Range(0, AlphabetBallones.Length);
        currentAlphabet = AlphabetBallones[currentAlphaIndex];
        otherLvlAlphabets.Add(AlphabetBallones[currentAlphaIndex]);

        GameObject newBallone = Instantiate(currentAlphabet, StartBalloneHelpSr.transform.position, Quaternion.identity);
        newBallone.transform.SetParent(ballonesParentObject.transform);
        Destroy(StartBalloneHelpSr);



        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, AlphabetBallones.Length);

            if (rand != currentAlphaIndex)
            {
                otherLvlAlphabets.Add( AlphabetBallones[rand]);
            }
        }

        //change alphabets
        foreach (Image a in goals)
        {
            a.sprite = AlphabetSprites[currentAlphaIndex];
        }

        foreach (Image a in goals)
        {
            a.color = new Color(0, 0, 0, 0.3f);
        }
        StartCoroutine(PlaySound("LevelIntro"));
        StartCoroutine(StartHelp());
        StartCoroutine(HideHelpWithDelay());
        winPopup.SetActive(false);
        SoundManager.Instance.Play("music1");

        Timer.Instance.beginTimer();

    }
    public void PlayAlphabetSound(string index)
    {

        if (index == "A")
        {
            SoundManager.Instance.Play("A");
        }
        else if (index == "B")
        {
            SoundManager.Instance.Play("B");
        }
        else if (index == "P")
        {
            SoundManager.Instance.Play("P");
        }
        else if (index == "T")
        {
            SoundManager.Instance.Play("T");
        }
        else if (index == "C3")
        {
            SoundManager.Instance.Play("C3");
        }
        else if (index == "J")
        {
            SoundManager.Instance.Play("J");
        }
        else if (index == "Ch")
        {
            SoundManager.Instance.Play("Ch");
        }
        else if (index == "Hj")
        {
            SoundManager.Instance.Play("Hj");
        }
        else if (index == "Kh")
        {
            SoundManager.Instance.Play("Kh");
        }
        else if (index == "D")
        {
            SoundManager.Instance.Play("D");
        }
        else if (index == "Zl")
        {
            SoundManager.Instance.Play("Zl");
        }
        else if (index == "R")
        {
            SoundManager.Instance.Play("R");
        }
        else if (index == "Zr")
        {
            SoundManager.Instance.Play("Zr");
        }
        else if (index == "Zh")
        {
            SoundManager.Instance.Play("Zh");
        }
        else if (index == "S")
        {
            SoundManager.Instance.Play("S");
        }
        else if (index == "Sh")
        {
            SoundManager.Instance.Play("Sh");
        }

        else if (index == "Sd")
        {
            SoundManager.Instance.Play("Sd");
        }
        else if (index == "Zd")
        {
            SoundManager.Instance.Play("Zd");
        }
        else if (index == "T2")
        {
            SoundManager.Instance.Play("T2");
        }
        else if (index == "Z2")
        {
            SoundManager.Instance.Play("Z2");
        }
        else if (index == "Eyn")
        {
            SoundManager.Instance.Play("Eyn");
        }
        else if (index == "Ghyn")
        {
            SoundManager.Instance.Play("Ghyn");
        }
        else if (index == "F")
        {
            SoundManager.Instance.Play("F");
        }
        else if (index == "Gh")
        {
            SoundManager.Instance.Play("Gh");
        }
        else if (index == "K")
        {
            SoundManager.Instance.Play("K");
        }
        else if (index == "G")
        {
            SoundManager.Instance.Play("G");
        }
        else if (index == "L")
        {
            SoundManager.Instance.Play("L");
        }
        else if (index == "M")
        {
            SoundManager.Instance.Play("M");
        }
        else if (index == "N")
        {
            SoundManager.Instance.Play("N");
        }

        else if (index == "V")
        {
            SoundManager.Instance.Play("V");
        }
        else if (index == "H")
        {
            SoundManager.Instance.Play("H");
        }
        else if (index == "Y")
        {
            SoundManager.Instance.Play("Y");
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
    IEnumerator PlayAlphabetSoundIe()
    {
        yield return new WaitForSeconds(1.2f);
        PlayAlphabetSound(currentAlphabet.name.PadLeft(8));
    }
    private void Update()
    {
        if (count < goals.Length)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 2f)
            {
                CreateBallones();
                timePassed = 0f;
            }
        }

    }

    private void CreateBallones()
    {
        int count = Random.Range(1, 2);
        for (int i = 0; i < count; i++)
        {
            GameObject ballone = otherLvlAlphabets[Random.Range(0, otherLvlAlphabets.Count)];

            

            Vector3 position = new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y, transform.position.z);
            GameObject newBallone = Instantiate(ballone, position, Quaternion.identity);
            newBallone.transform.SetParent(ballonesParentObject.transform);
            if (newBallone.transform.childCount > 1)
                Destroy(newBallone.transform.GetChild(newBallone.transform.childCount - 1).gameObject);
        }

        int rand= Random.Range(0, 20);
        if (rand < 5)
        {
            GameObject ballone = NormalBallones[Random.Range(0, NormalBallones.Length)];

            if (ballone.transform.childCount > 2)
                Destroy(ballone.transform.GetChild(ballone.transform.childCount - 1));

            Vector3 position = new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y, transform.position.z);
            GameObject newBallone = Instantiate(ballone, position, Quaternion.identity);
            newBallone.transform.SetParent(ballonesParentObject.transform);
        }
    }

    public void HitBallone()
    {
        count++;
        goals[count - 1].color = new Color(1, 1, 1, 1);
        goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90);
        
        if (UnityEngine.Random.Range(0, 5) < 2)
        {
            PlayAlphabetSound(currentAlphabet.name.PadLeft(8));
        }
        else
        {
            SoundManager.Instance.Play("like");
        }

        SoundManager.Instance.Play("win");

        ShowWin();

    }

    string RemoveEnd(string str, int len)
    {
        if (str.Length < len)
        {
            return string.Empty;
        }

        return str.Remove(str.Length - len);
    }

    public void HitBallone(string name)
    {

        string n = name.Substring(8);
        if (n.Contains("Clone"))
            n = RemoveEnd(n, 7);
        PlayAlphabetSound(n);

        if (name.Contains( AlphabetBallones[currentAlphaIndex].name))
        {
            count++;
            goals[count - 1].color = new Color(1, 1, 1, 1);
            goals[count - 1].transform.DOShakeScale(0.4f, 0.1f, 5, 90);

            


            SoundManager.Instance.Play("win");

            StartCoroutine(Win());
        }
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
            ObscuredPrefs.SetInt("st", star + 7);

            celebrationParticle.Play();
            winPopup.SetActive(true);
            winAlphabetPronImg.sprite = PronounciationSprites[currentAlphaIndex];
            winMainAlphabetImg.sprite = whiteAlphabetSprites[currentAlphaIndex];
            CalculateTime();
            winPopup.transform.DOShakeScale(0.6f, 0.1f, 5, 90);
            StartCoroutine(PlayAlphabetSoundIe());

            for (int i = 0; i < ballonesParentObject.transform.childCount; i++)
            {
                Destroy(ballonesParentObject.transform.GetChild(i).gameObject);
            }
        }

    }
    public void LoadLevel(string index)
    {
        Loading.Instance.ShowLoading(index);
    }

    public void HideHelp()
    {



    }

    public void ShowHelp()
    {
        StartCoroutine(StartHelp());
    }

    IEnumerator StartHelp()
    {
        yield return new WaitForSeconds(0.5f);

        if(HelpFinger!=null)
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
        int count = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name + "Count");
        int lastTime = ObscuredPrefs.GetInt(SceneManager.GetActiveScene().name);
        ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name, time + lastTime);
        ObscuredPrefs.SetInt(SceneManager.GetActiveScene().name + "Count", count + 1);

    }
}
