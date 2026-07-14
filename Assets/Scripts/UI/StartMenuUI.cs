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
    }

    private void OnStartButtonClick()
    {
        GameStarted();
        Hide();
    }
}
