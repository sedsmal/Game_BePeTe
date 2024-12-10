using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManagment : MonoBehaviour
{
    //public GameObject loadnigScreen;
    public ParticleSystem[] alphabetsParticle;

    // Start is called before the first frame update
    void Awake()
    {
        //loadnigScreen.SetActive(false);
    }

    private void Start()
    {

        Initialize(0);
    }

    // Update is called once per frame
    public void ActiveSceneChanging()
    {
        //loadnigScreen.SetActive(true);
    }



    public void Initialize(int i)
    {
            StartCoroutine(StartParticle(alphabetsParticle[i],i));
    }

    IEnumerator StartParticle(ParticleSystem p,int i)
    {
        yield return new WaitForSeconds(0.05f);
        
        p.Play();

        if (i + 1 < alphabetsParticle.Length)
        {
            Initialize(i + 1);
        }
        else
        {
            //ChangeScene();
            StartCoroutine(ChangeSceneIe());
        }   
    }

    IEnumerator ChangeSceneIe()
    {
        yield return new WaitForSeconds(2.3f);
        Loading.Instance.ShowLoading(Statics.nextLevel);
    }
    public void ChangeScene()
    {
        Loading.Instance.ShowLoading(Statics.nextLevel);
    }


}
