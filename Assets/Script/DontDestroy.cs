using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public delegate void Change();
    public static event Change TimeChanged;
    public Sprite[] sprites;
    [HideInInspector] public Image image;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void Start()
    {
        image = GetComponent<Image>();

        if (sprites.Length != 0)
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }


        // wait 1.5 seconds before change to Scene2
        //StartCoroutine(TimeChangedScene());
    }

    //IEnumerator TimeChangedScene()
    //{
    //    print(Time.time + " seconds");
    //    yield return new WaitForSeconds(1.5f);
    //    print(Time.time + " seconds");

    //    // call the event
    //    TimeChanged();
    //}

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;
        image.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        //Debug.Log("Scenes: " + currentName + ", " + next.name);
    }

    //void OnEnable()
    //{
    //    Debug.Log("OnEnable");
    //    ScriptExample1.TimeChanged += ChangeScene;
    //}

    //void ChangeScene()
    //{
    //    Debug.Log("Changing to Scene2");
    //    SceneManager.LoadScene("Scene2");

    //    Scene scene = SceneManager.GetSceneByName("Scene2");
    //    SceneManager.SetActiveScene(scene);
    //}

    //void OnDisable()
    //{
    //    ScriptExample1.TimeChanged -= ChangeScene;
    //    Debug.Log("OnDisable happened for Scene1");
    //}

}
