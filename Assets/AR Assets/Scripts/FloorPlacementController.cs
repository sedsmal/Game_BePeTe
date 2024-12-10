using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Management;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.Storage;

[RequireComponent(typeof(ARRaycastManager))]
public class FloorPlacementController : SingletonComponent<FloorPlacementController>
{
    public GameManager gameManager;
    [HideInInspector]
    public bool hasStarted;



    public Text selectPlane;
    public GameObject startButton;
    public SoundManagerScript soundManagerScript;
    private List<GameObject> spawnNew = new List<GameObject>();
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    [SerializeField]
    public GameObject alphabet;
    public GameObject land;
    public GameObject balls;
    static public ARPlane savePlane;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [HideInInspector] public Pose hitPose;
    private bool surfaceFound = false, playingMusic = false;
    private GameObject[] balloons;
    ARAnchor modelsAnchor;
    public ARAnchor anchor;
    private GameObject activeAlphabet;
    public int numberOfTargets = 5;
    public GameObject hintCamera;
    public GameObject replayButton;

    public Slider PlaneScanSlider;
    public GameObject locationPrefab;
    private GameObject locationGameobject;
    private bool islocated;
    private int numberOfDestory=0;
    public int totalNumberOfDestrory;
    public GameObject winPopUp;
    public ParticleSystem celebrationParticle;
    [HideInInspector]public ARAnchorManager aRAnchorManager;
    private void OnEnable()
    {
        //StartSession();
    }
    void Awake()
    {
        //StartSession();
        savePlane = null;
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRAnchorManager = GetComponent<ARAnchorManager>();
        

        //aRPlaneManager.enabled = true;
        //selectPlane.enabled = true;
        //surfaceFound = false;
    }

    private void Start()
    {
        hintCamera.SetActive(true);
        replayButton.SetActive(true);
        PlaneScanSlider.value = 0;
        foreach (var plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
            Destroy(plane.gameObject);
        }

        StartCoroutine(PlaySound("ArLevelIntro"));
        
        aRPlaneManager.enabled = true;
        selectPlane.enabled = true;
        surfaceFound = false;
        hits = new List<ARRaycastHit>();
        winPopUp.SetActive(false);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {

        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;

        return false;
    }
    void Update()
    {
        //if (aRPlaneManager.trackables.count > 1)
        //{
        //    DestroyPlane();
        //}

        if (selectPlane.enabled && aRPlaneManager.trackables.count > 0&&!surfaceFound)
        {
            
            StartGameAutomaticaly2();
        }

        if (surfaceFound)
        {
            if (!hasStarted)
            {
                SetLoca();

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        StartButton();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space)) { StartButton(); }


            }

        }

        //foreach (var plane in aRPlaneManager.trackables)
        //{
        //    if (plane.gameObject.activeSelf)
        //    {
        //        float distance = Vector3.Distance(Camera.main.transform.position, plane.transform.position);
        //        Debug.Log("Distance to Plane: " + distance.ToString("F2") + " meters");
        //    }
        //}

        //if (!TryGetTouchPosition(out Vector2 touchPosition))
        //    return;

        //if (!surfaceFound && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        //    {
        //        savePlane = aRPlaneManager.GetPlane(hits[0].trackableId);
        //        hitPose = hits[0].pose;
        //        modelsAnchor = savePlane.gameObject.AddComponent<ARAnchor>();
        //        foreach (var plane in aRPlaneManager.trackables)
        //        {
        //            if (hits[0].trackableId != plane.trackableId)
        //                plane.gameObject.SetActive(false);
        //            //Destroy(plane.gameObject);
        //        }
        //        aRPlaneManager.enabled = false;

        //        selectPlane.enabled = false;
        //        //startButton.SetActive(true);
        //        surfaceFound = true;
        //        StartButton();
        //    }
        //}
    }
    public void StartGameAutomaticaly()

    {
        if (!surfaceFound)
        {
            if (aRRaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), hits, TrackableType.PlaneWithinPolygon))
            {
                savePlane = aRPlaneManager.GetPlane(hits[0].trackableId);

                if (savePlane.extents.x * savePlane.extents.y > 0.31f)
                {
                    hintCamera.SetActive(false);
                    replayButton.SetActive(false);
                    hitPose = hits[0].pose;

                    foreach (var plane in aRPlaneManager.trackables)
                    {
                        if (hits[0].trackableId != plane.trackableId)
                            plane.gameObject.SetActive(false);

                    }
                    aRPlaneManager.enabled = false;
                    modelsAnchor = savePlane.gameObject.AddComponent<ARAnchor>();
                    selectPlane.enabled = false;
                    StartButton();
                    surfaceFound = true;
                }

            }
        }
    }

    Pose calculatePose()
    {
        return new Pose(savePlane.transform.position,savePlane.transform.rotation);
    }
    public void StartGameAutomaticaly2()

    {
        if (!surfaceFound)
        {
           
            foreach (var plane in aRPlaneManager.trackables)
            {
                float distance = Vector3.Distance(Camera.main.transform.position, plane.transform.position);
                PlaneScanSlider.DOValue( plane.extents.x * plane.extents.y / 0.4f * distance,0.2f);
                if (plane.extents.x * plane.extents.y > 0.4f*distance)
                {
                    savePlane = plane;
                    savePlane.transform.position = savePlane.transform.position + new Vector3(0,-0.13f,0);
                    hitPose = calculatePose();
                    foreach (var planee in aRPlaneManager.trackables)
                    {
                        if (savePlane != planee)
                        {
                            planee.gameObject.SetActive(false);
                        }
                        
                    }
                    aRPlaneManager.enabled = false;
                    modelsAnchor = savePlane.gameObject.AddComponent<ARAnchor>();
                    anchor = aRAnchorManager.AttachAnchor(savePlane, hitPose);
                    selectPlane.enabled = false;
                    surfaceFound = true;
                    hintCamera.SetActive(false);
                    //StartButton();
                    SetLoca();
                    SoundManager.Instance.Play("ChoosePlace");
                    break;
                    //replayButton.SetActive(false);
                }


            } 
        }
    }
    public void DestroyPlane()
    {
        int i = 0;
        foreach (var plane in aRPlaneManager.trackables)
        {

            if (i != 0)
            {
                plane.gameObject.SetActive(false);
            }


            i++;
        }
    }

    public void SetLoca()
    {
        if (surfaceFound)
        {
            if (locationGameobject == null)
            {
                locationGameobject = Instantiate(locationPrefab);
                locationGameobject.transform.rotation = hitPose.rotation;
                locationGameobject.transform.SetParent(anchor.transform);
            }
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (aRRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                foreach (var hit in hits)
                {
                    if (hit.trackableId == savePlane.trackableId)
                    {
                        Vector3 hitPosition = hit.pose.position;

                        locationGameobject.transform.position = hitPosition+new Vector3(0f,-0.015f,0f);

                        break;
                    }
                }
            }
        }
    }
    public void StartButton()
    {
        hasStarted = true;
        //savePlane.transform.localScale = new Vector3(savePlane.transform.localScale.x, savePlane.transform.localScale.y, savePlane.transform.localScale.z);
        
        
        GameObject plane = GameObject.FindGameObjectWithTag("Plane");
       // plane.transform.localScale = new Vector3(savePlane.transform.localScale.x * 3, savePlane.transform.localScale.y, savePlane.transform.localScale.z * 3);
        plane.GetComponent<MeshRenderer>().enabled = false;
        plane.GetComponent<LineRenderer>().enabled = false;
        plane.GetComponent<ARPlaneMeshVisualizer>().enabled = false;

        if (!playingMusic)
        {
            soundManagerScript.PlaySound("startGameMusic");
            playingMusic = true;
        }

        //foreach (var target in spawnNew)
        //{
        //    Destroy(target);
        //}
        // GameObject[] objectArray = objectToSpawn.ToArray();

        
        GameObject landd=Instantiate(land, new Vector3(locationGameobject.transform.position.x, locationGameobject.transform.position.y, locationGameobject.transform.position.z), hitPose.rotation);
        //landd.gameObject.AddComponent<ARAnchor>();
        landd.transform.SetParent(anchor.transform);
        
        //GameObject balll = Instantiate(balls, new Vector3(locationGameobject.transform.position.x, locationGameobject.transform.position.y, locationGameobject.transform.position.z), hitPose.rotation);

        //balll.transform.SetParent(landd.transform);
        // spawnNew.Add(Instantiate(objectArray[Random.Range(0, objectArray.Length)], new Vector3(savePlane.transform.position.x, savePlane.transform.position.y + 0.05f, savePlane.transform.position.z), hitPose.rotation));
        activeAlphabet = Instantiate(alphabet, new Vector3(locationGameobject.transform.position.x, locationGameobject.transform.position.y+ 0.2955f, locationGameobject.transform.position.z), hitPose.rotation);
        //activeAlphabet.gameObject.AddComponent<ARAnchor>();
        activeAlphabet.transform.SetParent(landd.transform.GetChild(0));
        activeAlphabet.transform.localScale = activeAlphabet.transform.localScale * 4;
        startButton.SetActive(false);
        StartCoroutine(PlaySound("StartPlay"));
        Destroy(locationGameobject);

        //land.GetComponent<ArBasment>().StartGame();
        //StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(2);

        Vector3 StartPos = FloorPlacementController.Instance.hitPose.position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f), 0);

        //for(int i = 0; i < 3; i++)
        //{
        // Instantiate(balloons[i], spawnPoints[i].position, Quaternion.identity);
        Instantiate(balloons[UnityEngine.Random.Range(0, balloons.Length)], StartPos, Quaternion.identity);
        // }

        StartCoroutine(StartSpawning());
    }

    public void MakeAlphabetBigger()
    {
        if (activeAlphabet.transform.localScale.z < 10)
            activeAlphabet.transform.DOScale(activeAlphabet.transform.localScale * 1.1f, 1f);
    }
    public void RotateAlphabet()
    {
        activeAlphabet.transform.DOLocalRotate(activeAlphabet.transform.localRotation.eulerAngles+new Vector3(0f,20f,0f), 1f);
    }
    public void RotateLeftAlphabet()
    {
        activeAlphabet.transform.DOLocalRotate(activeAlphabet.transform.localRotation.eulerAngles + new Vector3(0f, -20f, 0f), 1f);
    }
    internal void MakeAlphabetSmaller()
    {

        if (activeAlphabet.transform.localScale.z > 4)
            activeAlphabet.transform.DOScale(activeAlphabet.transform.localScale * 0.9f, 1f);
    }
    public void StopAlphabetAnimation()
    {
        activeAlphabet.transform.DOKill();
        //DOTween.KillAll();
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
    private void OnDestroy()
    {
        EndSession();
    }
    //private void OnDisable()
    //{
    //    EndSession();
    //}

    public void WinController()
    {
        numberOfDestory++;
        if (numberOfDestory >= totalNumberOfDestrory)
        {
            winPopUp.SetActive(true);

            int star = ObscuredPrefs.GetInt("st");
            ObscuredPrefs.SetInt("st", star + 7);

            celebrationParticle.Play();
            PlaySoundImmediately("like");
        }
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

    IEnumerator ShowWin()
    {
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(PlaySound("like"));
        winPopUp.SetActive(true);
        celebrationParticle.Play();
        winPopUp.transform.DOShakeScale(0.6f, 0.1f, 5, 90);

        CalculateTime();
    }


    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
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






