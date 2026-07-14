using UnityEngine;
using UnityEngine.UI;
using static GameEvents;

public class StartMenuUI : UIPanel
{
    public Button startButton;
    public Button quitButton;
    private CanvasGroup startCanvas;

    void Start()
    {
        startButton.onClick.AddListener(() => OnStartButtonClick());
        quitButton.onClick.AddListener(() => OnQuitButtonClick());
    }

    private void OnStartButtonClick()
    {
        GameStarted();
        Hide();
    }

    private void OnQuitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
