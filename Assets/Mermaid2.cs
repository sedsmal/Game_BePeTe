using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid2 : MonoBehaviour
{
    Animator animator;
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseUpAsButton()
    {
        particleSystem.Play();
        animator.ResetTrigger("stop");
        animator.SetTrigger("start");
    }

    public void ResetAnimationTriggers()
    {
        particleSystem.Stop();
        animator.ResetTrigger("start");
        animator.SetTrigger("stop");
    }
}
