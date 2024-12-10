using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] balloons;

    // Start is called before the first frame update
    public void StartBallones()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(4);

        Vector3 StartPos = FloorPlacementController.Instance.hitPose.position + new Vector3(UnityEngine.Random.Range(0.1f, 2), UnityEngine.Random.Range(0.1f, 2), 0);

        //for(int i = 0; i < 3; i++)
        //{
            // Instantiate(balloons[i], spawnPoints[i].position, Quaternion.identity);
            Instantiate(balloons[UnityEngine.Random.Range(0,balloons.Length)], StartPos, Quaternion.identity);
       // }

        StartCoroutine(StartSpawning());
    }
}
