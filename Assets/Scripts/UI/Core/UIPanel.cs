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

    public virtual Tween Show(float duration)
    {
        gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        canvasGroup.DOKill(); 

        return canvasGroup.DOFade(1f, duration);
    }

    public virtual Tween Hide(float duration)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        canvasGroup.DOKill();
        return canvasGroup.DOFade(0f, duration);
    }

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