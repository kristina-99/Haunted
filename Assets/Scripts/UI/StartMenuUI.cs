using UnityEngine;
using UnityEngine.UI;
using static GameEvents;

public class StartMenuUI : UIPanel
{
    public Button startButton;
    public Button quitButton;

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
}
