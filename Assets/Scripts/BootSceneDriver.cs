using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneDriver : MonoBehaviour
{
    private const string HUDscene = "Assets/Scenes/HUD.unity";
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(LoadAdditionalScenes());
    }

    private IEnumerator LoadAdditionalScenes()
    {
        AsyncOperation loadHUD = SceneManager.LoadSceneAsync(HUDscene, LoadSceneMode.Additive);

        while (!loadHUD.isDone)
        {
            yield return null; 
        }

        Destroy(gameObject);
    }
    
}
