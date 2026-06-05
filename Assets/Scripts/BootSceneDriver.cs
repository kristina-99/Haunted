using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneDriver : MonoBehaviour
{
    private const string HUDscene = "Assets/Scenes/HUD.unity";
    private const string ArcadeMap = "Assets/Scenes/Arcade_Map.unity";
    
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
        yield return StartCoroutine(SingleSceneLoad(loadHUD)); 
        AsyncOperation loadArcadeMap = SceneManager.LoadSceneAsync(ArcadeMap, LoadSceneMode.Additive);
        yield return StartCoroutine(SingleSceneLoad(loadArcadeMap));
        Destroy(gameObject);
    }

    private IEnumerator SingleSceneLoad(AsyncOperation loadScene)
    {
        while(!loadScene.isDone)
        {
            yield return null; 
        }
    }
}
