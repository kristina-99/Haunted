using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEvents;

public class StartMenuPanel : UIPanel
{
    public Button startButton;
    public Button quitButton;

    void OnEnable()
    {
        OnGameEnded += HandleEndMenu;
    }

    void OnDisable()
    {
        OnGameEnded -= HandleEndMenu;
    }

    void Start()
    {
        startButton.onClick.AddListener(GameStarted);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void HandleEndMenu(GameConstants.GameResult result)
    {
        StartCoroutine(ShowPanelAfterResultScreen());
        
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene("Core");
    }

    private IEnumerator ShowPanelAfterResultScreen()
    {
        yield return new WaitForSeconds(10f);
        Show(2f);
    }
}
