using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BizzyBeeGames;

public class spaceShip : MonoBehaviour
{
    private float? lastMousePoint = null;
    Vector3 lastMouse;

    private void OnMouseDown()
    {
        transform.DOShakeScale(0.5f, 0.5f, 10, 90);
    }
    //Update is called once per frame
    void Update()
    {
        if (!Statics.ispause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (SpaceManager.Instance.IsHelpActive()) { SpaceManager.Instance.HideHelp(); }
                lastMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                lastMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SoundManager.Instance.Play("StartEngine");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                lastMousePoint = null;
            }
            if (lastMousePoint != null)
            {
                float difference = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - lastMousePoint.Value;
                float finalpos = transform.position.y + (difference / 1);
                if (finalpos > 5f)
                {
                    finalpos = 5f;
                }
                else if (finalpos < -5f)
                {
                    finalpos = -5f;
                }
                transform.position = new Vector3(transform.position.x, finalpos, transform.position.z);
                lastMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;


                if (difference > 0.01)
                {
                    var targetRotation = new Vector3(0, 0, 30);
                    var lookTo = Quaternion.Euler(targetRotation);

                    //rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * runSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookTo, 100f * Time.deltaTime);
                }
                else if (difference < -0.01)
                {
                    var targetRotation = new Vector3(0, 0, -30);
                    var lookTo = Quaternion.Euler(targetRotation);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookTo, 100f * Time.deltaTime);

                }



            }
        }
  
    }
}
