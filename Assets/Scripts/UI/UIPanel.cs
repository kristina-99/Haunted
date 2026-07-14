using DG.Tweening;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected Tween activeTransition;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void ResetUI()
    {
        KillActiveTransition();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void Show(float duration = 0.5f, Ease ease = Ease.OutQuad, System.Action onComplete = null)
    {
        KillActiveTransition();
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        activeTransition = canvasGroup.DOFade(1f, duration)
            .SetEase(ease)
            .SetLink(gameObject)
            .OnComplete(() => onComplete?.Invoke());
    }

    public virtual void Hide(float duration = 0.5f, Ease ease = Ease.InQuad, System.Action onComplete = null)
    {
        KillActiveTransition();
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        activeTransition = canvasGroup.DOFade(0f, duration)
            .SetEase(ease)
            .SetLink(gameObject)
            .OnComplete(() => onComplete?.Invoke());
    }

    protected void KillActiveTransition()
    {
        if (activeTransition != null && activeTransition.IsActive())
        {
            activeTransition.Kill();
        }
        canvasGroup.DOKill();
    }

    protected virtual void OnDestroy()
    {
        KillActiveTransition();
    }
}
