using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Fades the panel in and returns the Tween so it can be yielded in a coroutine.
    /// </summary>
    public virtual Tween Show(float duration)
    {
        // Ensure the game object is active and can receive raycasts
        gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        // Kill any active tweens on this CanvasGroup to avoid overlapping conflicts
        canvasGroup.DOKill(); 

        return canvasGroup.DOFade(1f, duration);
    }

    /// <summary>
    /// Fades the panel out and returns the Tween.
    /// </summary>
    public virtual Tween Hide(float duration)
    {
        // Disable interaction immediately so player can't click fading buttons
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        canvasGroup.DOKill();

        // Fade out, then deactivate the GameObject once the tween is fully complete
        return canvasGroup.DOFade(0f, duration);
    }

    /// <summary>
    /// Instantly hides the UI without transition animations.
    /// </summary>
    public virtual void ResetUI()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOKill();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        //gameObject.SetActive(false);
    }
}