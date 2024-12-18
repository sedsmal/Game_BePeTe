using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using DG.Tweening;
using CodeStage.AntiCheat.Storage;
using UPersian.Utils;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using BizzyBeeGames;
using UnityEngine.SceneManagement;
using ArabicSupport;

public class MainMenuManager : SingletonComponent<MainMenuManager>
{
    //public GameObject lockpopUp;
    public InputField inputField;
    public GameObject parentAuthen;
    public GameObject StarGiftPopup;
    public Button starGiftPopBtn;
    public Text starGiftPopupText;
    public GameObject timeDetection;
    private Gyroscope gyro;
    public Text starText;

    public bool gyroEnabled = false;

    //public Text coinText; // Reference to the UI Text for displaying coins
    public float updateSpeed = 0.05f; // Speed of the number increment animation
    private int currentCoins = 0; // Current coins displayed
    private int targetCoins = 0; // Coins the player should have after collecting


    private int currentGiftCoins = 0; // Current coins displayed
    private int targetGiftCoins = 0; // Coins the player should have after collecting

    public GameObject starPrefab; // Prefab for the star
    public Transform spawnPosition; // The position where stars will appear
    public Transform targetPosition; // The UI position of the coin counter
    public int starCount = 10; // Number of stars to spawn
    public float popupDuration = 0.5f; // Time for the star to pop up before moving
    public float moveSpeed = 5f; // Speed of the stars moving to the target
    public float spawnRadius = 1f; // Radius for random star spawn positions around the spawn point
    private bool doesGive;
    private void Start()
    {
        gyroEnabled = EnableGyro();

        //if(SceneManager.GetActiveScene().name== "Menu Alphabets 1")
        //{
        //    lockpopUp.SetActive(false);
        //}
        if (ObscuredPrefs.GetString("ParentLockCode") == ""||!ObscuredPrefs.HasKey("ParentLockCode"))
        {
            ObscuredPrefs.SetString("ParentLockCode", "10 + 99 = ؟");
            ObscuredPrefs.SetString("ParentLockCodeAnsware", "109");
        }

        parentAuthen.SetActive(false);
        timeDetection.SetActive(false);

        SoundManager.Instance.Play("music1");
        StartCoroutine(PlaySound("LevelIntro"));
        ServerManagment.Instance.InitializeServer();
        currentCoins = ObscuredPrefs.GetInt("stT");
        starText.text = ArabicFixer.Fix(currentCoins.ToString(),true,true);
        doesGive = false;
        StarGiftPopup.SetActive(false);
        StartCoroutine(ShowGiveStar());
    }

    public void SpawnStars()
    {
        Destroy(starGiftPopBtn);
        for (int i = 0; i < starCount; i++)
            {
                StartCoroutine(AnimateStar((i * 0.1f), i));
            }
    }


    private IEnumerator AnimateStar( float delay,int i)
    {
        // Delay before starting the popup animation
        yield return new WaitForSeconds(delay);
        // Generate a random position around the spawn point
        Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPos = spawnPosition.position + randomOffset;

        // Instantiate the star at the random position
        GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);
        star.transform.SetParent(starText.transform.parent.parent);
        //targetPosition.transform.position-new Vector3(500,500,0)
     
        star.transform.DOLocalMove(targetPosition.transform.localPosition, 2).SetEase(Ease.InOutExpo).OnComplete(()=> { Destroy(star);
            if (i == 6)
            {
                Destroy(spawnPosition.gameObject);
            }
            if (i == 9)
            {
                StarGiftPopup.transform.DOShakeScale(0.3f, 0.1f, 1, 2).OnComplete(() => { StarGiftPopup.SetActive(false); });  } });
        star.transform.DOScale(new Vector3(0.2f,0.2f,0.2f), 1.5f);
        star.transform.DOLocalRotate(
               new Vector3(0f, 0, 360), 3, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        // Start the animation for each star


    }



    private IEnumerator ShowGiveStar()
    {
        yield return new WaitForSeconds(0.3f);
        //ObscuredPrefs.GetInt("st")
        if (2 > 0 )
        {
            StarGiftPopup.SetActive(true);
            StarGiftPopup.transform.DOShakeScale(0.3f, 0.1f, 1, 2);
            starGiftPopupText.text = ArabicFixer.Fix(ObscuredPrefs.GetInt("st").ToString(), true, true);
            
        }
        else
        {
            StarGiftPopup.SetActive(false);
        }
    }

    public void GiveStar()
    {
        Destroy(starGiftPopBtn.gameObject.GetComponent<ButtonPressAnimation>());
            starGiftPopBtn.interactable = false;
            int newStar = ObscuredPrefs.GetInt("st");
            int oldStar = ObscuredPrefs.GetInt("stT");
            
            AddStars(newStar, starText, oldStar, newStar + oldStar);
            AddStars(-newStar, starGiftPopupText, newStar, 0);
            SpawnStars();
            ObscuredPrefs.SetInt("stT", newStar + oldStar);
            ObscuredPrefs.SetInt("st", 0);

        //starText.DOText((newStar+oldStar).ToString(), 2f).OnComplete(()=> { StarGiftPopup.SetActive(false); });}

    }
        public void AddStars(int amount,Text txt, int currentCoins, int targetCoins)
    {
        targetCoins += amount; // Update the target coin count
        StartCoroutine(AnimateCoinCount(txt, currentCoins, targetCoins)); // Start the animation
    }

    private IEnumerator AnimateCoinCount(Text txt,int currentCoins,int targetCoins)
    {
        // Animate until currentCoins matches targetCoins
        while (currentCoins < targetCoins)
        {
            currentCoins++;
            txt.text = currentCoins.ToString(); // Update the text
            yield return new WaitForSeconds(updateSpeed); // Wait for a short moment

        }
        
    }
        private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }
    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
        SoundManager.Instance.Play("btn");
        //PlayAlphabetSound(levelName);
    }

    public void PlayAlphabetSound(string levelName) {

        if (levelName == "B0")
        {
            SoundManager.Instance.Play("B");
        }
        else if (levelName == "A0")
        {
            SoundManager.Instance.Play("A");
        }
        else if (levelName == "Y0")
        {
            SoundManager.Instance.Play("Y");
        }
        else if (levelName == "Z20")
        {
            SoundManager.Instance.Play("Z2");
        }
        else if (levelName == "T20")
        {
            SoundManager.Instance.Play("T2");
        }
        else if (levelName == "T0")
        {
            SoundManager.Instance.Play("T");
        }
        else if (levelName == "P0")
        {
            SoundManager.Instance.Play("P");
        }
        else if (levelName == "C30")
        {
            SoundManager.Instance.Play("C3");
        }
        else if (levelName == "J0")
        {
            SoundManager.Instance.Play("J");
        }
        else if (levelName == "Ch0")
        {
            SoundManager.Instance.Play("Ch");
        }
        else if (levelName == "Hj0")
        {
            SoundManager.Instance.Play("Hj");
        }
        else if (levelName == "Kh0")
        {
            SoundManager.Instance.Play("Kh");
        }
        else if (levelName == "Zl0")
        {
            SoundManager.Instance.Play("Zl");
        }
        else if (levelName == "D0")
        {
            SoundManager.Instance.Play("D");
        }
        else if (levelName == "Zr0")
        {
            SoundManager.Instance.Play("Zr");
        }
        else if (levelName == "R0")
        {
            SoundManager.Instance.Play("R");
        }
        else if (levelName == "Zh0")
        {
            SoundManager.Instance.Play("Zh");
        }
        else if (levelName == "S0")
        {
            SoundManager.Instance.Play("S");
        }
        else if (levelName == "Sh0")
        {
            SoundManager.Instance.Play("Sh");
        }
        else if (levelName == "Sd0")
        {
            SoundManager.Instance.Play("Sd");
        }
        else if (levelName == "Zd0")
        {
            SoundManager.Instance.Play("Zd");
        }
        else if (levelName == "T20")
        {
            SoundManager.Instance.Play("T2");
        }
        else if (levelName == "Z20")
        {
            SoundManager.Instance.Play("Z2");
        }
        else if (levelName == "Eyn0")
        {
            SoundManager.Instance.Play("Eyn");
        }
        else if (levelName == "Ghyn0")
        {
            SoundManager.Instance.Play("Ghyn");
        }
        else if (levelName == "F0")
        {
            SoundManager.Instance.Play("F");
        }
        else if (levelName == "Gh0")
        {
            SoundManager.Instance.Play("Gh");
        }
        else if (levelName == "K0")
        {
            SoundManager.Instance.Play("K");
        }
        else if (levelName == "G0")
        {
            SoundManager.Instance.Play("G");
        }
        else if (levelName == "L0")
        {
            SoundManager.Instance.Play("L");
        }

        else if (levelName == "M0")
        {
            SoundManager.Instance.Play("M");
        }
        else if (levelName == "N0")
        {
            SoundManager.Instance.Play("N");
        }
        else if (levelName == "V0")
        {
            SoundManager.Instance.Play("V");
        }
        else if (levelName == "H0")
        {
            SoundManager.Instance.Play("H");
        }
        else if (levelName == "Practice0")
        {
            SoundManager.Instance.Play("Practice");
        }
        else if (levelName == "Alpha")
        {
            SoundManager.Instance.Play("Alpha");
        }


    }


    public void laught()
    {
        SoundManager.Instance.Play("kidslau");
    }

    public void babytalk()
    {
        SoundManager.Instance.Play("kidstalk");
    }
    public IEnumerator PlaySound(string soundname)
    {
        yield return new WaitForSeconds(0.75f);
        SoundManager.Instance.Play(soundname);
    }

    //public void ShowLockInfo()
    //{
    //    if (!lockpopUp.activeInHierarchy)
    //    {
    //        lockpopUp.SetActive(true);
    //        lockpopUp.GetComponent<CanvasGroup>().alpha = 0f;
    //        lockpopUp.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    //        lockpopUp.transform.DOShakeScale(0.3f, 0.5f, 3, 90);
    //    }
    //}

    //public void HideLockInfo()
    //{
    //    lockpopUp.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
    //    lockpopUp.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { lockpopUp.SetActive(false); });

    //}

    //public void UnlockGame()
    //{
    //    WorldTimeAPI.Instance.CompairTime();
    //    WorldTimeAPI.Instance.activeTest();
    //    //hi();
    //}

    public void Type(string character)
    {
        if (inputField.text.Length < 6) { inputField.text += character; }
    }

    public void Remove()
    {
        if (inputField.text.Length > 0) { inputField.text = inputField.text.Remove(inputField.text.Length - 1); }
    }

    public void Confirm()
    {
        if (inputField.text == ObscuredPrefs.GetString("ParentLockCodeAnsware"))
        {
            SoundManager.Instance.Play("btn");

            //if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Main Menu New")
            //{
            //    Loading.Instance.ShowLoading("Parents Menu");
            //}
            //else if (SceneManager.GetActiveScene().name == "Menu Alphabets 1" || SceneManager.GetActiveScene().name == "Menu Alphabets new")
            //{
            //    Loading.Instance.ShowLoading("Store");

            //}

            Loading.Instance.ShowLoading("Parents Menu");


        }
        else
        {
            SoundManager.Instance.Play("wrong parents");
            inputField.text = "";
        }
    }

    public void ShowParentAuthentication()
    {
        if (!parentAuthen.activeInHierarchy)
        {
            parentAuthen.SetActive(true);
            parentAuthen.GetComponent<CanvasGroup>().alpha = 0f;
            parentAuthen.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            parentAuthen.transform.DOShakeScale(0.3f, 0.5f, 3, 90);
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                StartCoroutine(PlaySound("parents"));
            }
            else if (SceneManager.GetActiveScene().name == "Menu Alphabets 1")
            {
               // SoundManager.Instance.Play("buyPremium");
                StartCoroutine(PlaySound("buyPremium"));

            }

        }
    }

    public void HideParentAuthentication()
    {
        //parentAuthen.transform.DOScale(Vector3.zero, 0.1f).OnComplete(()=> { parentAuthen.SetActive(false); });
        parentAuthen.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        parentAuthen.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { parentAuthen.SetActive(false); });

    }

    public void ShowTimeDetection()
    {
        if (!parentAuthen.activeInHierarchy)
        {
            timeDetection.SetActive(true);
            timeDetection.GetComponent<CanvasGroup>().alpha = 0f;
            timeDetection.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            timeDetection.transform.DOShakeScale(0.3f, 0.5f, 3, 90);
        }
    }

    public void HidetimeDetection()
    {
        //parentAuthen.transform.DOScale(Vector3.zero, 0.1f).OnComplete(()=> { parentAuthen.SetActive(false); });
        timeDetection.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        timeDetection.transform.DOShakeScale(0.2f, 0.5f, 3, 90).OnComplete(() => { timeDetection.SetActive(false); });

    }

}
