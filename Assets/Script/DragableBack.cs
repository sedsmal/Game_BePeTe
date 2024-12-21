using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BizzyBeeGames;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DragableBack : MonoBehaviour, IDragHandler, IEndDragHandler,IBeginDragHandler,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
{
    private Vector2 prevPos;
    private List<RaycastResult> raycastResults;
    public string backLevel;
    public Image[] backobjects;
    public GameObject droppedFieldObj;
    public Sprite[] icons;
    public Image buttonIcon;
    [HideInInspector] public bool ishit=false;

    private void Start()
    {
        prevPos = transform.position;
        
        foreach (Image i in backobjects)
        {
            i.color=new Color(1,1,1,0);
        }
        droppedFieldObj.SetActive(false);
    }
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            Statics.ispause = true;
        }
        else if(Time.timeScale == 1)
        {
            Statics.ispause = false;

        }
        //float x = transform.position.x;

        //if (x > -82)
        //{
        //    transform.position = new Vector3(-83, transform.position.y, transform.position.z);
        //}
        //else if (x < -1795)
        //{
        //    transform.position = new Vector3(-1794, transform.position.y, transform.position.z);
        //}
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!ishit)
        {
            raycastResults = new List<RaycastResult>();
            
        }

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (!ishit) {
            Vector3 pos;
        if (SceneManager.GetActiveScene().name.Contains("7")|| SceneManager.GetActiveScene().name.Contains("0")||
            SceneManager.GetActiveScene().name== "Menu Alphabets 1"|| SceneManager.GetActiveScene().name == "Practice Menu"||
            SceneManager.GetActiveScene().name == "Parents Menu" || SceneManager.GetActiveScene().name.Contains("8"))
        {
             pos =
            eventData.position;
        
            pos.z = 0;
        }
        else {
            pos = Camera.main.ScreenToWorldPoint(
          eventData.position
             );
            pos.z = 0;
        }

        

        transform.position =new Vector3( pos.x,transform.position.y,transform.position.z);


        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
           // Debug.Log(hit.gameObject.name);
            // もし DroppableField の上なら、の処理
            if (hit.gameObject.name == "DroppableField")
            {
                    ishit = true;
                    transform.position = new Vector3(hit.gameObject.transform.position.x, transform.position.y, transform.position.z);
                if(backLevel==""|| backLevel == null)
                {
                    if (SceneManager.GetActiveScene().name == "Menu Alphabets 1" || SceneManager.GetActiveScene().name == "Practice Menu")
                    {
                        LoadLevel("Main Menu");
                    }
                    else
                    {
                        LoadLevel("Menu Alphabets 1");
                    }

                }
                else { LoadLevel(backLevel); }

                    
                
                
                
            }
        }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Time.timeScale = 1;
        transform.position = new Vector3(prevPos.x, transform.position.y, transform.position.z);

        foreach (Image i in backobjects)
        {
            i.DOFade(0, 0.4f).OnComplete(()=> { buttonIcon.sprite = icons[0]; });
        }



        //droppedFieldObj.SetActive(false);

        //var raycastResults = new List<RaycastResult>();
        //EventSystem.current.RaycastAll(eventData, raycastResults);

        //foreach (var hit in raycastResults)
        //{
        //    Debug.Log(hit.gameObject.name);
        //    // もし DroppableField の上なら、の処理
        //    if (hit.gameObject.name == "DroppableField")
        //    {
        //        transform.position = new Vector3(hit.gameObject.transform.position.x, transform.position.y, transform.position.z);
        //    }
        //    else
        //    {
        //  transform.position = new Vector3(prevPos.x, transform.position.y, transform.position.z);
        //    }
        //}
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    Debug.Log("drop");
    //    var raycastResults = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventData, raycastResults);

    //    foreach (var hit in raycastResults)
    //    {
    //        Debug.Log(hit.gameObject.name);
    //        // もし DroppableField の上なら、の処理
    //        if (hit.gameObject.name == "DroppableField")
    //        {
    //            transform.position = new Vector3(hit.gameObject.transform.position.x, transform.position.y, transform.position.z); 
    //        }
    //    }
    //}


    public void LoadLevel(string levelName)
    {
        Loading.Instance.ShowLoading(levelName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //if (Time.timeScale == 1)
        //{
        buttonIcon.sprite = icons[1];
        droppedFieldObj.SetActive(true);

        foreach (Image i in backobjects)
        {

            i.DOFade(1, 0.2f).OnComplete(() => {  });

        }

        //}else if(Time.timeScale == 0)
        //{
        //    buttonIcon.sprite = icons[0];
        //    Time.timeScale = 1;

        //    foreach (Image i in backobjects)
        //    {
        //        i.DOFade(0, 0.4f);
        //    }
        //    droppedFieldObj.SetActive(false);
        //}

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
        {
            buttonIcon.sprite = icons[1];
            droppedFieldObj.SetActive(true);

            foreach (Image i in backobjects)
            {
                i.DOFade(1, 0.2f).OnComplete(() => {
                   // StartCoroutine(Pause());

                    Time.timeScale = 0;
                    SoundManager.Instance.SetSoundTypeOnOff(SoundManager.SoundType.Music, false);
                    SoundManager.Instance.SetSoundTypeOnOff(SoundManager.SoundType.SoundEffect, false);
                });
            }
        }
        else if (Time.timeScale == 0)
        {
            buttonIcon.sprite = icons[0];
            Time.timeScale = 1;
            SoundManager.Instance.SetSoundTypeOnOff(SoundManager.SoundType.Music, true);
            SoundManager.Instance.SetSoundTypeOnOff(SoundManager.SoundType.SoundEffect, true);

            foreach (Image i in backobjects)
            {
                i.DOFade(0, 0.4f);
            }
            droppedFieldObj.SetActive(false);


            //initialize camera view after unpause in VR sceans
            if (GyroCameraController.Instance != null)
            {
                GyroCameraController.Instance.InitializeGyro();
            }
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (Time.timeScale == 1)
        //{
        //    buttonIcon.sprite = icons[1];
        //    droppedFieldObj.SetActive(true);

        //    foreach (Image i in backobjects)
        //    {

        //        i.DOFade(1, 0.2f).OnComplete(() => { StartCoroutine(Pause()); });

        //    }

        //}
        //else if (Time.timeScale == 0)
        //{
        //    buttonIcon.sprite = icons[0];
        //    Time.timeScale = 1;

        //    foreach (Image i in backobjects)
        //    {
        //        i.DOFade(0, 0.4f);
        //    }
        //    droppedFieldObj.SetActive(false);
        //}
    }
}
