using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;
using UnityEngine.EventSystems;

public class NewArSphere : MonoBehaviour
{
    private Vector3 defaultScale;
    private bool isTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(defaultScale, UnityEngine.Random.Range(0.4f, 0.9f)).OnComplete(() =>
        {
            transform.DOShakePosition(0.7f, 0.03f, 1, 1);

        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began )
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            // Do something with the touched sphere
                            transform.DOScale(Vector3.zero, 0.6f).OnComplete(() => { FloorPlacementController.Instance.WinController(); Destroy(this.gameObject); });
                        }
                    }
                }
            }
        }
    }

    void OnPointerDown(PointerEventData eventData) // Use appropriate input event
    {
        isTouched = true; // Mark the sphere as touched
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

    void OnPointerUp(PointerEventData eventData) // Use appropriate input event
    {
        
        if (UnityEngine.Random.Range(0, 10) < 6)
        {
            if (UnityEngine.Random.Range(0, 10) < 6)
            {
                StartCoroutine(PlaySound("Ali"));
            }
            else
            {
                StartCoroutine(PlaySound("Afarin"));
            }
        }
        SoundManager.Instance.Play("hit");

        transform.DOShakeScale(1.6f, 3, 1, 1).OnComplete(() => {
            FloorPlacementController.Instance.WinController();
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            //Destroy(this.gameObject);
        });



        if (isTouched)
        {
            isTouched = false; // Mark the sphere as untouched

        }
    }
    
    
}
