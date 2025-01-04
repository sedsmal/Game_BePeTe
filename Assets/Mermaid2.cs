using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mermaid2 : MonoBehaviour
{
    Animator animator;
    public ParticleSystem particleSystem;
    Vector3 defScale;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        defScale = transform.localScale;
    }

    private void OnMouseUpAsButton()
    {
        transform.DOShakeScale(1.2f, 0.2f, 1, 0).OnComplete(() => { transform.localScale = defScale; });
        particleSystem.Play();
        animator.ResetTrigger("stop");
        animator.SetTrigger("start");
    }

    public void ResetAnimationTriggers()
    {
        particleSystem.Stop(false);
        animator.ResetTrigger("start");
        animator.SetTrigger("stop");
    }
}
