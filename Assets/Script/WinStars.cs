using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;

public class WinStars : MonoBehaviour
{
    public GameObject[] starParticle;
    private void Start()
    {
        for(int i = 0; i < starParticle.Length; i++)
        {
            starParticle[i].SetActive(false);
        }
    }
    public void PlayStarsSound()
    {
        SoundManager.Instance.Play("Star");
    }

    public void Particle1()
    {
        starParticle[0].SetActive(true);
    }
    public void Particle2()
    {
        starParticle[1].SetActive(true);
    }
    public void Particle3()
    {
        starParticle[2].SetActive(true);
    }
    public void Particle4()
    {
        starParticle[3].SetActive(true);
    }
    public void Particle5()
    {
        starParticle[4].SetActive(true);
    }
    public void Particle6()
    {
        starParticle[5].SetActive(true);
    }
    public void Particle7()
    {
        starParticle[6].SetActive(true);
    }

}
