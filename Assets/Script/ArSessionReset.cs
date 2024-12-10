
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class ArSessionReset : MonoBehaviour
{

    private void OnDestroy()
    {
        GetComponent<ARSession>().Reset();
    }

    void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnSceneUnloaded(Scene current)
    {
        if (current == SceneManager.GetActiveScene())
        {
            LoaderUtility.Deinitialize();
            LoaderUtility.Initialize();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
